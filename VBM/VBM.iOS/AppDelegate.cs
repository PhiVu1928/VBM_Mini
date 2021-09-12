using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using FFImageLoading.Forms.Platform;
using Syncfusion.XForms.iOS.TextInputLayout;

namespace VBM.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            SfTextInputLayoutRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfListViewRenderer.Init();
            SfTimePickerRenderer.Init();
            SfDatePickerRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();
            CachedImageRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyBXmXQSq7dKDdfo9V1aUMseqa1zURZslT4");
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
