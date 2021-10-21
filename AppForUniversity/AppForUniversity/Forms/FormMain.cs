using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppForUniversity.Logic.BusinessLogic;
using AppForUniversity.Logic.ViewModels;
using AppForUniversity.Logic.BindingModels;
using Library;
using Lab2Appanov.Components;
using NotVisualComponents;
using Library.HelperModels;

namespace AppForUniversity.Forms
{
    public partial class FormMain : Form
    {
        private readonly StudentLogic studentLogic = new StudentLogic();

        public FormMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<StudentViewModel> list = studentLogic.Read(null);

                Queue<string> queue = new Queue<string>();
                queue.Enqueue("Course");
                queue.Enqueue("Scholarship");
                queue.Enqueue("Id");
                queue.Enqueue("FIO");

                outputControl.FillHierarchy<StudentViewModel>(queue);
                outputControl.FillTree<StudentViewModel>(list);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void CreateStudent()
        {
            try
            {
                FormStudent form = new FormStudent();
                form.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void DeleteStudent()
        {
            if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    StudentViewModel student = outputControl.GetSelectedEntry<StudentViewModel>();
                    studentLogic.Delete(new StudentBindingModel { Id = student.Id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void EditStudent()
        {
            try
            {
                StudentViewModel student = outputControl.GetSelectedEntry<StudentViewModel>();
                FormStudent form = new FormStudent();
                form.Id = (student.Id);
                form.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void outputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Control.ModifierKeys & Keys.Control) == Keys.Control)
        && e.KeyValue == 'A')
            {
                CreateStudent();
            }
            if (e.KeyCode == Keys.U && e.Control)
            {
                EditStudent();
            }
            if (e.KeyCode == Keys.D && e.Control)
            {
                DeleteStudent();
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                UniversityWordDocument document = new UniversityWordDocument();
                List<String> strList = new List<string>();
                List<StudentViewModel> list = studentLogic.Read(new StudentBindingModel { });

                foreach(StudentViewModel student in list)
                {
                    if(student.Scholarship != 0)
                    {
                        strList.Add(student.FIO + " - " + student.Сharacteristic);
                    }
                }

                using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        document.CreateDoc(dialog.FileName, "Отчет по студентам", strList.ToArray());
                    }
                }
            }
            if (e.KeyCode == Keys.T && e.Control)
            {
                SecondTableComponent table = new SecondTableComponent();
                List<StudentViewModel> list = studentLogic.Read(null);

                List<TableColumnHelper> columnHelpers = new List<TableColumnHelper>();
                columnHelpers.Add(new TableColumnHelper { Name = "", PropertyName = "Id" });
                columnHelpers.Add(new TableColumnHelper { Name = "ФИО", PropertyName = "FIO" });
                columnHelpers.Add(new TableColumnHelper { Name = "Характеристика", PropertyName = "Сharacteristic" });
                columnHelpers.Add(new TableColumnHelper { Name = "Стипендия", PropertyName = "Scholarship" });
                columnHelpers.Add(new TableColumnHelper { Name = "Курс", PropertyName = "Course" });

                TableRowHelper[] rowHelpers = new TableRowHelper[2];
                rowHelpers[0] = new TableRowHelper() { Height = 50 };
                rowHelpers[1] = new TableRowHelper() { Height = 50 };

                using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        table.SaveTable(dialog.FileName, "Отчет по студентам", columnHelpers, rowHelpers, list);
                    }
                }
            }
            if (((Control.ModifierKeys & Keys.Control) == Keys.Control)
        && e.KeyValue == 'C')
            {
                ComponentChartExcel gistagram = new ComponentChartExcel();
                Dictionary<string, int> dictionary  = new Dictionary<string, int>();
                List<StudentViewModel> list = studentLogic.Read(new StudentBindingModel { });

                foreach (StudentViewModel student in list)
                {
                    if (student.Scholarship != 0)
                    {
                        if (dictionary.ContainsKey(student.Course))
                        {
                            dictionary[student.Course]++;
                        }
                        else
                        {
                            dictionary.Add(student.Course, 1);
                        }
                    }
                }

                using (var dialog = new SaveFileDialog { Filter = "xls|*.xls" })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        gistagram.CreateFile(dialog.FileName, "Отчет по студентам", "Диаграмма по студентам", LegendLocation.Up, dictionary);
                    }
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStudent();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditStudent();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void справочникКурсовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormСourse form = new FormСourse();
            form.ShowDialog();
            LoadData();
        }

    }
}
