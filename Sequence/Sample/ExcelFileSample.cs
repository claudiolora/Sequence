using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Sequence.Sample;

public class ExcelFileSample<T>
{
    /// <summary>
    /// https://dev.to/mtmb/generate-excel-with-npoi-in-c-904
    /// </summary>
    /// <param name="values"></param>
    /// <param name="path"></param>
    public void Write(List<T[]> values, string path)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        HSSFFont myFont = (HSSFFont)workbook.CreateFont();
        myFont.FontHeightInPoints = 11;
        myFont.FontName = "Tahoma";


        // Defining a border
        HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
        borderedCellStyle.SetFont(myFont);
        borderedCellStyle.BorderLeft = BorderStyle.Medium;
        borderedCellStyle.BorderTop = BorderStyle.Medium;
        borderedCellStyle.BorderRight = BorderStyle.Medium;
        borderedCellStyle.BorderBottom = BorderStyle.Medium;
        borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

        ISheet Sheet = workbook.CreateSheet("Report");
        //Creat The Headers of the excel
        IRow HeaderRow = Sheet.CreateRow(0);

        //Create The Actual Cells
        CreateCell(HeaderRow, 0, "Batch Name", borderedCellStyle);
        CreateCell(HeaderRow, 1, "RuleID", borderedCellStyle);
        CreateCell(HeaderRow, 2, "Rule Type", borderedCellStyle);
        CreateCell(HeaderRow, 3, "Code Message Type", borderedCellStyle);
        CreateCell(HeaderRow, 4, "Severity", borderedCellStyle);

        // This Where the Data row starts from
        int RowIndex = 1;

        //Iteration through some collection
        foreach (T[] row in values)
        {

            int i = 0;
            //Creating the CurrentDataRow
            IRow CurrentRow = Sheet.CreateRow(RowIndex);
            CreateCell(CurrentRow, 0, i.ToString(), borderedCellStyle);
            // This will be used to calculate the merge area
            int NumberOfRules = row.Length;
            if (NumberOfRules > 1)
            {
                int MergeIndex = (NumberOfRules - 1) + RowIndex;

                //Merging Cells
                NPOI.SS.Util.CellRangeAddress MergedBatch = new NPOI.SS.Util.CellRangeAddress(RowIndex, MergeIndex, 0, 0);
                Sheet.AddMergedRegion(MergedBatch);
            }

            // Iterate through cub collection
            foreach (T element in row)
            {
                if (i > 0)
                    CurrentRow = Sheet.CreateRow(RowIndex);

                CreateCell(CurrentRow, 1, element.ToString(), borderedCellStyle);
                RowIndex++;
                i++;
            }
            RowIndex = NumberOfRules >= 1 ? RowIndex : RowIndex + 1;
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

    private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
    {
        ICell Cell = CurrentRow.CreateCell(CellIndex);
        Cell.SetCellValue(Value);
        Cell.CellStyle = Style;
    }

}
