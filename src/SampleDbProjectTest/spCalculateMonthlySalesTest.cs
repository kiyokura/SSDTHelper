using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SampleDbProjectTest
{
  [TestFixture]
  public class spCalculateMonthlySalesTest
  {
    [Test]
    public void spCalculateMonthlySales_Execute()
    {
      var excelFile = Util.GetLocalFileFullPath("spCalculateMonthlySalesTest.xlsx");

      // init data
      var dt = SSDTHelper.ExcelReader.Read(excelFile, "DailySales");
      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt);

      using (var cn = new SqlConnection(Config.ConnectionString))
      {
        cn.Open();

        // execute 
        var param = new {
          year = 2017,
          month = 1
        };
        cn.Execute("spCalculateMonthlySales", param: param, commandType: System.Data.CommandType.StoredProcedure);

        // read result
        var resultset = cn.Query("SELECT * FROM MonthlySales ORDER BY ShopID, SalesMonth");

        // check result set
        var message = "";
        var ismatch = SSDTHelper.ResultCheker.IsMatch(resultset, excelFile, "ResultCheckSheet", out message);
        Assert.AreEqual(true, ismatch, message);
      }
    }
  }
}
