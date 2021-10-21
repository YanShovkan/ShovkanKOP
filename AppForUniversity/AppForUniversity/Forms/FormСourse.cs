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
    public partial class FormСourse : Form
    {
        private readonly CourseLogic logic = new CourseLogic();
        List<CourseViewModel> list;

        public FormСourse()
        {
            InitializeComponent();
        }


        public void LoadData()
        {
            try
            {
                list = logic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                if(dataGridView.Rows.Count == 0)
                {
                    list.Add(new CourseViewModel());
                    dataGridView.DataSource = new List<CourseViewModel>(list);
                    dataGridView.CurrentCell = dataGridView.Rows[0].Cells[1];
                    return;
                }
                if (dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[1].Value != null )
                {
                    list.Add(new CourseViewModel());
                    dataGridView.DataSource = new List<CourseViewModel>(list);
                    dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[1];
                    return;
                }
            }
            if (e.KeyData == Keys.Delete)
            {
                if (MessageBox.Show("Вы действительно хотите удалить?", "Предупреждение",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    logic.Delete(new CourseBindingModel() { Id = (int)dataGridView.CurrentRow.Cells[0].Value });
                    list = logic.Read(null);
                    dataGridView.DataSource = new List<CourseViewModel>(list);
                }

            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(((DataGridView)sender).CurrentCell.EditedFormattedValue.ToString()))
            {

                if (dataGridView.CurrentRow.Cells[0].Value != null)
                {
                    var listDBM = logic.Read(new CourseBindingModel() { Id = (int)dataGridView.CurrentRow.Cells[0].Value });
                    dataGridView.CurrentRow.Cells[1].Value = ((CourseViewModel)listDBM[0]).Name;
                }

            }
            else
            {
                if (dataGridView.CurrentRow.Cells[0].Value != null)
                {
                    logic.Update(new CourseBindingModel()
                    {
                        Id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value),
                        Name = (string)dataGridView.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
                else
                {
                    logic.Create(new CourseBindingModel()
                    {
                        Name = (string)dataGridView.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
            }

            LoadData();
        }

        private void FormСourse_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
