using System.Linq;
using Danfoss.CaseControllerApp.Apple.Controllers;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Support.XamarinSidebar.Hints;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Exceptions;
using SidebarNavigation;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Infrastructure
{
    public class DanfossPresenter : MvxIosViewPresenter
    {
        private HeaderViewController _content;
        protected virtual UINavigationController ParentRootViewController { get; set; }
        public static MvxSidebarPanelController RootViewController { get; set; }

        public DanfossPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
            AddPresentationHintHandler<MvxSidebarActivePanelPresentationHint>(PresentationHintHandler);
            AddPresentationHintHandler<MvxSidebarPopToRootPresentationHint>(PresentationHintHandler);
            AddPresentationHintHandler<MvxSidebarResetRootPresentationHint>(PresentationHintHandler);
        }

        private bool PresentationHintHandler(MvxPanelPresentationHint hint)
        {
            if (hint == null)
                return false;

            hint.Navigate();

            return true;
        }

        public override void Show(MvxViewModelRequest request)
        {
            IMvxIosView viewController = Mvx.Resolve<IMvxIosViewCreator>().CreateView(request);
            Show(viewController);
        }

        public override void Show(IMvxIosView view)
        {
            if (view is IMvxModalIosView)
            {
                PresentModalViewController(view as UIViewController, true);
                return;
            }

            var viewController = view as UIViewController;

            if (viewController == null)
            {
                throw new MvxException("Passed in IMvxIosView is not a UIViewController");
            }

            if (RootViewController == null)
            {
                InitRootViewController();
            }

            var viewPresentationAttribute = GetViewPresentationAttribute(view);

            switch (viewPresentationAttribute.Panel)
            {
                case MvxPanelEnum.Left:
                {
                    RootViewController.LeftSidebarController.ChangeMenuView(viewController);
                    return;
                }
                case MvxPanelEnum.Right:
                {
                    var root = viewController.CastInstanceTo<MvxViewController>();
                    var vm = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(root.Request, null).CastInstanceTo<RootViewModel>();
                    root.ViewModel = vm;
                    _content = new HeaderViewController();
                    _content.ViewModel = vm;
                    RootViewController.NavigationController.PushViewController(_content, false);
                    RootViewController.RightSidebarController.ChangeMenuView(viewController);
                    return;
                }
                case MvxPanelEnum.Center:
                {
                    _content.Content.PushViewController(viewController, true);
                    return;
                }
            }
        }

        public override void Close(IMvxViewModel toClose)
        {
            if (ParentRootViewController.ViewControllers.Count() > 1)
                ParentRootViewController.PopViewController(true);
            else if (RootViewController.NavigationController.ViewControllers.Count() > 1)
                RootViewController.NavigationController.PopViewController(true);
            else
                base.Close(toClose);
        }

        protected MvxPanelPresentationAttribute GetViewPresentationAttribute(IMvxIosView view)
        {
            if (view == null)
                return default(MvxPanelPresentationAttribute);

            return view.GetType().GetCustomAttributes(typeof(MvxPanelPresentationAttribute), true).FirstOrDefault() as MvxPanelPresentationAttribute;
        }

        protected virtual void InitRootViewController()
        {
            foreach (var view in Window.Subviews)
                view.RemoveFromSuperview();

            MasterNavigationController = new UINavigationController();

            OnMasterNavigationControllerCreated();

            RootViewController = new MvxSidebarPanelController(MasterNavigationController);
            RootViewController.Initialize();
            RootViewController.RightSidebarController.MenuLocation = MenuLocations.Right;
            RootViewController.LeftSidebarController.MenuLocation = MenuLocations.Left;
            RootViewController.LeftSidebarController.Disabled = true;
            RootViewController.RightSidebarController.HasShadowing = false;

            ParentRootViewController = new UINavigationController(RootViewController);
            ParentRootViewController.NavigationBarHidden = true;

            SetWindowRootViewController(ParentRootViewController);

            Mvx.RegisterSingleton<IMvxSideMenu>(RootViewController);
        }
    }
}