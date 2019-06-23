using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Tools;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dima.Models
{
    public class EngineerReportModel
    {
        private static string[] Headers = new[] { "Назва замовлення", "Стан", "Код компанії", "Приблизний час виконання" };
        public void Report(int tab_Number, string engname)
        {
            var reqs = PostgresService.Instance.RequestsByEngineer(tab_Number);
            CreateDocument(reqs, engname);
        }

        public void CreateDocument(IEnumerable<Request> report, string engnam)
        {
            try
            {
                System.Threading.Tasks.Task.Run(() => CreateDocumentInternal(report, engnam));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CreateDocumentInternal(IEnumerable<Request> report,string engnam)
        {
            var reportItemsList = report.ToList();
            var winword = new Microsoft.Office.Interop.Word.Application();
            winword.ShowAnimation = false;

            object missing = System.Reflection.Missing.Value;
            winword.Visible = false;

            var document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            foreach (Section section in document.Sections)
            {
                Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
                headerRange.Font.Size = 25;
                headerRange.Text = $"Звіт за замовленнями інженера {engnam}";
            }

            var para1 = document.Content.Paragraphs.Add(ref missing);
            para1.Range.InsertParagraphAfter();
            para1.Range.Text = "!!!!!!!!!!!!!!!!";

            Table firstTable = document.Tables.Add(para1.Range, reportItemsList.Count + 1, 4, ref missing, ref missing);

            firstTable.Borders.Enable = 1;
            foreach (Row row in firstTable.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    //Header row
                    if (cell.RowIndex == 1)
                    {
                        cell.Range.Text = Headers[cell.ColumnIndex - 1];
                        cell.Range.Font.Bold = 1;
                        //other format properties goes here
                        cell.Range.Font.Name = "verdana";
                        cell.Range.Font.Size = 10;
                        //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
                        cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                        //Center alignment for the Header cells
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    }
                    //Data row
                    else
                    {
                        var currItem = reportItemsList[cell.RowIndex - 2];
                        string text = null;
                        switch (cell.ColumnIndex)
                        {
                            case 1:
                                text = currItem.Request_Name.ToString();
                                break;
                            case 2:
                                text = currItem.StateReq.GetDescription();
                                break;
                            case 3:
                                text = currItem.Code_edrpou.ToString();
                                break;
                            case 4:
                                text = currItem.Approximate_Duration.ToString();
                                break;
                        }
                        cell.Range.Text = text;
                    }
                }
            }

            //Save the document
            document.Activate();
            try
            {
                winword.Visible = true;
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

            }
            winword.Quit(ref missing, ref missing, ref missing);
        }

    }
}
