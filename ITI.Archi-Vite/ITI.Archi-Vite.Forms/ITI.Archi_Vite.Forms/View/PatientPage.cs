using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class PatientPage : ContentPage
    {
        public PatientPage()
        {
			//Label topLeftText;
			Label topLeftLabel, centerLabel, bottomRightLabel;

			public AbsoluteLayoutDemoPage ()
			{
				Label header = new Label {
					Text = "AbsoluteLayout Demo",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					HorizontalOptions = LayoutOptions.Center
				};

				AbsoluteLayout simpleLayout = new AbsoluteLayout {
					BackgroundColor = Color.Blue.WithLuminosity (0.9),
					VerticalOptions = LayoutOptions.FillAndExpand
				};

				topLeftLabel = new Label {
					Text = "Top Left",
					TextColor = Color.Black
				};

				centerLabel = new Label {
					Text = "Centered",
					TextColor = Color.Black
				};

				bottomRightLabel = new Label { 
					Text = "Bottom Right",
					TextColor = Color.Black
				};


				// PositionProportional flag maps the range (0.0, 1.0) to
				// the range "flush [left|top]" to "flush [right|bottom]"
				AbsoluteLayout.SetLayoutFlags (bottomRightLabel,
					AbsoluteLayoutFlags.PositionProportional);

				AbsoluteLayout.SetLayoutBounds (topLeftLabel,
					new Rectangle (0f,
						0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

				AbsoluteLayout.SetLayoutFlags (centerLabel,
					AbsoluteLayoutFlags.PositionProportional);

				AbsoluteLayout.SetLayoutBounds (centerLabel,
					new Rectangle (0.5,
						0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

				AbsoluteLayout.SetLayoutFlags (bottomRightLabel,
					AbsoluteLayoutFlags.PositionProportional);

				AbsoluteLayout.SetLayoutBounds (bottomRightLabel,
					new Rectangle (1f,
						1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

				simpleLayout.Children.Add (topLeftLabel);
				simpleLayout.Children.Add (centerLabel);
				simpleLayout.Children.Add (bottomRightLabel);

				// Accomodate iPhone status bar.
				this.Padding = 
					new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5);

				// Build the page.
				this.Content = new StackLayout {
					Children = {
						header,
						simpleLayout
					}
				};
        }
    }
}
