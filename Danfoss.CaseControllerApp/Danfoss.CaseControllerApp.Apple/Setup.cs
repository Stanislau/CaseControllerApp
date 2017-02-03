using System;
using System.Collections.Generic;
using Danfoss.CaseControllerApp.Apple.Infrastructure;
using Danfoss.CaseControllerApp.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            return new DanfossPresenter((MvxApplicationDelegate)ApplicationDelegate, Window);
        }

        protected override void InitializeViewLookup()
        {
            IDictionary<Type, Type> viewModelViewLookup = new MvxViewModelViewLookupBuilderIgnoreSupport().Build(this.GetViewAssemblies());
            
            if (viewModelViewLookup == null)
                return;

            Mvx.Resolve<IMvxViewsContainer>().AddAll(viewModelViewLookup);
        }
    }
}