using Spike.model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Spike.controls
{
    public class TagEditor: Editor
    {
        public TagEditor()
        {

        }

        #region AddMention
        public void AddMention(string text)
        {
            Console.WriteLine($"Adding mention {text}");

            AddMentionEventArgs args = new AddMentionEventArgs();
            args.MentionText = text;
            OnAddMention(args);
        }

        public event EventHandler<AddMentionEventArgs> MentionAdd;
        protected virtual void OnAddMention(AddMentionEventArgs e)
        {
            EventHandler<AddMentionEventArgs> handler = MentionAdd;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region UpdateMentionNames
        public void UpdateAddedNames(List<Person> addedNames)
        {
            UpdateAddedNamesEventArgs args = new UpdateAddedNamesEventArgs();
            args.AddedNames = addedNames;
            OnUpdateAddedNames(args);
        }

        public event EventHandler<UpdateAddedNamesEventArgs> UpdateMentionNames;
        protected virtual void OnUpdateAddedNames(UpdateAddedNamesEventArgs e)
        {
            EventHandler<UpdateAddedNamesEventArgs> handler = UpdateMentionNames;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Reformat
        public void Reformat()
        {
            EventArgs args = new EventArgs();
            OnReformatText(args);
        }

        public event EventHandler<EventArgs> ReformatText;
        protected virtual void OnReformatText(EventArgs e)
        {
            EventHandler<EventArgs> handler = ReformatText;
            if (handler != null)
            {
                handler(this, e);
            }
        } 
        #endregion

        public class AddMentionEventArgs : EventArgs
        {
            public string MentionText { get; set; }
        }

        public class UpdateAddedNamesEventArgs : EventArgs
        {
            public List<Person> AddedNames { get; set; }
        }
    }
}
