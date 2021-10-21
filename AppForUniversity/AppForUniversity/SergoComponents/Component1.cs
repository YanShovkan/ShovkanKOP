using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory1.Components
{
    /**Список значений.
        Список заполняется
        через метод,
        передающий
        список строк

        Заполнить список можно через
        метод FillList (List<String> list)
        и списка строк
    */
    public partial class Component1 : UserControl
    {
        public string ChoosenLine { 
            set {
                int index = listBox1.Items.IndexOf(value);
                if (index != -1)
                {
                    listBox1.SetSelected(index, true);
                }
                else {
                    listBox1.ClearSelected();
                }
            } 
            get {
                if (listBox1.SelectedIndex != -1)
                {
                    return (String)listBox1.SelectedItem;
                }
                else { 
                    return String.Empty; 
                }
            } 
        }

        private event EventHandler _notify;
        public event EventHandler Notify { 
            add { _notify += value; }
            remove { _notify -= value; } 
        }

        public Component1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() {
            listBox1.SelectedValueChanged+=_notify;
        }

        public void FillList(List<String> strs) {
            foreach (var str in strs) {
                listBox1.Items.Add(str);
            }
        }

        public void CleanList() {
            listBox1.Items.Clear();
        }
    }
}
