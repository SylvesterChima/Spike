using PropertyChanged;
using Spike.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Spike.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class ChatViewModel
    {
        public List<Person> People { get; set; }
        public List<Person> mPeople { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public bool ShowTags { get; set; }

        public ChatViewModel()
        {
            var dt = new List<Person>();
            this.Messages = new ObservableCollection<Message>();
            dt.Add(new Person { Name = "Chris Don", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg" });
            dt.Add(new Person { Name = "Sylvester", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG" });
            dt.Add(new Person { Name = "Chima", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Thomas%27s_langur_Presbytis_thomasi.jpg/142px-Thomas%27s_langur_Presbytis_thomasi.jpg" });
            dt.Add(new Person { Name = "Kingsley", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg/320px-Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg" });
            dt.Add(new Person { Name = "Kenneth", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/RhinopitecusBieti.jpg/320px-RhinopitecusBieti.jpg" });
            dt.Add(new Person { Name = "Sunday", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg" });
            dt.Add(new Person { Name = "Kalu", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/320px-Cuc.Phuong.Primate.Rehab.center.jpg" });
            dt.Add(new Person { Name = "Femi", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg" });
            dt.Add(new Person { Name = "Festus", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg" });
            People = dt;
            mPeople = dt;
        }
    }
}
