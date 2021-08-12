using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Spike.controls;
using Spike.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TagEditor), typeof(TagEditorRenderer))]
namespace Spike.Droid.Renderer
{
    [Obsolete]
    public class TagEditorRenderer: EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            //if (e.NewElement != null)
            //    Control.AfterTextChanged += Control_AfterTextChanged;

            //if (e.OldElement != null)
            //    Control.AfterTextChanged -= Control_AfterTextChanged;

        }

        private void Control_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            //detect if '@' is entered.
            if (e.Editable.LastOrDefault() == '@')
            {
                //show a popup list for selection.
                //I here use a simple menu for testing, you should be able to change it to your list popup.
                //PopupMenu popup = new PopupMenu(Xamarin.Forms.Forms.Context, Control);
                //popup.Menu.Add(Android.Views.Menu.None, 1, 1, "Fried Rice");
                //popup.Menu.Add(Android.Views.Menu.None, 2, 2, "Grilled Chilli Meat");
                //popup.Menu.Add(Android.Views.Menu.None, 3, 3, "Egg Sauce");
                //popup.Gravity = GravityFlags.RelativeLayoutDirection;
                //popup.Show();
                //popup.MenuItemClick += (ss, ee) =>
                //{
                //    var item = ee.Item.TitleFormatted;
                //    e.Editable.Delete(e.Editable.Length() - 1, e.Editable.Length());
                //    SpannableString spannable = new SpannableString("@" + item);
                //    spannable.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.Blue), 0, item.Length() + 1, SpanTypes.ExclusiveExclusive);
                //    e.Editable.Append(spannable);
                //    popup.Dismiss();
                //};

                MessagingCenter.Send<object, string>(this, "filter", e.Editable.LastOrDefault().ToString());
            }
        }
    }
}