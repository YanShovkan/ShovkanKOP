using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppForUniversity.Logic.BindingModels;
using AppForUniversity.Logic.BusinessLogic;
using AppForUniversity.Logic.ViewModels;

namespace AppForUniversity.Forms
{
    public partial class FormStudent : Form
    {
        public int Id { set { id = value; } }
        private readonly StudentLogic studentLogic = new StudentLogic();
        private readonly CourseLogic courseLogic = new CourseLogic();
        private int? id;

        public FormStudent()
        {
            InitializeComponent();
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(richTextBoxСharacteristic.Text))
            {
                MessageBox.Show("Введите характеристику", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(universityComboBoxCourse.item))
            {
                MessageBox.Show("Выберите курс", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                StudentBindingModel student = new StudentBindingModel
                {
                    Id = id,
                    FIO = textBoxFIO.Text,
                    Сharacteristic = richTextBoxСharacteristic.Text,
                    Course = universityComboBoxCourse.item,
                    Scholarship = componentScholarship.Number
                };
                if (student.Id.HasValue)
                {
                    studentLogic.Update(student);
                }
                else
                {
                    studentLogic.Create(student);
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            List<CourseViewModel> list = courseLogic.Read(null);
            List<String> listStr = new List<String>();
            foreach (var name in list)
            {
                listStr.Add(name.Name);
            }

            universityComboBoxCourse.Fill(listStr);

            if (id.HasValue)
            {
                try
                {
                    var view = studentLogic.Read(new StudentBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxFIO.Text = view.FIO;
                        richTextBoxСharacteristic.Text = view.Сharacteristic;
                        universityComboBoxCourse.item = view.Course;
                        componentScholarship.Number = view.Scholarship;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }

        }

    }
}
