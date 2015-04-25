using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        private readonly string[] _headerText = { "From", "To", "Distance", "Transport Mode" };
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            var app = new Application { Visible = false, DisplayAlerts = false };
            var workBook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            var sheet = (Worksheet)workBook.Worksheets[1];

            SetHeaders(sheet);
            SetCities(sheet, from, to, links);

            workBook.SaveAs(fileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);

            workBook.Close();
        }


        private void SetCities(Worksheet sheet, City start, City end, List<Link> linkList)
        {
            var startPositionBody = 2;

            foreach (var currentLink in linkList)
            {
                Range bodyFrom = sheet.Cells[startPositionBody, 1];
                Range bodyTo = sheet.Cells[startPositionBody, 2];
                Range bodyDisntance = sheet.Cells[startPositionBody, 3];
                Range bodyMode = sheet.Cells[startPositionBody, 4];

                bodyFrom.Value2 = String.Format("{0}({1})", currentLink.FromCity.Name, currentLink.FromCity.Country);
                bodyTo.Value2 = String.Format("{0}({1})", currentLink.ToCity.Name, currentLink.ToCity.Country);
                bodyDisntance.Value2 = currentLink.Distance;
                bodyMode.Value2 = Enum.GetName(typeof(TransportModes), currentLink.TransportMode);

                startPositionBody++;
            }
        }

        private void SetHeaders(Worksheet sheet)
        {
            for (int i = 1; i <= _headerText.Length; i++)
            {
                Range header = sheet.Cells[1, i];
                header.Value2 = _headerText[i - 1];
                header.Font.Size = 14;
                header.Font.Bold = true;
                var border = header.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;
                border.Weight = 2d;
            }
        }
    }
}
