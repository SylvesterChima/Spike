using Spike.model;
using Spike.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spike.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public List<Person> addedNames = new List<Person>();
        private string currentName = "";
        private string oldText = "";
        private int CurrentCursorPosition = 0;

        public ChatPage()
        {
            InitializeComponent();
            //rteMessage.DefaultFontColor = Color.Black;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, int>(this, "setCurrentCursorPosition", (p, position) =>
            {
                CurrentCursorPosition = position;
            });

            MessagingCenter.Subscribe<object, List<Person>>(this, "RemoveTags", (p, tags) =>
            {
                addedNames = tags;
                Console.WriteLine($"hhs::::{addedNames.Count}");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, int>(this, "setCurrentCursorPosition");
            MessagingCenter.Unsubscribe<object, List<model.Person>>(this, "RemoveTags");
        }


        private void Member_Tapped(object sender, EventArgs e)
        {
            var model = this.BindingContext as ChatViewModel;
            try
            {
                var person = (Person)((Grid)sender).BindingContext;
                StringBuilder newText = new StringBuilder();
                if (person != null)
                {
                    var splitBySpaceArrayMgs = mMessage.Text.Substring(0, CurrentCursorPosition).Split(' ');
                    var lastWord = splitBySpaceArrayMgs.Where(c => c != "").LastOrDefault();
                    if (lastWord.ToLower() != currentName.ToLower())
                    {
                        currentName = lastWord;
                    }
                    var rs = person.Name.Substring(currentName.Length - 1);
                    if (currentName.Length > 1 && currentName.Substring(1, 1) == currentName.Substring(1, 1).ToLower())
                    {
                        MessagingCenter.Send<object, string>(this, "appendText", rs.ToLower());

                    }
                    else
                    {
                        MessagingCenter.Send<object, string>(this, "appendText", rs);
                    }

                    addedNames.Add(new Person { Name = "@" + person.Name });
                    MessagingCenter.Send<object, List<Person>>(this, "AddTags", addedNames);

                    //if (addedNames.FirstOrDefault(c => c.Name == "@" + person.Name) == null)
                    //{

                    //}

                    MessagingCenter.Send<object>(this, "reformatText");
                    model.ShowTags = false;

                }
            }
            catch (Exception ex)
            {

            }

        }


        //private async void Member_Tapped(object sender, EventArgs e)
        //{
        //    var model = this.BindingContext as ChatViewModel;
        //    try
        //    {
        //        var person = (Person)((Grid)sender).BindingContext;
        //        StringBuilder newText = new StringBuilder();
        //        if (person != null)
        //        {
        //            var mTextValue = rteMessage.GetRawString();
        //            var cp = rteMessage.CursorPosition;
        //            var mm = mTextValue.Substring(0, cp);
        //            var splitByEnterArrayMgs = mm.Split('\n');
        //            var splitByNBSArrayMgs = string.Join(" ", splitByEnterArrayMgs).Split('\xA0');
        //            var splitBySpaceArrayMgs = string.Join(" ", splitByNBSArrayMgs).Split(' ');
        //            var lastWord = splitBySpaceArrayMgs.Where(c => c != "").LastOrDefault();
        //            if(lastWord.ToLower() != currentName.ToLower())
        //            {
        //                currentName = lastWord;
        //            }
        //            var rs = person.Name.Substring(currentName.Length - 1);
        //            if (currentName.Length > 1 && currentName.Substring(1, 1) == currentName.Substring(1, 1).ToLower())
        //            {
        //                rteMessage.InsertHTMLText(rs.ToLower());
        //            }
        //            else
        //            {
        //                rteMessage.InsertHTMLText(rs);
        //            }

        //            if (addedNames.FirstOrDefault(c => c == person.Name) == null)
        //            {
        //                addedNames.Add("@" + person.Name);
        //            }

        //            await Task.Delay(100);
        //            string mText = rteMessage.GetRawString();
        //            foreach (var item in addedNames)
        //            {
        //                if (mText.ToLower().Contains(item.ToLower()))
        //                {
        //                    if("@" + person.Name.ToLower() == item.ToLower())
        //                    {
        //                        if (currentName.Length > 1 && currentName.Substring(1, 1) == currentName.Substring(1, 1).ToLower())
        //                        {
        //                            var lc = item.ToLower();
        //                            mText = mText.Replace(lc, $"<a href=\"#\" style=\"color:blue; text-decoration: none;\">{item + '\xA0'}</a>");
        //                        }
        //                        else
        //                        {
        //                            mText = mText.Replace(item, $"<a href=\"#\" style=\"color:blue; text-decoration: none;\">{item + '\xA0'}</a>");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        mText = mText.Replace(item, $"<a href=\"#\" style=\"color:blue; text-decoration: none;\">{item}</a>");
        //                    }
        //                }
        //            }

        //            rteMessage.Text = mText.Trim();
        //            await Task.Delay(200);
        //            rteMessage.CursorPosition = rteMessage.Text.Length;
        //            //MainThread.BeginInvokeOnMainThread(() =>
        //            //{
        //            //    rteMessage.CursorPosition = mText.Length;
        //            //});

        //            currentName = "";
        //            model.ShowTags = false;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //private async void rteMessage_TextChanged(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        //{
        //    var model = this.BindingContext as ChatViewModel;
        //    try
        //    {
        //        if (rteMessage.Text != null)
        //        {
        //            var splitByEnterArrayMgs = rteMessage.Text.Split('\n');
        //            var splitByNBSArrayMgs = string.Join(" ", splitByEnterArrayMgs).Split('\xA0');
        //            var splitBySpaceArrayMgs = string.Join(" ", splitByNBSArrayMgs).Split(' ');
        //            var arrayWithSymbols = splitBySpaceArrayMgs.Where(c => c != "" && c.Substring(0, 1) == "@").ToList();
        //            var dt = arrayWithSymbols.Where(c => addedNames.FirstOrDefault(x => x.ToLower() == c.ToLower()) == null).ToList();


        //            if (dt.Count > 0)
        //            {
        //                foreach (var item in dt)
        //                {
        //                    var isAdded = addedNames.FirstOrDefault(c => c.ToLower() == item.ToLower());
        //                    if (isAdded == null)
        //                    {
        //                        var mShow = true;
        //                        var ff = addedNames.Where(c => c.Contains(" "));
        //                        foreach (var sp in ff)
        //                        {
        //                            var fsv = sp.Split(' ')[0];
        //                            if(fsv.ToLower() == item.ToLower())
        //                            {
        //                                mShow = false;
        //                            }
        //                        }
        //                        if (mShow)
        //                        {
        //                            currentName = item;
        //                            model.ShowTags = true;
        //                            if (string.IsNullOrWhiteSpace(item.Replace("@", "")))
        //                            {
        //                                model.People = model.mPeople;
        //                            }
        //                            else
        //                            {
        //                                //model.People = model.mPeople.Where(c => c.Name.ToLower().Contains(item.ToLower().Replace("@", ""))).ToList();
        //                                var sq = currentName.ToLower().Replace("@", "");
        //                                model.People = model.mPeople.Where(c => c.Name.Length >= sq.Length && c.Name.ToLower().Substring(0, sq.Length) == sq).ToList();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            model.ShowTags = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        model.ShowTags = false;
        //                    }
        //                }
        //            }
        //            else
        //            {   
        //                model.ShowTags = false;
        //            }

        //            await Task.Delay(200);
        //            if(e.Text.Length < oldText.Length)
        //            {
        //                var mTextValue = rteMessage.GetRawString();

        //                if (!string.IsNullOrWhiteSpace(mTextValue))
        //                {
        //                    if (mTextValue.Length >= 3)
        //                    {
        //                        if (mTextValue.Length >= rteMessage.CursorPosition - 2)
        //                        {
        //                            var cp = rteMessage.CursorPosition - 2;
        //                            if (cp > 0)
        //                            {
        //                                var subStr = mTextValue.Substring(0, cp);
        //                                var lastWord = subStr.Split(' ').Where(c => c != "").LastOrDefault();
        //                                if (lastWord != null)
        //                                {
        //                                    if (lastWord.Substring(0, 1) == "@")
        //                                    {
        //                                        var dList = new List<string>();
        //                                        foreach (var item in addedNames)
        //                                        {
        //                                            if (!mTextValue.ToLower().Contains(item.ToLower()))
        //                                            {
        //                                                dList.Add(item);
        //                                            }
        //                                        }
        //                                        foreach (var item in dList)
        //                                        {
        //                                            addedNames.Remove(item);
        //                                        }

        //                                        if (dList.Count > 0)
        //                                        {
        //                                            if (addedNames.Count > 0)
        //                                            {
        //                                                foreach (var item in addedNames)
        //                                                {
        //                                                    if (mTextValue.ToLower().Contains(item.ToLower()))
        //                                                    {
        //                                                        mTextValue = mTextValue.Replace(item, $"<a href=\"#\" style=\"color:blue; text-decoration: none;\">{item}</a>");
        //                                                    }
        //                                                }
        //                                            }
        //                                            //rteMessage.Text = "";
        //                                            rteMessage.Text = mTextValue;
        //                                            if (dList.Count > 0)
        //                                            {
        //                                                await Task.Delay(200);
        //                                                MainThread.BeginInvokeOnMainThread(() => {
        //                                                    rteMessage.CursorPosition = cp;
        //                                                });
        //                                                //await Task.Delay(10000);
        //                                                if (subStr.Length + 1 > cp)
        //                                                {
        //                                                    if (rteMessage.Text.Length > cp)
        //                                                    {
        //                                                        if (rteMessage.Text[cp] != ' ' || rteMessage.Text[cp] != '\xA0')
        //                                                        {
        //                                                            cp = cp + 1;
        //                                                            await Task.Delay(200);
        //                                                            MainThread.BeginInvokeOnMainThread(() => {
        //                                                                rteMessage.CursorPosition = cp;
        //                                                            });
        //                                                            if (subStr.Length > cp)
        //                                                            {
        //                                                                if (rteMessage.Text[cp] != ' ' || rteMessage.Text[cp] != '\xA0')
        //                                                                {
        //                                                                    cp = cp + 1;
        //                                                                    await Task.Delay(200);
        //                                                                    MainThread.BeginInvokeOnMainThread(() => {
        //                                                                        rteMessage.CursorPosition = cp;
        //                                                                    });
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //            }
        //            oldText = e.Text;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var model = this.BindingContext as ChatViewModel;
        //    try
        //    {
        //        //var text = $"<!DOCTYPE html><html><body><h1>The a element</h1><a href=\"#\" style=\"color: green; text-decoration:none;\">Visit W3Schools.com!</a></body></html>";
        //        model.Messages.Add(new Message { Text = rteMessage.HtmlText.Replace("<a","<span").Replace("</a>", "</span>") });
        //        addedNames.Clear();
        //        currentName = "";
        //        rteMessage.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        private void mMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            var model = this.BindingContext as ChatViewModel;
            try
            {
                if (mMessage.Text != null)
                {
                    var splitBySpaceArrayMgs = mMessage.Text.Split(' ');
                    var arrayWithSymbols = splitBySpaceArrayMgs.Where(c => c != "" && c.Substring(0, 1) == "@").ToList();
                    var dt = arrayWithSymbols.Where(c => addedNames.FirstOrDefault(x => x.Name.ToLower() == c.ToLower()) == null).ToList();


                    if (dt.Count > 0)
                    {
                        foreach (var item in dt)
                        {
                            var isAdded = addedNames.FirstOrDefault(c => c.Name.ToLower() == item.ToLower());
                            if (isAdded == null)
                            {
                                var mShow = true;
                                var ff = addedNames.Where(c => c.Name.Contains(" "));
                                foreach (var sp in ff)
                                {
                                    var fsv = sp.Name.Split(' ')[0];
                                    if (fsv.ToLower() == item.ToLower())
                                    {
                                        mShow = false;
                                    }
                                }
                                if (mShow)
                                {
                                    currentName = item;
                                    model.ShowTags = true;
                                    if (string.IsNullOrWhiteSpace(item.Replace("@", "")))
                                    {
                                        model.People = model.mPeople;
                                    }
                                    else
                                    {
                                        var sq = currentName.ToLower().Replace("@", "");
                                        model.People = model.mPeople.Where(c => c.Name.Length >= sq.Length && c.Name.ToLower().Substring(0, sq.Length) == sq).ToList();
                                    }
                                }
                                else
                                {
                                    model.ShowTags = false;
                                }
                            }
                            else
                            {
                                model.ShowTags = false;
                            }
                        }
                    }
                    else
                    {
                        model.ShowTags = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}