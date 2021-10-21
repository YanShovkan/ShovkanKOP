using System;
using System.Collections.Generic;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab2Appanov.Components
{
    public partial class ComponentChartExcel : Component
    {
        public ComponentChartExcel()
        {
            InitializeComponent();
        }

        public ComponentChartExcel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateFile(string fileName, string name, string chartName, LegendLocation location, Dictionary<string, int> dictionary)
        {
            if (fileName != null && name != null && chartName != null && dictionary != null)
            {
                var misValue = System.Reflection.Missing.Value;
                var xlApp = new Excel.Application();
                var xlWorkBook = xlApp.Workbooks.Add(misValue);
                var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Name = name;
                xlWorkSheet.Cells[1, 1].Value = name;

                int startColumnCell = 1;
                int startRowCell = 2;

                int k = startRowCell;
                foreach (var keyValue in dictionary)
                {
                    xlWorkSheet.Cells[k, startColumnCell] = keyValue.Key;
                    xlWorkSheet.Cells[k, startColumnCell + 1] = keyValue.Value;
                    k++;
                }

                var range = xlWorkSheet.Range[xlWorkSheet.Cells[startRowCell, startColumnCell], xlWorkSheet.Cells[startRowCell + dictionary.Count - 1, startColumnCell + 1]];
                range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Columns.AutoFit();

                Excel.ChartObjects chartObjs = (Excel.ChartObjects)xlWorkSheet.ChartObjects();
                Excel.ChartObject chartObj = chartObjs.Add(200, 10, 500, 300);
                Excel.Chart xlChart = chartObj.Chart;
                xlChart.ChartType = Excel.XlChartType.xlPie;
                xlChart.HasTitle = true;
                xlChart.ChartTitle.Text = chartName;
                xlChart.HasLegend = true;

                switch (location)
                {
                    case LegendLocation.Up:
                        xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
                        break;
                    case LegendLocation.Down:
                        xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom;
                        break;
                    case LegendLocation.Left:
                        xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionLeft;
                        break;
                    case LegendLocation.Right:
                        xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight;
                        break;
                }

                xlChart.SetSourceData(range, Excel.XlRowCol.xlColumns);

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
