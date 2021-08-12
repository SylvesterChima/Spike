using Android.Text;
using Android.Text.Style;
using Spike.model;
using Spike.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Spike
{
    public partial class MainPage : ContentPage
    {
        public IList<Monkey> Monkeys { get; private set; }
        public List<string> addedNames = new List<string>();
        public string currentName = "";
        //public IList<Monkey> mMonkeys { get; private set; }
        public IList<Person> People { get; private set; }
        public IList<Person> mPeople { get; private set; }
        public bool ShowTags { get; set; } = false;

        public MainPage()
        {
            InitializeComponent();


            Monkeys = new List<Monkey>();
            People = new List<Person>();
            Monkeys.Add(new Monkey
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Howler Monkey",
                Location = "South America",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg"
            });


            People.Add(new Person { Name = "Chris", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg" });
            People.Add(new Person { Name = "Sylvester", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG" });
            People.Add(new Person { Name = "Chima", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Thomas%27s_langur_Presbytis_thomasi.jpg/142px-Thomas%27s_langur_Presbytis_thomasi.jpg" });
            People.Add(new Person { Name = "Kingsley", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg/320px-Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg" });
            People.Add(new Person { Name = "Kenneth", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/RhinopitecusBieti.jpg/320px-RhinopitecusBieti.jpg" });
            People.Add(new Person { Name = "Sunday", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg" });
            People.Add(new Person { Name = "Kalu", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/320px-Cuc.Phuong.Primate.Rehab.center.jpg" });
            People.Add(new Person { Name = "Femi", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg" });
            People.Add(new Person { Name = "Festus", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg" });
            mPeople = People;

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, string>(this, "filter", (p, str) =>
              {
                  this.ShowTags = true;
                  if (!string.IsNullOrEmpty(str))
                  {
                      sNames.IsVisible = true;
                      //People = mPeople.Where(c => c.Name.Contains(str)).ToList();
                  }
              });
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
            Page page = FreshMvvm.FreshPageModelResolver.ResolvePageModel<ChatViewModel>();
            NavigationPage nav = new FreshMvvm.FreshNavigationContainer(page) as NavigationPage;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
        }

        private void Editor_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //try
            //{
            //    if (txtMessage.Text != null)
            //    {
            //        var arrayMgs = txtMessage.Text.Split(' ');
            //        var arrayWithSymbols = arrayMgs.Where(c => c != "" && c.Substring(0, 1) == "@").ToList();
            //        var dt = arrayWithSymbols.Where(c => addedNames.FirstOrDefault(x => x.ToLower() == c.ToLower()) == null).ToList();
            //        if (dt.Count > 0)
            //        {
            //            foreach (var item in dt)
            //            {
            //                var isAdded = addedNames.FirstOrDefault(c => c.ToLower() == item.ToLower().Replace("@", ""));
            //                if (isAdded == null)
            //                {
            //                    currentName = item;
            //                    this.ShowTags = true;
            //                    filterConatiner.IsVisible = true;
            //                    if (string.IsNullOrWhiteSpace(item.Replace("@", "")))
            //                    {
            //                        People = mPeople;
            //                    }
            //                    else
            //                    {
            //                        People = mPeople.Where(c => c.Name.ToLower().Contains(item.ToLower().Replace("@", ""))).ToList();
            //                    }
            //                    BindableLayout.SetItemsSource(sNames, People);
            //                    //return;
            //                }
            //                else
            //                {
            //                    this.ShowTags = false;
            //                    filterConatiner.IsVisible = false;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            this.ShowTags = false;
            //            filterConatiner.IsVisible = false;
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void Member_Tapped(object sender, EventArgs e)
        {
            var person = (Person)((Grid)sender).BindingContext;
            if (person != null)
            {
                if(addedNames.FirstOrDefault(c=>c==person.Name)== null)
                {
                    addedNames.Add("@" + person.Name);
                }

                var txt = new List<string>();
                foreach (var item in txtMessage.Text.Split(' '))
                {
                    if (item == currentName)
                    {
                        SpannableString spannable = new SpannableString("@" + person.Name);
                        spannable.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.Blue), 0, person.Name.Length + 1, SpanTypes.ExclusiveExclusive);
                        //txt.Add("@" + person.Name);

                        txt.Add(spannable.ToString());
                    }
                    else
                    {
                        txt.Add(item);
                    }
                }
                var newText = string.Join(" ", txt);
                txtMessage.Text = newText.TrimEnd(' ') + " ";

                currentName = "";

                this.ShowTags = false;
                filterConatiner.IsVisible = false;
            }
        }

        private void SfRichTextEditor_TextChanged(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        {
            try
            {
                if (rteMessage.Text != null)
                {

                    var arrayMgs = rteMessage.Text.Split(' ');
                    var arrayWithSymbols = arrayMgs.Where(c => c != "" && c.Substring(0, 1) == "@").ToList();
                    var dt = arrayWithSymbols.Where(c => addedNames.FirstOrDefault(x => x.ToLower() == c.ToLower()) == null).ToList();
                    if (dt.Count > 0)
                    {
                        foreach (var item in dt)
                        {
                            var isAdded = addedNames.FirstOrDefault(c => c.ToLower() == item.ToLower().Replace("@", ""));
                            if (isAdded == null)
                            {
                                currentName = item;
                                this.ShowTags = true;
                                filterConatiner.IsVisible = true;
                                if (string.IsNullOrWhiteSpace(item.Replace("@", "")))
                                {
                                    People = mPeople;
                                }
                                else
                                {
                                    People = mPeople.Where(c => c.Name.ToLower().Contains(item.ToLower().Replace("@", ""))).ToList();
                                }
                                BindableLayout.SetItemsSource(sNames, People);
                                //return;
                            }
                            else
                            {
                                this.ShowTags = false;
                                filterConatiner.IsVisible = false;
                            }
                        }
                    }
                    else
                    {
                        this.ShowTags = false;
                        filterConatiner.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
