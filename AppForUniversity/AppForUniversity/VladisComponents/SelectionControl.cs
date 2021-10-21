using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1Appanov.Components
{
	public partial class SelectionControl : UserControl
	{
		/*
			Список с выбором.
			Список заполняется
			через метод,
			передающий
			список строк 
		 */
		private event EventHandler valueChanged;
		public event EventHandler ValueChanged {
			add { valueChanged += value; }
			remove { valueChanged -= value; }
		}
		public SelectionControl()
		{
			InitializeComponent();
			checkedListBox1.SelectedValueChanged += valueChanged;
		}

		public void FillList(List<string> List)
		{
			ClearCheckedList();
			checkedListBox1.Items.AddRange(List.ToArray());
			checkedListBox1.Refresh();
		} 

		public void ClearCheckedList()
		{
			checkedListBox1.Items.Clear();
		}

		public string value
		{
			set
			{
				checkedListBox1.SelectedItem = value;
			}

			get
			{
				if (checkedListBox1.Items == null)
				{
					return null;
				}
				var selectedItems = string.Join(",",
	checkedListBox1.CheckedItems.Cast<string>()
		.Select(x => checkedListBox1.GetItemText(x)));
				return selectedItems;
			}
		}

		private void SelectionControl_VisibleChanged(object sender, EventArgs e)
		{

		}
	}
}
