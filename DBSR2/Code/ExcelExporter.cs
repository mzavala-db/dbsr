using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Drawing;
using System.Configuration;
using OfficeOpenXml;

namespace DBSR2
{
    public class ExcelExporter
    {
        #region Properties
        public DataTable DataSource
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public List<string> Subtitles
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public ExcelExporter()
        {
        }

        public ExcelExporter(DataTable dataTable)
        {
            DataSource = dataTable;
        }

        public ExcelExporter(DataTable dataTable, string title)
        {
            DataSource = dataTable;
            Title = title;
        }

        public ExcelExporter(DataTable dataTable, string title, List<string> subtitles)
        {
            DataSource = dataTable;
            Title = title;
            Subtitles = subtitles;
        }
        #endregion


        public void Export(string fileName)
        {
            if (DataSource == null)
                throw new ArgumentNullException("La propiedad DataSource no se ha definido");

            var file = new FileInfo(Path.Combine(HttpContext.Current.Server.MapPath("~/files"), fileName));
            
            int rowNumber = 1;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Horas");

                if (Title != null)
                {
                    ws.Cells[rowNumber, 1].Value = Title;
                    ws.Cells[rowNumber, 1].Style.Font.Bold = true;
                    ws.Cells[rowNumber, 1].Style.Font.Color.SetColor(Color.OrangeRed);
                    ws.Cells[rowNumber, 1].Style.Font.Size = 14;

                    rowNumber += 2;
                }

                if (Subtitles != null)
                {
                    foreach (string subtitle in Subtitles)
                    {
                        ws.Cells[rowNumber, 1].Value = subtitle;
                        ws.Cells[rowNumber, 1].Style.Font.Bold = true;
                        rowNumber++;
                    }
                    rowNumber++;
                }

                // los titulos salen de los nombres de las columnas
                int i = 1;
                foreach (DataColumn col in DataSource.Columns)
                {
                    ws.Cells[rowNumber, i].Value = col.ColumnName;
                    ws.Cells[rowNumber, i].Style.Font.Bold = true;
                    ws.Cells[rowNumber, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[rowNumber, i].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[rowNumber, i].Style.Font.Color.SetColor(Color.White);
                    i++;
                }
                ws.Cells[rowNumber, 1, rowNumber, i - 1].AutoFilter = true;

                // ahora se exportan los datos
                i = 1;
                rowNumber++;
                foreach (DataRow row in DataSource.Rows)
                {
                    i = 1;
                    foreach (DataColumn col in DataSource.Columns)
                    {
                        ws.Cells[rowNumber, i++].Value = row[col];
                    }
                    rowNumber++;
                }

                ws.Cells.AutoFitColumns(0);

                package.Save();
            }
        }
    }
}