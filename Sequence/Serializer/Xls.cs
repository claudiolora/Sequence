using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Sequence.Serializer;

public class Xls<T> : ISerializer<List<T[]>>
{
    private string path;

    public Xls(string path)
    {
        this.path = path;
    }

    /// <summary>
    /// https://dev.to/mtmb/generate-excel-with-npoi-in-c-904
    /// </summary>
    /// <param name="values"></param>
    /// <param name="path"></param>
    public void Write(List<T[]> values)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        HSSFFont myFont = (HSSFFont)workbook.CreateFont();
        myFont.FontHeightInPoints = 11;
        myFont.FontName = "Tahoma";


        // Defining a border
        HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
        borderedCellStyle.SetFont(myFont);
        //borderedCellStyle.BorderLeft = BorderStyle.Medium;
        //borderedCellStyle.BorderTop = BorderStyle.Medium;
        //borderedCellStyle.BorderRight = BorderStyle.Medium;
        //borderedCellStyle.BorderBottom = BorderStyle.Medium;
        //borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

        ISheet Sheet = workbook.CreateSheet("Report");

        //CreateHeader(Sheet, borderedCellStyle);

        // This Where the Data row starts from
        int RowIndex = 0;

        //Iteration through some collection
        foreach (T[] row in values)
        {
            int ColIndex = 0;
            //Creating the CurrentDataRow
            IRow CurrentRow = Sheet.CreateRow(RowIndex);
            CreateCell(CurrentRow, ColIndex, (RowIndex + 1).ToString(), borderedCellStyle);


            // Iterate through cub collection
            foreach (T element in row)
            {
                ColIndex++;
                CreateCell(CurrentRow, ColIndex, element.ToString(), borderedCellStyle);
            }
            RowIndex++;
        }


        // Auto sized all the affected columns
        int lastColumNum = Sheet.GetRow(0).LastCellNum;
        for (int i = 0; i <= lastColumNum; i++)
        {
            Sheet.AutoSizeColumn(i);
            GC.Collect();
        }
        // Write Excel to disk 
        using (var fileData = new FileStream(path, FileMode.Create))
        {
            workbook.Write(fileData);
        }
    }

    private void CreateHeader(ISheet sheet, HSSFCellStyle cellStyle)
    {
        //Creat The Headers of the excel
        IRow HeaderRow = sheet.CreateRow(0);

        int i = 0;
        //Create The Actual Cells
        CreateCell(HeaderRow, i++, "Batch Name", cellStyle);
        CreateCell(HeaderRow, i++, "RuleID", cellStyle);
        CreateCell(HeaderRow, i++, "Rule Type", cellStyle);
        CreateCell(HeaderRow, i++, "Code Message Type", cellStyle);
        CreateCell(HeaderRow, i++, "Severity", cellStyle);
    }

    private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
    {
        ICell Cell = CurrentRow.CreateCell(CellIndex);
        Cell.SetCellValue(Value);
        Cell.CellStyle = Style;
    }

}
