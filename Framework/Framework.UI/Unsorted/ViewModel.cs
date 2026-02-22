using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ViewModel
    {
        public ViewModel()
        {
        }

        public ViewModel(int id, string text)
            : this()
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
