using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualComponents
{
    public partial class UniversityTextBox : UserControl
    {
        private string _sample = string.Empty;

        public string value
        {
            get
            {
                if (Regex.IsMatch(textBox.Text, _sample))
                {
                    return textBox.Text;
                }
                return null;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public string sample
        {
            get { return _sample; }
            set { _sample = value; }
        }

        public string example
        {
            get { return toolTip.GetToolTip(textBox); }
            set { toolTip.SetToolTip(textBox, value);}
        }

        public UniversityTextBox()
        {
            InitializeComponent();
        }
    }
}
