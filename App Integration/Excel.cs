using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace App_Integration
{
    public class Excel
    {
        public void CreateWorkbook(string workbookName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(XLEventTracking.Disabled);
            wb.Worksheets.Add("Sheet1");
            wb.SaveAs(workbookName);
        }

        public void CreateWorkbook(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(XLEventTracking.Disabled);
            wb.Worksheets.Add(sheetName);
            wb.SaveAs(workbookName);
        }

        public void CreateWorkbook(string workbookName, string[] sheetNames)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            using (var wb = new XLWorkbook())
            {
                // Ensure array worksheet names are unique before adding
                foreach (var sheetName in sheetNames.Distinct().ToArray())
                    wb.Worksheets.Add(sheetName);

                wb.SaveAs(workbookName);
            }
        }

        public void AddWorksheet(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheets.Add(sheetName);
            wb.Save();
        }

        public void AddWorksheet(string workbookName, string[] sheetNames)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            using (var wb = new XLWorkbook(workbookName))
            {
                // Ensure array worksheet names are unique before adding
                foreach (var sheetName in sheetNames.Distinct().ToArray())
                    wb.Worksheets.Add(sheetName);

                wb.Save();
            }
        }

        public void RemoveWorksheet(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheet(sheetName).Delete();
            wb.Save();
        }

        public void MoveWorkSheetByName(string workbookName, string sheetName, int newPos)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheet(sheetName).Position = newPos;
            wb.Save();
        }

        public void MoveWorkSheetByPosition(string workbookName, int pos, int newPos)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheet(pos).Position = newPos;
            wb.Save();
        }

        public void RenameWorksheet(string workbookName, string sheetName, string newSheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheet(sheetName).Name = newSheetName;
            wb.Save();
        }

        public void CopyWorksheetToAnotherWorkbook(string sourceWorkbookName, string sourceSheetName, string targetWorkbook, string targetSheetName)
        {
            if (String.IsNullOrEmpty(sourceWorkbookName)) throw new ArgumentException(nameof(sourceWorkbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(targetWorkbook)) throw new ArgumentException(nameof(targetWorkbook) + " cannot be null or empty.");

            var wbSource = new XLWorkbook(sourceWorkbookName, XLEventTracking.Disabled);
            var wsSource = wbSource.Worksheet(sourceSheetName);
            var wbTarget = new XLWorkbook(targetWorkbook, XLEventTracking.Disabled);
            wsSource.CopyTo(wbTarget, targetSheetName);
            wbTarget.Save();
        }

        public void CopyWorksheetToNewWorkbook(string sourceWorkbookName, string sourceSheetName, string targetWorkbook, string targetSheetName)
        {
            if (String.IsNullOrEmpty(sourceWorkbookName)) throw new ArgumentException(nameof(sourceWorkbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(targetWorkbook)) throw new ArgumentException(nameof(targetWorkbook) + " cannot be null or empty.");

            var wbSource = new XLWorkbook(sourceWorkbookName, XLEventTracking.Disabled);
            var wsSource = wbSource.Worksheet(sourceSheetName);
            var wbTarget = new XLWorkbook(XLEventTracking.Disabled);
            wsSource.CopyTo(wbTarget, targetSheetName);
            wbTarget.SaveAs(targetWorkbook);
        }

        public void ChangeWorksheetVisibilty(string workbookName, string sheetName, string sheetVisibiltyOption)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            Enum.TryParse(sheetVisibiltyOption, out XLWorksheetVisibility sheetVisibilty);
            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            wb.Worksheet(sheetName).Visibility = sheetVisibilty;
            wb.Save();
        }

        public string FindCellsInARangeByExactValue(string workbookName, string sheetName, string range, string value)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            string result = "";

            IXLRange rangeFound = ws.Range(range);

            IXLCells cellsFound = rangeFound.CellsUsed(cell => cell.GetValue<string>() == value);

            result = String.Join(";", from a in cellsFound.ToArray() select a.Address.ToString());

            return result;
        }

        public string FindRowInARangeByTwoExactValues(string workbookName, string sheetName, string range, string value1, string value2)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            string result = "";


            IXLRange rangeFound = ws.Range(range);
            Console.WriteLine(rangeFound.RowsUsed().Count().ToString());
            IEnumerable<IXLRangeRow> rowsFound = rangeFound.RowsUsed().Where(row => row.Cells().Any(cell => cell.GetString() == value1) && row.Cells().Any(cell => cell.GetString() == value2));

            result = String.Join(";", from a in rowsFound select a.RowNumber().ToString());

            return result;
        }

        public string GetCellValue(string workbookName, string sheetName, string cell)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            return ws.Cell(cell).Value.ToString();
        }

        public void WriteCell(string workbookName, string sheetName, string range, string value, string format)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            if (format == null || format.ToUpper() == "TEXT")
            {
                ws.Ranges(range).SetValue(value).SetDataType(XLDataType.Text);
            }
            else if (format.ToUpper() == "NUMBER")
            {
                decimal formattedValue = Decimal.Parse(value, CultureInfo.GetCultureInfo("pt-BR"));
                ws.Ranges(range).SetValue(formattedValue).SetDataType(XLDataType.Number);
            }
            else if (format.ToUpper() == "DATETIME")
            {
                DateTime formattedValue = DateTime.Parse(value, CultureInfo.GetCultureInfo("pt-BR"));
                ws.Ranges(range).SetValue(formattedValue).SetDataType(XLDataType.DateTime);
            }
            else
            {
                throw new Exception("Formato inválido.");
            }

            wb.Save();
        }

        public void WriteRange(string workbookName, string sheetName, string startingCell, bool addHeaders, string data, string columnDelimiter, string rowDelimiter)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            DataTable dataTable = ConvertStringToDataTable(data, columnDelimiter, rowDelimiter);

            if (addHeaders)
                ws.Cell(startingCell).InsertTable(dataTable, false);
            else
                ws.Cell(startingCell).InsertData(dataTable);

            wb.Save();
        }

        public void AppendRange(string workbookName, string sheetName, string data, string columnDelimiter, string rowDelimiter)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            DataTable dataTable = ConvertStringToDataTable(data, columnDelimiter, rowDelimiter);

            ws.Cell(ws.LastRowUsed().RowNumber() + 1, 1).InsertData(dataTable);

            wb.Save();
        }

        //        public string ReadRange(string workbookName, string sheetName, string range, string data, string columnDelimiter, string rowDelimiter)
        //        {
        //            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
        //        
        //            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
        //            var ws = wb.Worksheet(sheetName);
        //
        //            return null;
        //        }

        public void AddRow(string workbookName, string sheetName, int row, int rowsToInsert, bool insertBelow)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            if (insertBelow)
                ws.Row(row).InsertRowsBelow(rowsToInsert);
            else
                ws.Row(row).InsertRowsAbove(rowsToInsert);

            wb.Save();
        }

        public void DeleteRow(string workbookName, string sheetName, string rows)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Rows(rows).Delete();
            wb.Save();
        }

        public void AddColumn(string workbookName, string sheetName, string col, int ColsToInsert, bool insertAfter = true)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            if (insertAfter)
                ws.Column(col).InsertColumnsAfter(ColsToInsert);
            else
                ws.Column(col).InsertColumnsBefore(ColsToInsert);

            wb.Save();
        }

        public void RemoveColumn(string workbookName, string sheetName, string cols)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Columns(cols).Delete();
            wb.Save();
        }

        public string GetLastColumnUsed(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            return wb.Worksheet(sheetName).LastColumnUsed(XLCellsUsedOptions.Contents).ColumnLetter();
        }

        public int GetLastRowUsed(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            return wb.Worksheet(sheetName).LastRowUsed(XLCellsUsedOptions.Contents).RowNumber();
        }

        public string GetUsedRange(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            return wb.Worksheet(sheetName).RangeUsed().RangeAddress.ToString();
        }

        public void AdjustColumnWidth(string workbookName, string sheetName, string colLetters)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Columns(colLetters).AdjustToContents();
        }

        public void AdjustAllColumnsWidth(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Columns().AdjustToContents();
            wb.Save();
        }

        public void AdjustRowHeight(string workbookName, string sheetName, string rowNumbers)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Rows(rowNumbers).AdjustToContents();
        }

        public void HideColumns(string workbookName, string sheetName, string colLetters)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Columns(colLetters).Hide();
            wb.Save();
        }

        public void UnhideColumns(string workbookName, string sheetName, string colLetters)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Columns(colLetters).Unhide();
            wb.Save();
        }

        public void HideRows(string workbookName, string sheetName, string rowNumbers)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Rows(rowNumbers).Hide();
            wb.Save();
        }

        public void UnhideRows(string workbookName, string sheetName, string rowNumbers)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.Rows(rowNumbers).Unhide();
            wb.Save();
        }

        public void CreateExcelTable(string workbookName, string sheetName, string tableName, string themeName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            var table = ws.RangeUsed().CreateTable(tableName);
            table.Theme = XLTableTheme.FromName(themeName);
            wb.Save();
        }

        public void CreateExcelTable(string workbookName, string sheetName, string tableRange, string tableName, string themeName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            var table = ws.Range(tableRange).CreateTable(tableName);
            table.Theme = XLTableTheme.FromName(themeName);
            wb.Save();
        }

        public void AddFilters(string workbookName, string sheetName)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);
            ws.RangeUsed().SetAutoFilter();
            wb.Save();
        }

        private DataTable ConvertStringToDataTable(string data, string columnDelimiter, string rowDelimiter)
        {
            DataTable dataTable = new DataTable();
            bool columnsAdded = false;

            foreach (string row in data.Split(rowDelimiter.ToCharArray()[0]))
            {
                DataRow dataRow = dataTable.NewRow();
                int columnCount = 0;
                foreach (string cell in row.Split(columnDelimiter.ToCharArray()[0]))
                {
                    if (!columnsAdded)
                    {
                        DataColumn dataColumn = new DataColumn(cell);
                        dataTable.Columns.Add(dataColumn);
                    }
                    else
                    {
                        dataRow[columnCount] = cell;
                    }
                    columnCount++;
                }

                if (columnsAdded)
                    dataTable.Rows.Add(dataRow);

                columnsAdded = true;
            }

            return dataTable;
        }

        public void ProtectWorksheet(string workbookName, string sheetName, string sheetPassword)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            ws.Protect(sheetPassword);
            wb.Save();
        }

        public void FormatBordersToThin(string workbookName, string sheetName, string cellsRange)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            if (String.IsNullOrEmpty(cellsRange))
            {
                ws.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }
            else
            {
                ws.Ranges(cellsRange).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Ranges(cellsRange).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            wb.Save();
        }

        public void FormatNumberStyle(string workbookName, string sheetName, string cellsRange, string numberFormat)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(cellsRange)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            ws.Ranges(cellsRange).Style.NumberFormat.Format = numberFormat;

            wb.Save();
        }

        public void SetRangeBackgroundColorToNoFill(string workbookName, string sheetName, string cellsRange)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            if (String.IsNullOrEmpty(cellsRange))
            {
                ws.RangeUsed().Style.Fill.PatternType = XLFillPatternValues.None;
            }
            else
            {
                ws.Ranges(cellsRange).Style.Fill.PatternType = XLFillPatternValues.None;
            }

            wb.Save();
        }

        public void ShowGridLines(string workbookName, string sheetName, bool showGridLines)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            ws.ShowGridLines = showGridLines;

            wb.Save();
        }

        public void AlignRangeContentToLeft(string workbookName, string sheetName, string cellsRange)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            if (String.IsNullOrEmpty(cellsRange))
            {
                ws.RangeUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            }
            else
            {
                ws.Ranges(cellsRange).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            }

            wb.Save();
        }

        public void ProtectWorksheetWithAutoFilterAllowed(string workbookName, string sheetName, string sheetPassword)
        {
            if (String.IsNullOrEmpty(workbookName)) throw new ArgumentException(nameof(workbookName) + " cannot be null or empty.");
            if (String.IsNullOrEmpty(sheetName)) throw new ArgumentException(nameof(sheetName) + " cannot be null or empty.");

            var wb = new XLWorkbook(workbookName, XLEventTracking.Disabled);
            var ws = wb.Worksheet(sheetName);

            var protection = ws.Protect(sheetPassword);
            protection.AllowedElements = XLSheetProtectionElements.AutoFilter;

            wb.Save();
        }
    }
}
