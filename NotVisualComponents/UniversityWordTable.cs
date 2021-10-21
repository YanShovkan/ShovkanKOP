using NotVisualComponents.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Reflection;

namespace NotVisualComponents
{
    public partial class UniversityWordTable : Component
    {
        public UniversityWordTable()
        {
            InitializeComponent();
        }

        public UniversityWordTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateDoc<T>(string parth, string title, List<T> enterData, List<int[]> consolidatedСolumns, Dictionary<string, int> columnNamesAndSize, List<string> columnTitles)
        {

            foreach (int[] firstOneGroupOfConsolidatedСolumns in consolidatedСolumns)
            {
                foreach (int firstColumnIndex in firstOneGroupOfConsolidatedСolumns)
                {
                    foreach (int[] secondOneGroupOfConsolidatedСolumns in consolidatedСolumns)
                    {
                        if (firstOneGroupOfConsolidatedСolumns != secondOneGroupOfConsolidatedСolumns)
                        {
                            foreach (int secondColumnIndex in secondOneGroupOfConsolidatedСolumns)
                            {
                                if (firstColumnIndex == secondColumnIndex)
                                {
                                    throw new Exception("Error");
                                }
                            }
                        }
                    }
                }
            }

            foreach (string columnName in columnNamesAndSize.Keys)
            {
                int propertyIndex = 0;
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (property.Name == columnName)
                    {
                        break;
                    }
                    if (property.Name != columnName && typeof(T).GetProperties().Length - 1 == propertyIndex)
                    {
                        throw new Exception("Error");
                    }
                    propertyIndex++;
                }
            }

            int consolidatedСolumnsCount = 0;
            foreach (int[] consolidatedСolumnGroup in consolidatedСolumns)
            {
                consolidatedСolumnsCount += consolidatedСolumnGroup.Length;
            }
            if (columnTitles.Count != ((columnNamesAndSize.Count - consolidatedСolumnsCount) + consolidatedСolumns.Count  + consolidatedСolumnsCount))
            {
                throw new Exception("Error");
            }

            if (parth == null || title == null || enterData == null || consolidatedСolumns == null || columnNamesAndSize == null || columnTitles == null)
            {
                throw new Exception("Error");
            }
            else
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(parth, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body docBody = mainPart.Document.AppendChild(new Body());
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { (title, new WordTextProperties { Bold = true, Size = "48" }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));

                    Table table = new Table();

                    TableProperties tblProp = new TableProperties(
                        new TableBorders(new TopBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 },
                            new BottomBorder()
                            { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 },
                            new LeftBorder()
                            { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 },
                            new RightBorder()
                            { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 },
                            new InsideHorizontalBorder()
                            { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 },
                            new InsideVerticalBorder()
                            { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 24 })
                    );

                    table.Append(tblProp);

                    foreach (int[] oneGroupOfConsolidatedСolumns in consolidatedСolumns)
                    {
                        for (int i = 0; i < oneGroupOfConsolidatedСolumns.Length; i++)
                        {
                            for (int j = 0; j < oneGroupOfConsolidatedСolumns.Length - 1; j++)
                            {
                                if (oneGroupOfConsolidatedСolumns[j] > oneGroupOfConsolidatedСolumns[j + 1])
                                {
                                    int temp = oneGroupOfConsolidatedСolumns[j];
                                    oneGroupOfConsolidatedСolumns[j] = oneGroupOfConsolidatedСolumns[j + 1];
                                    oneGroupOfConsolidatedСolumns[j + 1] = temp;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < consolidatedСolumns.Count; i++)
                    {
                        for (int j = 0; j < consolidatedСolumns.Count - 1; j++)
                        {
                            if (consolidatedСolumns[j][0] > consolidatedСolumns[j + 1][0])
                            {
                                int[] temp = consolidatedСolumns[j];
                                consolidatedСolumns[j] = consolidatedСolumns[j + 1];
                                consolidatedСolumns[j + 1] = temp;
                            }
                        }
                    }

                    TableRow firstTittleRow = new TableRow();
                    TableRow secondTittleRow = new TableRow();

                    int consolidatedСolumnsGroupIndex = 0;
                    Queue<string> columnTitlesQueue = new Queue<string>(columnTitles);

                    for (int i = 0; i < columnNamesAndSize.Count; i++)
                    {
                        if (consolidatedСolumnsGroupIndex < consolidatedСolumns.Count && i == consolidatedСolumns[consolidatedСolumnsGroupIndex][0])
                        {
                            TableCell firstCell = new TableCell();
                            TableCell secondCell = new TableCell();

                            firstCell.Append(
                                new TableCellProperties(new HorizontalMerge() { Val = MergedCellValues.Restart }),
                                new Paragraph(new Run(new Text(columnTitlesQueue.Dequeue())))
                                );

                            secondCell.Append(
                               new TableCellProperties(new HorizontalMerge() { Val = MergedCellValues.Restart }),
                               new Paragraph(new Run(new Text(columnTitlesQueue.Dequeue())))
                               );

                            firstTittleRow.Append(firstCell);
                            secondTittleRow.Append(secondCell);

                            for (int j = 0; j < consolidatedСolumns[consolidatedСolumnsGroupIndex].Length - 1; j++)
                            {
                                TableCell nextFirstCell = new TableCell();
                                TableCell nextSecondCell = new TableCell();

                                nextFirstCell.Append(
                                    new TableCellProperties(new HorizontalMerge() { Val = MergedCellValues.Continue }),
                                    new Paragraph(new Run(new Text(null)))
                                    );

                                nextSecondCell.Append(
                                   new TableCellProperties(new HorizontalMerge() { Val = MergedCellValues.Restart }),
                                   new Paragraph(new Run(new Text(columnTitlesQueue.Dequeue())))
                                   );

                                firstTittleRow.Append(nextFirstCell);
                                secondTittleRow.Append(nextSecondCell);
                            }

                            i += consolidatedСolumns[consolidatedСolumnsGroupIndex].Length - 1;
                            consolidatedСolumnsGroupIndex++;
                        }
                        else
                        {
                            TableCell firstCell = new TableCell();
                            TableCell secondCell = new TableCell();

                            firstCell.Append(
                                new TableCellProperties(new VerticalMerge() { Val = MergedCellValues.Restart }),
                                new Paragraph(new Run(new Text(columnTitlesQueue.Dequeue())))
                                );

                            secondCell.Append(
                               new TableCellProperties(new VerticalMerge() { Val = MergedCellValues.Continue }),
                               new Paragraph(new Run(new Text(null)))
                               );

                            firstTittleRow.Append(firstCell);
                            secondTittleRow.Append(secondCell);
                        }
                    }

                    table.Append(firstTittleRow, secondTittleRow);

                    foreach (T data in enterData)
                    {
                        TableRow tableRow = new TableRow();
                        foreach (string columnName in columnNamesAndSize.Keys)
                        {
                            TableCell tableCell = new TableCell();

                            foreach (PropertyInfo property in typeof(T).GetProperties())
                            {
                                if (property.Name.Equals(columnName))
                                {
                                    tableCell.Append(
                                        new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = columnNamesAndSize[columnName].ToString() }),
                                        new Paragraph(new Run(new Text(property.GetValue(data).ToString())))
                                        );
                                    break;
                                }
                            }
                            tableRow.Append(tableCell);
                        }
                        table.Append(tableRow);
                    }
                    docBody.Append(table);
                }
            }
        }

        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space =
                   SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }

            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new
               ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val =
                   paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
