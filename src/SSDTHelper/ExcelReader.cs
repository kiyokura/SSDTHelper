using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SSDTHelper
{
  /// <summary>
  /// Read Excel file to DataTable
  /// </summary>
  public class ExcelReader
  {
    /// <summary>
    /// Reads Excel file and creates DataTable
    /// </summary>
    /// <param name="excellFilePath">The path of the target excel file.</param>
    /// <param name="sheetName">The name of the target sheet.</param>
    /// <returns>DataTable object</returns>
    /// <remarks>
    /// Read a specified sheet of Excel file into DataTable.
    /// The sheet name is set in the TableName property of the DataTable.
    /// </remarks>
    public static DataTable Read(string excellFilePath, string sheetName)
    {
      return Read(excellFilePath, (new string[] { sheetName }))[0];
    }

    /// <summary>
    /// Reads Excel file and creates DataTable
    /// </summary>
    /// <param name="excellFilePath">The path of the target excel file.</param>
    /// <param name="sheetNames">The names of the target sheets.</param>
    /// <returns>DataTable objects</returns>
    /// <remarks>
    /// Read a specified sheet of Excel file into DataTable.
    /// The sheet name is set in the TableName property of the DataTable.
    /// </remarks>
    public static IList<DataTable> Read(string path, IEnumerable<string> sheetNames)
    {
      using (var xl = new ExcelPackage())
      {
        var dts = new List<DataTable>();

        using (var st = File.OpenRead(path))
        {
          xl.Load(st);
        }

        foreach (var sheetName in sheetNames)
        {
          using (var ws = xl.Workbook.Worksheets[sheetName])
          {
            var dt = new DataTable();
            dt.TableName = sheetName;

            foreach (var headerCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
              dt.Columns.Add(headerCell.Text);
            }

            var startRow = 2;
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
              var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
              DataRow row = dt.Rows.Add();
              foreach (var cell in wsRow)
              {
                row[cell.Start.Column - 1] = cell.Text;
              }
            }
            dts.Add(dt);
          }

        }
        return dts;
      }
    }
  }
}
