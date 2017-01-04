using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Data;

namespace SSDTHelperTest
{

  [TestFixture]
  public class ResultCheckerTest
  {

    [OneTimeSetUp]
    public void Init()
    {

    }

    [Test]
    public void ResultCheckerTest01()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");

      // import Data
      var dt = SSDTHelper.ExcelReader.Read(excelFile, "DataTypeAndFormatPattern");
      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt);


      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();

        var result = cn.Query(@"SELECT * FROM DataTypeAndFormatPattern ORDER BY ID");

        var sheetName = "ResultCheckSheet";
        string message;
        var ismatch = SSDTHelper.ResultCheker.IsMatch(result, excelFile, sheetName, out message);

        Assert.AreEqual(true, ismatch, message);
      }
    }


    [Test]
    public void ResultCheckerTest02()
    {
      var excelFile = Util.GetLocalFileFullPath("TestData.xlsx");

      // import Data
      var dt = SSDTHelper.ExcelReader.Read(excelFile, "DataTypeAndFormatPattern");
      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt);


      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();

        using (var cmd = cn.CreateCommand())
        {
          cmd.CommandText = "SELECT * FROM DataTypeAndFormatPattern ORDER BY ID";
          cmd.CommandType = CommandType.Text;
          using (var dr = cmd.ExecuteReader())
          {
            var sheetName = "ResultCheckSheet";
            string message;
            var ismatch = SSDTHelper.ResultCheker.IsMatch(dr, excelFile, sheetName, out message);

            Assert.AreEqual(true, ismatch, message);

          }
        }
      }
    }

  }
}
