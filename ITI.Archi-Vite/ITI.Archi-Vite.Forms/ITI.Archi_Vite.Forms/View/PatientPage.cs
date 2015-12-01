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
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            Label heading = new Label
            {
                Text = "RelativeLayout Example",
                TextColor = Color.Red,
            };

            Label relativelyPositioned = new Label
            {
                Text = "Positioned relative to my parent."
            };

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(heading, Constraint.RelativeToParent((parent) =>
            {
                return 0;
            }));

            relativeLayout.Children.Add(relativelyPositioned,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 3;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height / 2;
                }));
            this.Content = relativeLayout;
        }
    }
}
