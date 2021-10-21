using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.OfficeChart;

namespace NotVisualComponents
{
    public partial class UniversityWordGraphic : Component
    {
        public UniversityWordGraphic()
        {
            InitializeComponent();
        }

        public void CreateDoc(string filename, string title, string legend, Dictionary<string, int[]> objects, LegendPosition legendPosition)
        {
            if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(title))
            {
                WordDocument document = new WordDocument();
                IWSection section = document.AddSection();

                IWParagraph paragraph = section.AddParagraph();

                WChart chart = paragraph.AppendChart(500, 300);
                chart.ChartType = OfficeChartType.Line;

                chart.IsSeriesInRows = false;

                chart.ChartTitle = title;

                for (int i = 0; i < objects.Count; i++)
                {
                    IOfficeChartSerie series = chart.Series.Add(objects.ElementAt(i).Key);
                    series.Values = chart.ChartData[1, i + 1, objects.ElementAt(i).Value.Length + 1, i + 1];
                    series.SerieType = OfficeChartType.Line;
                    series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                }


                for (int i = 0; i < objects.Count; i++)
                {
                    for (int j = 0; j < objects.ElementAt(i).Value.Length; j++)
                    {
                        chart.ChartData.SetValue(j + 1, i + 1,  objects.ElementAt(i).Value[j]);
                    }
                }

                chart.HasLegend = true;

                switch (legendPosition)
                {
                    case LegendPosition.Top:
                        chart.Legend.Position = OfficeLegendPosition.Top;
                        break;
                    case LegendPosition.Bottom:
                        chart.Legend.Position = OfficeLegendPosition.Bottom;
                        break;
                    case LegendPosition.Left:
                        chart.Legend.Position = OfficeLegendPosition.Left;
                        break;
                    case LegendPosition.Right:
                        chart.Legend.Position = OfficeLegendPosition.Right;
                        break;
                }

                document.Save(filename);
                document.Close();

                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                throw new Exception("Неверные данные!");
            }
        }
    }
}
