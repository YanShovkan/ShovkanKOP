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
    /**Поле для ввода
        целочисленного
        значения с
        возможностью
        указания значения 
        null


    Текстовый визуальный компонент для ввода значения 
определенного типа (TextBox), предусматривающий 
возможность пустого (не заполненного значения). - есть

    В контроле должен быть CheckBox, который при проставленной галочке 
будет обозначать признак незаполненного значения (null) и 
неактивным элементом ввода значения (TextBox). - есть

    При не проставленной галочке элемент ввода значения должен быть 
активным для заполнения. - есть

    Также требуется свойство для установки и получения введенного 
значения (set, get) (предусмотреть возможность возврата 
значения null). Если у CheckBox не проставлена галочка, а в 
элементе не введено значение, выдавать ошибку. Если введенное 
значение не соответствует требуемому типу, выдавать ошибку.


    */
    public partial class Component2 : UserControl
    {
        public int? Number
        {
            set
            {
                checkBox1.Checked = !value.HasValue;
                textBox1.Text = value.ToString();
            }
            get
            {
                if (checkBox1.Checked)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(textBox1.Text) && !checkBox1.Checked)
                {
                    return null;
                }
                else
                {
                    int? number = null;
                    try
                    {
                        number = Convert.ToInt32(textBox1.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return number;
                }
            }
        }

        public Component2()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
        }
    }
}
