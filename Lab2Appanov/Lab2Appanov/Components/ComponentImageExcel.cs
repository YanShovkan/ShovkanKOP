using System;
using System.ComponentModel;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab2Appanov.Components
{
    public partial class ComponentImageExcel : Component
    {
        public ComponentImageExcel()
        {
            InitializeComponent();
        }

        public ComponentImageExcel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateFile(string fileName, string name, string[] images)
        {
            if (fileName != null && name != null && images != null)
            {
                var misValue = System.Reflection.Missing.Value;
                var xlApp = new Excel.Application();
                var xlWorkBook = xlApp.Workbooks.Add(misValue);
                var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Name = name;

                int x = 0;
                int y = 0;
                float width;
                float height;

                foreach (var image in images)
                {
                    Image newImage = Image.FromFile(image);
                    width = newImage.Width;
                    height = newImage.Height;
                    xlWorkSheet.Shapes.AddPicture(image, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, x, y, width, height);
                    y += (int)height;
                }

                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlApp);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkSheet);
            }        
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
