using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spike.controls;
using Spike.Droid.Renderer;
using Syncfusion.XForms.Android.RichTextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MentionEditor), typeof(MentionEditorRenderer))]
namespace Spike.Droid.Renderer
{
    public class MentionEditorRenderer: SfRichTextEditorRenderer
    {
         
    }
}