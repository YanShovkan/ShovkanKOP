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
	public partial class InputControl : UserControl
	{
		public InputControl()
		{
			InitializeComponent();
		}

		public DateTime? FirstDate { get; set; }
		public DateTime? LastDate { get; set; }
		private bool first = true;

		public DateTime value
		{
			set
			{
				if (first)
				{
					first = false;
					return;
				}
				if (!FirstDate.HasValue || !LastDate.HasValue)
                {
					throw new Exception();
				}
				if(value < FirstDate || value > LastDate || FirstDate == LastDate)
				{
					throw new Exception();
				}
				dateTimePicker1.Value = value;
			}

			get
			{
				if (first)
				{
					first = false;
					return dateTimePicker1.Value;
				}
				if (!FirstDate.HasValue || !LastDate.HasValue)
				{
					throw new Exception();
				}
				if (value < FirstDate || value > LastDate || FirstDate == LastDate)
				{
					throw new Exception();
				}
				return dateTimePicker1.Value;
			}
		}
	}
}
