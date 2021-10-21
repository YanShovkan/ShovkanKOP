using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;

namespace Library
{
    /**
     * 
     * Документ с 
таблицами. 
     * Формат 
документа - Pdf
     
    Не визуальный компонент для создания документа с таблицами. 

    * У компонента должен быть публичный метод, который должен 
принимать на вход имя файла (включая путь до файла), название 
документа (заголовок в документе) и набор таблиц (каждая 
представляет собой двумерный массив строк, где каждая строка –
ячейка таблицы документа). 
    
    Таблицы выделить границами. 
Ширина колонок одинакова у всех. Должна быть проверка на 
заполненность входных данных значениями.
Примеры создания документа в разных форматах (docx, xlsx, 
pdf).
     */
    public class FirstTableComponent
    {

        public void SaveTable(String direction, String documentName, List<string[,]> tables)
        {
            if (!IsFull(tables)) {
                throw new Exception("table have some empty mesh");
            }

            var document = new Document();

            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(documentName);
            paragraph.Format.SpaceAfter = 5;
            paragraph.Format.Alignment = ParagraphAlignment.Left;

            foreach (String[,] strings in tables) {
                var table = document.LastSection.AddTable();
                table.Borders.Width = 0.5;
                for (int i =0;i< strings.GetLength(1);i++) {
                    table.AddColumn();
                }

                for (int i=0;i<strings.GetLength(0);i++) {
                    var row = table.AddRow();
                    for (int j=0;j<table.Columns.Count;j++) {
                        row.Cells[j].AddParagraph(strings[i, j]);
                    }
                }
                Paragraph prg = section.AddParagraph();
                prg.Format.SpaceAfter = 5;
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true,
      PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(direction);
        }

        private Boolean IsFull(List<String[,]> tables) {
            foreach (String[,] table in tables) {
                foreach (String str in table) {
                    if (str == null) return false;
                }
            }
            return true;
        }
    }
}
