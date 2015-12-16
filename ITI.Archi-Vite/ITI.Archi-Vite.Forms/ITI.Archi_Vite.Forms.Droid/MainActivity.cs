using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;

using Xamarin.Forms;

using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace ITI.Archi_Vite.Forms.Droid
{
    [Activity(Label = "Archi'Vite", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity :  XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
			base.OnCreate (bundle);

			#region Resolver Init
			SimpleContainer container = new SimpleContainer();
			container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
			container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
			container.Register<INetwork>(t => t.Resolve<IDevice>().Network);

			Resolver.SetResolver(container.GetResolver());
			#endregion

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
        }
    }
}

