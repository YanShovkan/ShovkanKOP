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
using System.Reflection;

namespace VisualComponents
{
    public partial class UniversityListBox : UserControl
    {
        private string layoutString;
        private char openSymbol;
        private char endSymbol;

        public UniversityListBox()
        {
            InitializeComponent();
        }

        public int index
        {
            get { return listBox.SelectedIndex; }
            set { listBox.SelectedIndex = value; }
        }

        public void LayoutString(string layoutString, char openSymbol, char endSymbol)
        {
            this.layoutString = layoutString;
            this.openSymbol = openSymbol;
            this.endSymbol = endSymbol;
        }

        public void Fill<T>(List<T> list)
        {
            listBox.Items.Clear();

            List<string> names = GetStringNames();

            foreach (T obj in list)
            {
                listBox.Items.Add(FillString(obj, names));
            }

            listBox.Refresh();
        }

        public T GetItem<T>() where T : class, new()
        {
            T obj = new T();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            string[] layoutWordsArray = layoutString.Split(' ');
            string[] selectedWordsArray = listBox.SelectedItem.ToString().Split(' '); ;

            List<string> keyWords = new List<string>();
            List<string> names = GetStringNames();

            for (int i = 0; i < layoutWordsArray.Length; i++)
            {
                if (layoutWordsArray[i].Contains(openSymbol) && layoutWordsArray[i].Contains(endSymbol))
                {
                    if (i - 1 >= 0)
                    {
                        keyWords.Add(layoutWordsArray[i - 1]);
                    }
                    else
                    {
                        keyWords.Add(null);
                    }

                    if (i + 1 < layoutWordsArray.Length)
                    {
                        keyWords.Add(layoutWordsArray[i + 1]);
                    }
                    else
                    {
                        keyWords.Add(null);
                    }
                }
            }

            int namePosition = 0;
            foreach (string name in names)
            {
                PropertyInfo property = typeof(T).GetProperty(name);
                string value = null;
                for (int i = 0; i < selectedWordsArray.Length; i++)
                {
                    if (selectedWordsArray[i] == keyWords[namePosition * 2] || keyWords[namePosition * 2] == null)
                    {
                        i++;
                        while (i != selectedWordsArray.Length && selectedWordsArray[i] != keyWords[namePosition * 2 + 1])
                        {
                            value += selectedWordsArray[i] + " ";
                            i++;
                        }
                    }
                }

                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                namePosition++;
            }
            return obj;
        }

        private List<string> GetStringNames()
        {
            char[] layoutStringChars = layoutString.ToCharArray();
            List<string> names = new List<string>();

            for (int i = 0; i < layoutStringChars.Length; i++)
            {
                if (layoutStringChars[i] == openSymbol)
                {
                    string name = null;
                    int j = 0;
                    for (j = i + 1; layoutStringChars[j] != endSymbol; j++)
                    {
                        name += layoutStringChars[j];
                    }
                    i = j;
                    names.Add(name);
                }
            }
            return names;
        }

        private string FillString<T>(T obj, List<string> names)
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            char[] layoutStringChars = layoutString.ToCharArray();
            string finalString = null;

            int namePosition = 0;
            for (int i = 0; i < layoutStringChars.Length; i++)
            {
                if (layoutStringChars[i] == openSymbol)
                {
                    while (layoutStringChars[i] != endSymbol)
                    {
                        i++;
                    }

                    foreach (FieldInfo field in fields)
                    {
                        if (field.Name == names[namePosition])
                        {
                            finalString += field.GetValue(obj).ToString();
                        }
                    }

                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == names[namePosition])
                        {
                            finalString += property.GetValue(obj).ToString();
                        }
                    }

                    namePosition++;
                }
                else
                {
                    finalString += layoutStringChars[i];
                }
            }
            return finalString;
        }
    }
}
