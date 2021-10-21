
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using Laboratory2.HelperModels;
using System;

namespace Laboratory2
{
    /*
         Документ с 
        гистограммой. Формат 
        документа - Pdf


        У компонента должен быть публичный метод, 
        который должен принимать на вход имя файла (включая путь до 
        файла), название документа (заголовок в документе), заголовок 
        для диаграммы, указание расположения легенды для диаграммы 
        (создать для этого перечисление), набор данных для диаграммы 
        (название серии и данные для графика).


    Должна быть проверка 
    на заполненность входных данных значениями.
         */
    public class GistogramComponent
    {

        public void SaveGistogram(string nameOfFile, string nameOfDocument, string nameOfGistogram, 
            LocationLegend legend, GistogramInfo gistogramInfo) {

            Document document = new Document();
            Page page = new Page();
            document.Pages.Add(page);

            Chart chart = new Chart(0, 30, 500, 300);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Label label = new Label(nameOfDocument, 0, 0, 500, 30, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            Title histogramTitle = new Title(nameOfGistogram);
            chart.HeaderTitles.Add(histogramTitle);

            IndexedBarSeries barSeries = null;
            AutoGradient autogradient = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);

            foreach (var item in gistogramInfo.Data)
            {
                if (item.Value.Length < 1)
                {
                    throw new Exception("Input data is empty!");
                }
                barSeries = new IndexedBarSeries(item.Key);
                barSeries.Values.Add(item.Value);
                barSeries.Color = autogradient;
                plotArea.Series.Add(barSeries);
            }

            for (int i = 0; i < gistogramInfo.Data.Count; i++)
            {
                barSeries.YAxis.Labels.Add(new IndexedYAxisLabel((i + 1).ToString(), i));
            }

            chart.Legends.Placement = (LegendPlacement)legend;

            page.Elements.Add(chart);
            document.Draw(nameOfFile);

        }
    }
}
