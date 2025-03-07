using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms.Maps; // <-- Add this 'using' statement

namespace Pizza_App.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Initialize Xamarin.Forms.Maps for iOS
            Xamarin.FormsMaps.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
