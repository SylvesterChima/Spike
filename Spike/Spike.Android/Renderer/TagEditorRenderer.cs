using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Spike.controls;
using Spike.Droid.Renderer;
using Spike.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TagEditor), typeof(TagEditorRenderer))]
namespace Spike.Droid.Renderer
{
    public class TagEditorRenderer: EditorRenderer //ViewRenderer<TagEditor, EditText>
    {
        public List<model.Person> addedNames = new List<model.Person>();
        int NextCursorPosition = 0;

        public TagEditorRenderer(Context context): base(context)
        {

        }

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
                    Control.BeforeTextChanged += Control_BeforeTextChanged;
                    Control.AfterTextChanged += Control_AfterTextChanged;

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
            ReFormat();
        }

        private void Editor_UpdateMentionNames(object sender, TagEditor.UpdateAddedNamesEventArgs e)
        {
            addedNames = e.AddedNames;
        }

        private void Editor_AddMention(object sender, TagEditor.AddMentionEventArgs e)
        {
            try
            {
                var cp = Control.SelectionStart;
                SpannableStringBuilder builder = new SpannableStringBuilder(Control.Text);
                NextCursorPosition = cp + e.MentionText.Length;
                builder.Insert(cp, e.MentionText);
                Control.TextFormatted = builder;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        private void ReFormat()
        {
            try
            {
                var msg = Control.Text;
                var cp = 0;
                SpannableStringBuilder builder = new SpannableStringBuilder();
                var words = msg.Split(' ');
                var index = 0;
                var totalCount = words.Count();
                foreach (var item in words)
                {
                    if (addedNames.FirstOrDefault(c => c.Name.ToLower().Split(' ').Contains(item.ToLower())) != null)
                    {
                        SpannableString spannable;
                        var text = "";
                        var isTag = false;
                        if (addedNames.FirstOrDefault(c => c.Name.ToLower().Split(' ')[0] == item.ToLower()) != null)
                        {
                            text = "@" + item[1].ToString().ToUpper() + item.Substring(2);
                            isTag = true;
                        }
                        else
                        {
                            text = item;
                            if (index - 1 >= 0)
                            {
                                if (words[index - 1] != "" && words[index - 1].Substring(0, 1) == "@")
                                {
                                    isTag = true;
                                }
                            }
                        }
                        if (isTag)
                        {
                            spannable = new SpannableString(text);
                            spannable.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.ParseColor("#1C92B0")), 0, item.Length, SpanTypes.ExclusiveExclusive);
                            spannable.SetSpan(new StyleSpan(Android.Graphics.TypefaceStyle.Bold), 0, item.Length, SpanTypes.ExclusiveExclusive);
                            builder.Append(spannable);
                        }
                    }
                    else
                    {
                        builder.Append(item);
                    }
                    if (index != totalCount - 1)
                    {
                        builder.Append(" ");
                        cp = cp + item.Length + 1;
                    }
                    else
                    {
                        cp = cp + item.Length;
                    }

                    index++;

                }

                Control.TextFormatted = builder;
                Control.SetSelection(NextCursorPosition > builder.Length() ? NextCursorPosition - 1 : NextCursorPosition);
            }
            catch (System.Exception ex)
            {

            }
        }

        private void Control_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            MessagingCenter.Send<object, int>(this, "setCurrentCursorPosition", Control.SelectionStart);
        }

        private async void Control_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                if (e.BeforeCount > e.AfterCount)
                {
                    var cp = Control.SelectionStart;
                    var cp2 = Control.SelectionStart;
                    var msg = Control.Text;
                    var nextSpaceIndex = msg.IndexOf(' ', cp);
                    if(nextSpaceIndex != -1)
                    {
                        cp = nextSpaceIndex;
                    }
                    else
                    {
                        cp = msg.Length;
                    }
                    SpannableStringBuilder builder = new SpannableStringBuilder(Control.TextFormatted);

                    var lastWord = builder.SubSequence(0, cp).Split(' ').LastOrDefault();
                    if (lastWord != null)
                    {
                        if (addedNames.FirstOrDefault(c => c.Name.Split(' ').Contains(lastWord)) != null)
                        {
                            builder.Delete(cp - lastWord.Length, cp);
                            await Task.Delay(50);
                            var nextCP = cp - lastWord.Length;
                            Control.TextFormatted = builder;
                            Control.SetSelection(nextCP);
                            NextCursorPosition = nextCP;
                            ReFormat();
                            var tagObj = addedNames.FirstOrDefault(c => c.Name.Split(' ')[0] == lastWord);
                            if (tagObj != null)
                            {
                                addedNames.Remove(tagObj);
                                MessagingCenter.Send<object, List<model.Person>>(this, "RemoveTags", addedNames);
                            }
                        }
                        else
                        {
                            var lastWord2 = builder.SubSequence(0, cp2 - 1).Split(' ').LastOrDefault();
                            if (lastWord2 != null)
                            {
                                if(addedNames.FirstOrDefault(c => c.Name.Split(' ').Contains(lastWord2)) != null)
                                {
                                    NextCursorPosition = cp2 - 1;
                                    await Task.Delay(200);
                                    ReFormat();
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

    }


    public class mData
    {
        public string Name { get; set; }
        public cData IndexRange { get; set; }
    }

    public class cData
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
}