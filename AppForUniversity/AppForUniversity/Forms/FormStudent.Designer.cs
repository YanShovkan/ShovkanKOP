
namespace AppForUniversity.Forms
{
    partial class FormStudent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.labelFIO = new System.Windows.Forms.Label();
            this.labelСharacteristic = new System.Windows.Forms.Label();
            this.richTextBoxСharacteristic = new System.Windows.Forms.RichTextBox();
            this.universityComboBoxCourse = new VisualComponents.UniversityComboBox();
            this.labelCourse = new System.Windows.Forms.Label();
            this.labelScholarship = new System.Windows.Forms.Label();
            this.componentScholarship = new Laboratory1.Components.Component2();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(12, 25);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(142, 20);
            this.textBoxFIO.TabIndex = 0;
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Location = new System.Drawing.Point(12, 9);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(37, 13);
            this.labelFIO.TabIndex = 1;
            this.labelFIO.Text = "ФИО:";
            // 
            // labelСharacteristic
            // 
            this.labelСharacteristic.AutoSize = true;
            this.labelСharacteristic.Location = new System.Drawing.Point(160, 9);
            this.labelСharacteristic.Name = "labelСharacteristic";
            this.labelСharacteristic.Size = new System.Drawing.Size(136, 13);
            this.labelСharacteristic.TabIndex = 3;
            this.labelСharacteristic.Text = "Краткая характеристика:";
            // 
            // richTextBoxСharacteristic
            // 
            this.richTextBoxСharacteristic.Location = new System.Drawing.Point(160, 25);
            this.richTextBoxСharacteristic.Name = "richTextBoxСharacteristic";
            this.richTextBoxСharacteristic.Size = new System.Drawing.Size(164, 96);
            this.richTextBoxСharacteristic.TabIndex = 4;
            this.richTextBoxСharacteristic.Text = "";
            // 
            // universityComboBoxCourse
            // 
            this.universityComboBoxCourse.item = null;
            this.universityComboBoxCourse.Location = new System.Drawing.Point(330, 25);
            this.universityComboBoxCourse.Name = "universityComboBoxCourse";
            this.universityComboBoxCourse.Size = new System.Drawing.Size(128, 28);
            this.universityComboBoxCourse.TabIndex = 5;
            // 
            // labelCourse
            // 
            this.labelCourse.AutoSize = true;
            this.labelCourse.Location = new System.Drawing.Point(327, 9);
            this.labelCourse.Name = "labelCourse";
            this.labelCourse.Size = new System.Drawing.Size(34, 13);
            this.labelCourse.TabIndex = 6;
            this.labelCourse.Text = "Курс:";
            // 
            // labelScholarship
            // 
            this.labelScholarship.AutoSize = true;
            this.labelScholarship.Location = new System.Drawing.Point(477, 9);
            this.labelScholarship.Name = "labelScholarship";
            this.labelScholarship.Size = new System.Drawing.Size(64, 13);
            this.labelScholarship.TabIndex = 8;
            this.labelScholarship.Text = "Стипендия:";
            // 
            // componentScholarship
            // 
            this.componentScholarship.Location = new System.Drawing.Point(463, 25);
            this.componentScholarship.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.componentScholarship.Name = "componentScholarship";
            this.componentScholarship.Number = null;
            this.componentScholarship.Size = new System.Drawing.Size(241, 79);
            this.componentScholarship.TabIndex = 9;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 374);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(312, 64);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(476, 374);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(312, 64);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.componentScholarship);
            this.Controls.Add(this.labelScholarship);
            this.Controls.Add(this.labelCourse);
            this.Controls.Add(this.universityComboBoxCourse);
            this.Controls.Add(this.richTextBoxСharacteristic);
            this.Controls.Add(this.labelСharacteristic);
            this.Controls.Add(this.labelFIO);
            this.Controls.Add(this.textBoxFIO);
            this.Name = "FormStudent";
            this.Text = "Студенты";
            this.Load += new System.EventHandler(this.FormStudent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.Label labelFIO;
        private System.Windows.Forms.Label labelСharacteristic;
        private System.Windows.Forms.RichTextBox richTextBoxСharacteristic;
        private VisualComponents.UniversityComboBox universityComboBoxCourse;
        private System.Windows.Forms.Label labelCourse;
        private System.Windows.Forms.Label labelScholarship;
        private Laboratory1.Components.Component2 componentScholarship;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}