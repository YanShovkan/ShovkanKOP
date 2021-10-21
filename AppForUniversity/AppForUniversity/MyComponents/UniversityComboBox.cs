using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualComponents
{
    public partial class UniversityComboBox : UserControl
    {
        private event EventHandler selectedItemChanged;
        
        public event EventHandler SelectedItemChanged
        {
            add { selectedItemChanged += value; }
            remove { selectedItemChanged -= value; }
        }

        public string item
        {
            get
            {
                if(comboBox.SelectedItem != null)
                {
                    return comboBox.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null && comboBox.Items.Contains(value))
                {
                    comboBox.SelectedItem = value;
                }
            }
           
        }

        public UniversityComboBox()
        {
            InitializeComponent();
            comboBox.SelectedValueChanged += selectedItemChanged;
        }

        public void Clear()
        {
            comboBox.ResetText();
        }

        public void Fill(List<string> list)
        {
            Clear();
            comboBox.DataSource = list;
            comboBox.Refresh();
        }
    }
}
