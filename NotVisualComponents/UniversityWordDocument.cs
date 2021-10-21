using NotVisualComponents.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.ComponentModel;
using System;

/*
	Не визуальный компонент для создания документа с большим текстом.
	У компонента должен быть публичный метод, который должен принимать на вход имя файла (включая путь до файла),
	название документа (заголовок в документе) и массив строк (каждая строка – абзац текста в выходном документе или текст в ячейке для табличного документа).
	Должна быть проверка на заполненность входных данных значениями.
*/
namespace NotVisualComponents
{
    public partial class UniversityWordDocument : Component
    {
        public UniversityWordDocument()
        {
            InitializeComponent();
        }

        public UniversityWordDocument(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateDoc(string parth, string title, string[] enterStrings)
        {
            if (!string.IsNullOrEmpty(parth) || !string.IsNullOrEmpty(title) || enterStrings.Length > 0)
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(parth, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body docBody = mainPart.Document.AppendChild(new Body());
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { (title, new WordTextProperties { Bold = true, Size = "48", }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    foreach (var oneString in enterStrings)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<(string, WordTextProperties)> { (oneString, new WordTextProperties { Size = "48", Bold = false }) },
                            TextProperties = new WordTextProperties
                            {
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                    }
                    docBody.AppendChild(CreateSectionProperties());
                    wordDocument.MainDocumentPart.Document.Save();
                }
            }
            else
            {
                new Exception("Проверьте входные данные");
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

