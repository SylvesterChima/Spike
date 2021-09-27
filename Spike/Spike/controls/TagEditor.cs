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
        public class AddMentionEventArgs : EventArgs
        {
            public string MentionText { get; set; }
        }
    }
}
