using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Approximation
{
    class MyReadFromExcel
    {
        public string[,] MyData;
        public int NRow;
        public int NColumn;
        public void GetData(string FileName)
        {
            Excel.Application ObjWorkExcel = new Excel.Application();
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); 
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; 
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            NColumn = lastCell.Column;
            NRow = lastCell.Row;
            MyData = new string[lastCell.Column, lastCell.Row]; 
            for (int i = 0; i < lastCell.Column; i++) 
                for (int j = 0; j < lastCell.Row; j++)
                    MyData[i, j] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();
            ObjWorkBook.Close(false, Type.Missing, Type.Missing); 
            ObjWorkExcel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjWorkExcel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjWorkBook);
            GC.Collect(); 
        }
    }
}
