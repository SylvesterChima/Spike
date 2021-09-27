using Foundation;
using Spike.controls;
using Spike.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TagEditor), typeof(TagEditorRenderer))]
namespace Spike.iOS.Renderer
{
    public class TagEditorRenderer: EditorRenderer
    {
        public List<model.Person> addedNames = new List<model.Person>();
        int NextCursorPosition = 0;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                if (e.OldElement is TagEditor editor)
                {
                    editor.MentionAdd -= Editor_AddMention;
                    editor.UpdateMentionNames -= Editor_UpdateMentionNames;
                    editor.ReformatText -= Editor_ReformatText;
                }
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    
                }


                if (Control != null)
                {
                    if (e.NewElement is TagEditor editor)
                    {
                        editor.MentionAdd += Editor_AddMention;
                        editor.UpdateMentionNames += Editor_UpdateMentionNames;
                        editor.ReformatText += Editor_ReformatText;
                    }

                }
            }
        }

        private void Editor_ReformatText(object sender, EventArgs e)
        {
            //refort text
        }

        private void Editor_UpdateMentionNames(object sender, TagEditor.UpdateAddedNamesEventArgs e)
        {
            //update list
        }

        private void Editor_AddMention(object sender, TagEditor.AddMentionEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

    }
}