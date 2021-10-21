using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotVisualComponents
{
    public partial class UninversityWordDiagram : Component
    {
        public UninversityWordDiagram()
        {
            InitializeComponent();
        }

        public UninversityWordDiagram(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateDoc<T>(string filename, List<T> objects)
        {
            if (filename.Equals(""))
            {
                throw new Exception("Specify the filename!");
            }

            WordDocument document = new WordDocument();
            IWSection section = document.AddSection();

            IWParagraph paragraph = section.AddParagraph();

            WChart chart = paragraph.AppendChart(500, 300);
            chart.ChartType = OfficeChartType.Line;

            Type myType = typeof(T);
            PropertyInfo[] properties = myType.GetProperties();

            AddChartData(chart, properties, objects);
            chart.IsSeriesInRows = false;

            chart.ChartTitle = "Line Chart";

            int cnt = 0;
            for (int i = 0; i < properties.Length; i++)
            {
                if (!skippingProperties.Contains(properties[i].Name))
                {
                    IOfficeChartSerie series = chart.Series.Add(properties[i].Name);
                    series.Values = chart.ChartData[1, i + 1 - cnt, objects.Count + 1, i + 1 - cnt];
                    series.SerieType = OfficeChartType.Line;
                    series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                    continue;
                }
                cnt++;
            }

            chart.HasLegend = true;
            chart.Legend.Position = OfficeLegendPosition.Bottom;

            document.Save(filename);
            document.Close();

            System.Diagnostics.Process.Start(filename);
        }

        private void AddChartData<T>(WChart chart, PropertyInfo[] properties, List<T> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                int cnt = 0;
                for (int j = 0; j < properties.Length; j++)
                {
                    PropertyInfo currentProperty = properties.First(property => property.Name == properties[j].Name);
                    object value = currentProperty.GetValue(objects[i], null);
                    if (!(value is string))
                    {
                        chart.ChartData.SetValue(i + 1, j + 1 - cnt, value);
                        continue;
                    }
                    skippingProperties.Add(properties[j].Name);
                    cnt++;
                }
            }
        }

    }
}
