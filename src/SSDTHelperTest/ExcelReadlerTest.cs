using NUnit.Framework;
using System;

namespace SSDTHelperTest
{
  [TestFixture]
  public class ExcelReadlerTest
  {
    [Test]
    public void ReadExcel_SingleSheet()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");
      var tableName = "People";
      var dt = SSDTHelper.ExcelReader.Read(excelFile, tableName);

      Assert.AreEqual("People", dt.TableName);
      Assert.AreEqual(3, dt.Rows.Count);

      Assert.AreEqual("1", dt.Rows[0][0]);
      Assert.AreEqual("Hoge", dt.Rows[0][1]);
      Assert.AreEqual("20", dt.Rows[0][2]);

      Assert.AreEqual("2", dt.Rows[1][0]);
      Assert.AreEqual("Fuga", dt.Rows[1][1]);
      Assert.AreEqual("99", dt.Rows[1][2]);

      Assert.AreEqual("3", dt.Rows[2][0]);
      Assert.AreEqual("FugaFuga", dt.Rows[2][1]);
      Assert.AreEqual("0", dt.Rows[2][2]);
    }

    [Test]
    public void ReadExcel_MultiSheets()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");
      var tableNames = new string[] { "People", "Salary" };
      var dt = SSDTHelper.ExcelReader.Read(excelFile, tableNames);

      Assert.AreEqual("People", dt[0].TableName);
      Assert.AreEqual(3, dt[0].Rows.Count);

      Assert.AreEqual("1", dt[0].Rows[0][0]);
      Assert.AreEqual("Hoge", dt[0].Rows[0][1]);
      Assert.AreEqual("20", dt[0].Rows[0][2]);

      Assert.AreEqual("2", dt[0].Rows[1][0]);
      Assert.AreEqual("Fuga", dt[0].Rows[1][1]);
      Assert.AreEqual("99", dt[0].Rows[1][2]);

      Assert.AreEqual("3", dt[0].Rows[2][0]);
      Assert.AreEqual("FugaFuga", dt[0].Rows[2][1]);
      Assert.AreEqual("0", dt[0].Rows[2][2]);


      Assert.AreEqual("Salary", dt[1].TableName);
      Assert.AreEqual(3, dt[1].Rows.Count);

      Assert.AreEqual("1", dt[1].Rows[0][0]);
      Assert.AreEqual("3000", dt[1].Rows[0][1]);

      Assert.AreEqual("2", dt[1].Rows[1][0]);
      Assert.AreEqual("5000", dt[1].Rows[1][1]);

      Assert.AreEqual("3", dt[1].Rows[2][0]);
      Assert.AreEqual("4000", dt[1].Rows[2][1]);
    }

    [Test]
    public void ReadExcel_DataType()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");
      var dt = SSDTHelper.ExcelReader.Read(excelFile, "DataTypeAndFormatPattern");


      Assert.AreEqual("1", dt.Rows[0]["Id"]);

      // "2017/1/1"
      Assert.AreEqual("2017/01/01", dt.Rows[0]["DateCol01"]);

      // "2017/01/01"
      Assert.AreEqual("2017/01/01", dt.Rows[0]["DateCol02"]);

      // "2017/1/1"
      Assert.AreEqual("2017/01/01", dt.Rows[0]["DatetimeCol01"]);

      // "2017/12/31 16:00"
      Assert.AreEqual("2017/12/31 16:00", dt.Rows[0]["DatetimeCol02"]);

      // "2017/12/ 31 16:00:00"
      Assert.AreEqual("2017/12/31 16:00:00", dt.Rows[0]["DatetimeCol03"]);

      // 2017/12/31 1:1:1
      Assert.AreEqual("2017/12/31 1:1:1", dt.Rows[0]["DatetimeCol04"]);

      // 2017/12/31 00:00:00
      Assert.AreEqual("2017/12/31 00:00:00", dt.Rows[0]["DatetimeCol05"]);

      // 9999999.99
      Assert.AreEqual("9999999.99", dt.Rows[0]["DecCol01"]);

      // 9999999.9
      Assert.AreEqual("9999999.9", dt.Rows[0]["DecCol02"]);

      // 9999999
      Assert.AreEqual("9999999", dt.Rows[0]["DecCol03"]);

      // 9999999.00
      Assert.AreEqual("9999999.00", dt.Rows[0]["DecCol04"]);
    }

    [Test]
    public void ReadExcel_SheetNotFound()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");
        SSDTHelper.ExcelReader.Read(excelFile, "Dummy");
      });
    }

    [Test]
    public void ReadExcel_EvaluateFormula()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");
      var dt = SSDTHelper.ExcelReader.Read(excelFile, "EvaluateFormula");
      
      Assert.AreEqual(DateTime.Now.ToShortDateString(), dt.Rows[0]["Today"]);
      Assert.AreEqual(DateTime.Now.Year.ToString(), dt.Rows[0]["Year"]);
      Assert.AreEqual(DateTime.Now.Month.ToString(), dt.Rows[0]["Month"]);
      Assert.AreEqual(DateTime.Now.Day.ToString(), dt.Rows[0]["Day"]);
    }
  }
}
