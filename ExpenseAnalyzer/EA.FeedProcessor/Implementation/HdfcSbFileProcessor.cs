using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EA.FeedProcessor.Interface;
using EA.FeedProcessor.Model;
using EA.Repository.Interface;
using System.Data;

namespace EA.FeedProcessor.Implementation
{
    public class HdfcSbFileProcessor : IFeedProcessor
    {
        private FileInfo file;
        private MemoryStream memoryStream;
        private readonly IHdfcSbStatementRepository repository;

        public FileInfo File => file;

        public HdfcSbFileProcessor(IHdfcSbStatementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FeedProcess> Process(string FileFullname)
        {
            FeedProcess processResult = new FeedProcess()
            {
                Success = false,
                //to do : setting in database.
                ProcessId = null
            };
            file = new FileInfo(FileFullname);
            using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                memoryStream = new MemoryStream();
                await fs.CopyToAsync(this.memoryStream);
            }
            var table = new DataTable();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.memoryStream, false))
            {
                WorkbookPart? workbookPart = doc.WorkbookPart;
                IEnumerable<Sheet>? sheets = workbookPart?.Workbook.GetFirstChild<Sheets>()?.Elements<Sheet>();
                string? relationshipId = sheets?.First().Id;
                WorksheetPart? worksheetPart = workbookPart?.GetPartById(relationshipId!) as WorksheetPart;
                Worksheet? workSheet = worksheetPart?.Worksheet;
                SheetData? sheetData = workSheet?.GetFirstChild<SheetData>();
                IEnumerable<Row>? rows = sheetData?.Descendants<Row>();

                int columnIndex = 0;
                int StartIndex = 0;
                int EndIndex = 0;
                for (int i = 0; i < rows?.Count(); i++)
                {
                    var dateColumn = GetCellValue(doc, rows?.ElementAt<Row>(i).Descendants<Cell>().ElementAt(0)!);
                    if (!string.IsNullOrEmpty(dateColumn) && dateColumn.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = i;
                        i += 2;
                        StartIndex = i;

                    }

                    if (!string.IsNullOrEmpty(dateColumn) && StartIndex > 0 && dateColumn.Contains("********", StringComparison.OrdinalIgnoreCase))
                    {
                        EndIndex = i - 1;
                        break;
                    }
                }

                foreach (Cell cell in rows.Skip(columnIndex).ElementAt(0))
                {
                    table.Columns.Add(cell.CellReference.Value.Substring(0, 1));
                }

                //this will also include your header row...
                foreach (Row row in rows.Skip(StartIndex).Take(EndIndex - (StartIndex - 1)))
                {
                    DataRow tempRow = table.NewRow();
                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        var cell = row.Descendants<Cell>().ElementAt(i);
                        tempRow[cell.CellReference.Value.Substring(0, 1)] = GetCellValue(doc, cell);
                    }
                    table.Rows.Add(tempRow);
                }
            }

            if (table.Rows.Count > 0)
            {
                processResult.Success = await repository.InsertBulkData(table);
            }

            return processResult;
        }

        public string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
    }
}
