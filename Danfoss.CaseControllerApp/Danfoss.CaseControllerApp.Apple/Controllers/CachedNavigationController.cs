using System;
using System.Linq;
using Daven.SyntaxExtensions;
using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.Platform;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class CachedNavigationController : UINavigationController
    {
        public CachedNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
            
        }

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            var index = ViewControllers.IndexOf(x => x.GetType() == viewController.GetType());
            if (index != -1)
            {
                var top = ViewControllers.Last();
                if (top.GetType() == viewController.GetType())
                {
                    base.PopViewController(false);
                    base.PushViewController(viewController, false);
                }
                else
                {
                    var popTo = ViewControllers[index - 1];
                    base.PopToViewController(popTo, false);
                    base.PushViewController(viewController, false);
                    base.PushViewController(top, false);

                    base.PopViewController(true);
                }
            }
            else
            {
                base.PushViewController(viewController, (ViewControllers.Length > 1) && animated);
            }
        }
    }
}