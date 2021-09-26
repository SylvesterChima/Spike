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
        //List<string> items = new List<string> { "@Sylvester", "@Femi", "@Sugun" };
        public List<model.Person> addedNames = new List<model.Person>();
        public List<mTaggedNames> mNames = new List<mTaggedNames>();
        int NextCursorPosition = 0;

        public TagEditorRenderer(Context context): base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.k += Control_KeyPress;
                Control.BeforeTextChanged += Control_BeforeTextChanged;
                Control.AfterTextChanged += Control_AfterTextChanged;

                MessagingCenter.Unsubscribe<object>(this, "reformatText");
                MessagingCenter.Unsubscribe<object, string>(this, "appendText");
                MessagingCenter.Unsubscribe<object, List<model.Person>>(this, "AddTags");


                MessagingCenter.Subscribe<object>(this, "reformatText", (p) =>
                {
                    ReFormat();
                });

                MessagingCenter.Subscribe<object, TaggingUser>(this, "appendText", (p, tag) =>
                {
                    var cp = Control.SelectionStart;
                    SpannableStringBuilder builder = new SpannableStringBuilder(Control.TextFormatted);
                    NextCursorPosition = (cp - tag.TypedCount) + tag.Text.Length;
                    var isFirst = true;
                    var mCP = 0;

                    foreach (var item in tag.Text.Split(' '))
                    {
                        var spannable = new SpannableString(item);
                        spannable.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.ParseColor("#1C92B0")), 0, item.Length, SpanTypes.ExclusiveExclusive);
                        if (isFirst)
                        {
                            mCP = cp - tag.TypedCount;
                            builder.Delete(mCP, cp);
                            builder.Insert(mCP, spannable);
                            mNames.Add(new mTaggedNames
                            {
                                Name = builder.SubSequence(mCP, mCP + item.Length),
                                IndexRange = new mIndex
                                {
                                    Start = cp - tag.TypedCount,
                                    End = mCP + item.Length
                                }
                            });
                            builder.Insert(mCP + item.Length, " ");
                            mCP = mCP + item.Length;
                            isFirst = false;
                        }
                        else
                        {
                            builder.Insert(mCP + 1, spannable);
                            mNames.Add(new mTaggedNames
                            {
                                Name = builder.SubSequence(mCP + 1, NextCursorPosition),
                                IndexRange = new mIndex
                                {
                                    Start = mCP + 1,
                                    End = NextCursorPosition
                                }
                            });
                        }
                    }

                    Control.TextFormatted = builder;
                    Control.SetSelection(NextCursorPosition);
                    

                });

                MessagingCenter.Subscribe<object, List<model.Person>>(this, "AddTags", (p, tags) =>
                {
                    addedNames = tags;
                });

            }
        }

        private void ReFormat()
        {
            try
            {
                mNames.Clear();
                var msg = Control.Text;
                var cp = 0;
                SpannableStringBuilder builder = new SpannableStringBuilder();
                var words = msg.Split(' ');
                var index = 0;
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
                            builder.Append(spannable);
                            mNames.Add(new mTaggedNames
                            {
                                Name = text,
                                IndexRange = new mIndex
                                {
                                    Start = cp,
                                    End = cp + text.Length
                                }
                            });
                        }
                    }
                    else
                    {
                        builder.Append(item);
                    }
                    if (item != msg.Split(' ').LastOrDefault())
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
                    var dataObj = mNames.FirstOrDefault(c => cp >= c.IndexRange.Start && cp <= c.IndexRange.End);
                    if (dataObj != null)
                    {
                        cp = dataObj.IndexRange.End;
                        SpannableStringBuilder builder = new SpannableStringBuilder(Control.TextFormatted);


                        var name = builder.SubSequence(dataObj.IndexRange.Start, dataObj.IndexRange.End);
                        builder.Delete(dataObj.IndexRange.Start, dataObj.IndexRange.End);
                        Console.WriteLine($"deleted::::{name}");
                        await Task.Delay(50);
                        var tagObj = addedNames.FirstOrDefault(c => c.Name.Split(' ')[0] == name);
                        if (tagObj != null)
                        {
                            addedNames.Remove(tagObj);
                            MessagingCenter.Send<object, List<model.Person>>(this, "RemoveTags", addedNames);

                        }
                        var mName = mNames.FirstOrDefault(c => c.Name == name);
                        if (mName != null)
                        {
                            mNames.Remove(mName);
                        }
                        Control.TextFormatted = builder;
                        Control.SetSelection(cp - name.Length);
                        NextCursorPosition = cp - name.Length;
                        ReFormat();



                        //var lastWord = Control.Text.Substring(0, cp).Split(' ').LastOrDefault();
                        //if (lastWord != null)
                        //{
                        //    if (addedNames.FirstOrDefault(c => c.Name.Split(' ').Contains(lastWord)) != null)
                        //    {
                        //        builder.Delete(cp - lastWord.Length, cp);
                        //        await Task.Delay(50);
                        //        var nextCP = cp - lastWord.Length;
                        //        Control.TextFormatted = builder;
                        //        Control.SetSelection(nextCP);
                        //        //NextCursorPosition = nextCP;
                        //        //ReFormat();
                        //        var tagObj = addedNames.FirstOrDefault(c => c.Name.Split(' ')[0] == lastWord);
                        //        if (tagObj != null)
                        //        {
                        //            addedNames.Remove(tagObj);
                        //            MessagingCenter.Send<object, List<model.Person>>(this, "RemoveTags", addedNames);
                        //        }
                        //    }
                        //}
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

    }


    public class mTaggedNames
    {
        public string Name { get; set; }
        public mIndex IndexRange { get; set; }
    }

    public class mIndex
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
}