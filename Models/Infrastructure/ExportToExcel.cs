using System.Data;
using OfficeExcel = Microsoft.Office.Interop.Excel;


namespace ProcsDLL.Models.Infrastructure
{
    public class ExportToExcel
    {

        public static void ExportDataSetToExcel(DataSet ds, string strPath)
        {
            int inHeaderLength = 0, inColumn = 0, inRow = 0;
            System.Reflection.Missing Default = System.Reflection.Missing.Value;


            OfficeExcel.Application excelApp = new OfficeExcel.Application();
            OfficeExcel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);
            foreach (DataTable dtbl in ds.Tables)
            {
                //Create Excel WorkSheet
                OfficeExcel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add(Default, excelWorkBook.Sheets[excelWorkBook.Sheets.Count], 1, Default);
                excelWorkSheet.Name = dtbl.TableName;//Name worksheet

                //Write Column Name
                for (int i = 0; i < dtbl.Columns.Count; i++)
                    excelWorkSheet.Cells[inHeaderLength + 1, i + 1] = dtbl.Columns[i].ColumnName.ToUpper();

                //Write Rows
                for (int m = 0; m < dtbl.Rows.Count; m++)
                {
                    for (int n = 0; n < dtbl.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = inHeaderLength + 2 + m;
                        excelWorkSheet.Cells[inRow, inColumn].EntireColumn.NumberFormat = "@";
                        excelWorkSheet.Cells[inRow, inColumn] = dtbl.Rows[m].ItemArray[n].ToString();
                        //if (m % 2 == 0)
                        //    excelWorkSheet.get_Range("A" + inRow.ToString(), "G" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FCE4D6");
                    }
                }

                //Excel Header
                //OfficeExcel.Range cellRang = excelWorkSheet.get_Range("A1", "G3");
                //cellRang.Merge(false);
                //cellRang.Interior.Color = System.Drawing.Color.White;
                //cellRang.Font.Color = System.Drawing.Color.Gray;
                //cellRang.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                //cellRang.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;
                //cellRang.Font.Size = 26;
                //excelWorkSheet.Cells[1, 1] = "Greate Novels Of All Time";

                //Style table column names
                //cellRang = excelWorkSheet.get_Range("A4", "G4");
                //cellRang.Font.Bold = true;
                //cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                //cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#ED7D31");
                //excelWorkSheet.get_Range("F4").EntireColumn.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignRight;
                //Formate price column
                //excelWorkSheet.get_Range("F5").EntireColumn.NumberFormat = "0.00";
                //Auto fit columns
                excelWorkSheet.Columns.AutoFit();
            }

            //Delete First Page
            excelApp.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
            lastWorkSheet.Delete();
            excelApp.DisplayAlerts = true;

            //Set Defualt Page
            (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();

            excelWorkBook.SaveAs(strPath, Default, Default, Default, false, Default, OfficeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            excelWorkBook.Close();
            excelApp.Quit();
        }
    }
}