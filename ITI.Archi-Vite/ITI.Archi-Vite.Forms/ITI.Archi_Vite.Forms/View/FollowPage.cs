using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class FollowPage : ContentPage
	{
		public FollowPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


