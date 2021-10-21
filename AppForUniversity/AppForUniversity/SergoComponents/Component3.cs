using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory1.Components
{

    /**
     *  Таблица значений.
     *  Таблица заполняется
     *  через метод, передающий
     *  список объектов
     *  
     *  
     *  Визуальный компонент для вывода списка в виде таблицы
        (DataGridView). - есть

    DataGridView настроен так, чтобы выбиралась 
    только одна строка и строка целиком, а не ячейка, - есть
    
    RowHeaders был скрыт. - есть

    Отдельный метод для конфигурации столбцов. Через 
метод указывается сколько колонок в DataGridView добавлять, 
их заголовки, ширину, признак видимости и имя свойства/поля
объекта класса, записи которого будут в таблице выводиться, 
значение из которого потребуется выводить в ячейке этой 
колонки. - есть
    
    Метод отчистки строк. - есть.
    
    Публичное свойство для установки и получения индекса выбранной строки (set, get). - есть

    Публичный параметризованный метод для получения объекта из 
выбранной строки (создать объект и через рефлексию заполнить 
свойства его). - есть

    Заполнение :
    через параметризованный метод, у которого в 
передаваемых параметрах идет список объектов какого-то 
класса и через этот список идет заполнение DataGridView; - есть

     *  
     *  
    */
    public partial class Component3 : UserControl
    {
        public int SelectedRow
        {
            set
            {
                if (dataGridView1.Rows.Count <= value)
                {
                    throw new Exception("there is not too many rows");
                }
                else if (value < 0)
                {
                    throw new Exception("index for selection must be above 0");
                }
                else
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[value].Selected = true;
                }
            }
            get
            {
                return dataGridView1.CurrentCell.RowIndex;
            }
        }

        public Component3()
        {
            InitializeComponent();
        }

        public void ConfigureViewbox(List<DataGridViewColumn> options)
        {
            foreach (DataGridViewColumn column in options)
            {
                DataGridViewColumn tmp = new DataGridViewTextBoxColumn();
                tmp.HeaderText = column.HeaderText;
                tmp.Width = column.Width;
                tmp.Visible = column.Visible;
                tmp.DataPropertyName = column.DataPropertyName;
                dataGridView1.Columns.Add(tmp);
            }
        }

        public void CleanStrings()
        {
            dataGridView1.DataSource=null;
        }

        public T GetSelectedObject<T>()
        {
            T tmp = (T)Activator.CreateInstance(typeof(T));
            var properties = typeof(T).GetProperties();
            foreach (var propertie in properties)
            {
                int columnIndex = 0;
                Boolean propIsFound = false;
                for (; columnIndex<dataGridView1.Columns.Count;columnIndex++) {
                    if (dataGridView1.Columns[columnIndex].DataPropertyName.ToString()==propertie.Name) {
                        propIsFound = true;
                        break;
                    }
                }
                if (!propIsFound) { throw new Exception("can not find propertie"); };

                object value = dataGridView1.SelectedRows[0].Cells[columnIndex].Value;
                propertie.SetValue(tmp, value);
            }
            return tmp;
        }

        public void FillDataGrid<T>(List<T> list)
        {
            dataGridView1.DataSource = new List<T>(list);
        }
    }
}
