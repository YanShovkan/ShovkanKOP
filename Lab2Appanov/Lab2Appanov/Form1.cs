using Lab2Appanov.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2Appanov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] images = { "D:\\pic3.jpg", "D:\\pic4.jpg" };

            ComponentImageExcel componentImageExcel = new ComponentImageExcel();
            componentImageExcel.CreateFile("D:\\aaaa.xls", "Заголовок", images);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, int[]>();
            var array = new int[] { 2, 3 };
            var arrayHeader = new string[] {"Имя", "Возраст", "Должность", "Зарплата", "Номер телефона" };
            var arrayHeight = new int[] { 20, 30, 20, 20, 40 };
            var listHuman = new List<Human>() { new Human("Резников", 24, "Программист", 20000, "435632"),
                                                new Human("Аппанов", 24, "Директор", 232230000, "12121221"),
                                                new Human("Шовкань", 24, "Юрист", 30000, "34234234234"),
                                                new Human("Буткеев", 24, "Менеджер", 20000, "579786986")};
            dict.Add("Работа", array);
            ComponentTableExcel componentTableExcel = new ComponentTableExcel();
            componentTableExcel.CreateFile<Human>("D:\\bbb.xls", "Заголовок", dict, arrayHeight, arrayHeader, listHuman);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, int>();
            dict.Add("Аппанов", 40000);
            dict.Add("Шовкань", 10000);
            dict.Add("Резников", 15000);
            dict.Add("Буткеев", 10000);
            dict.Add("Альберт Хасанов", 100000);
            ComponentChartExcel componentChartExcel = new ComponentChartExcel();
            componentChartExcel.CreateFile("D:\\cccc.xls", "Заголовок", "Зарплаты", LegendLocation.Up, dict);
        }
    }
}