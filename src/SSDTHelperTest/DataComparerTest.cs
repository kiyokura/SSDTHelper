using Dapper;
using NUnit.Framework;
using System.Data;

namespace SSDTHelperTest
{
  [TestFixture]
  public class DataComparerTest
  {
    [OneTimeSetUp]
    public void Init()
    {

    }

    [Test]
    public void CompareTest()
    {
      var dt = SSDTHelper.ExcelReader.Read(Util.GetLocalFileFullPath("TestData.xlsx"), "People");

      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt, true);

      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();
        var result = cn.Query(@"SELECT Id , NAME, Age FROM People ORDER BY ID");
        string message;
        var ismatch = SSDTHelper.DataComparer.IsMatch(dt, result, out message);

        Assert.AreEqual(true, ismatch);
      }
    }


    [Test]
    public void CompareTest2()
    {
      var dt = SSDTHelper.ExcelReader.Read(Util.GetLocalFileFullPath("TestData.xlsx"), new string[] { "People", "Salary" });

      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt, true);

      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();
        var result = cn.Query(@"SELECT Id , NAME, Age FROM People ORDER BY ID");
        string message;
        var ismatch = SSDTHelper.DataComparer.IsMatch(dt[0], result, out message);

        Assert.AreEqual(true, ismatch, message);


        var result2 = cn.Query(@"SELECT * FROM Salary ORDER BY ID");
        string message2;
        var ismatch2 = SSDTHelper.DataComparer.IsMatch(dt[1], result2, out message2);

        Assert.AreEqual(true, ismatch2, message2);
      }

    }

    [Test]
    public void CompareTest3()
    {

      // create expected
      var dt = new DataTable();
      dt.Columns.Add("Id");
      dt.Columns.Add("DateCol01");
      dt.Columns.Add("DateCol02");
      dt.Columns.Add("DatetimeCol01");
      dt.Columns.Add("DatetimeCol02");
      dt.Columns.Add("DatetimeCol03");
      dt.Columns.Add("DatetimeCol04");
      dt.Columns.Add("DatetimeCol05");
      dt.Columns.Add("DecCol01");
      dt.Columns.Add("DecCol02");
      dt.Columns.Add("DecCol03");
      dt.Columns.Add("DecCol04");
      dt.Columns.Add("DecCol05");
      dt.Columns.Add("DecCol06");

      var row = dt.Rows.Add();
      row["Id"] = "1";
      row["DateCol01"] = "2017/01/01 0:00:00";
      row["DateCol02"] = "2017/01/01 0:00:00";
      row["DatetimeCol01"] = "2017/01/01 0:00:00";
      row["DatetimeCol02"] = "2017/12/31 16:00:00";
      row["DatetimeCol03"] = "2017/12/31 16:00:00";
      row["DatetimeCol04"] = "2017/12/31 1:01:01";
      row["DatetimeCol05"] = "2017/12/31 0:00:00";
      row["DecCol01"] = "9999999.99"; // decimal(9,2)
      row["DecCol02"] = "9999999.90"; // decimal(9,2)
      row["DecCol03"] = "9999999.00"; // decimal(9,2)
      row["DecCol04"] = "9999999.00"; // decimal(9,2)
      row["DecCol05"] = "9999999.9900"; // decimal(11,4)
      row["DecCol06"] = "9999999.0000"; // decimal(11,4)


      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();

        // SETUP
        var setupSql = @"
          BEGIN
            TRUNCATE TABLE [dbo].[DataTypeAndFormatPattern];
            INSERT INTO DataTypeAndFormatPattern 
                SELECT
                  1 ,
                  '2017/1/1', '2017/01/01',
                  '2017/1/1', '2017/12/31 16:00', '2017/12/ 31 16:00:00', '2017/12/31 1:1:1', '2017/12/31 00:00:00',
                  '9999999.99', '9999999.9', '9999999', '9999999.00','9999999.99', '9999999'
          END
        ";
        cn.Execute(setupSql);

        var result = cn.Query(@"SELECT * FROM DataTypeAndFormatPattern ORDER BY ID");
        string message;
        var ismatch = SSDTHelper.DataComparer.IsMatch(dt, result, out message);

        Assert.AreEqual(true, ismatch, message);
      }

    }


    [Test]
    public void CompareTest4()
    {

      // create expected
      var dt = new DataTable();
      dt.Columns.Add("Id");
      dt.Columns.Add("DateCol01");
      dt.Columns.Add("DateCol02");
      dt.Columns.Add("DatetimeCol01");
      dt.Columns.Add("DatetimeCol02");
      dt.Columns.Add("DatetimeCol03");
      dt.Columns.Add("DatetimeCol04");
      dt.Columns.Add("DatetimeCol05");
      dt.Columns.Add("DecCol01");
      dt.Columns.Add("DecCol02");
      dt.Columns.Add("DecCol03");
      dt.Columns.Add("DecCol04");
      dt.Columns.Add("DecCol05");
      dt.Columns.Add("DecCol06");

      var row = dt.Rows.Add();
      row["Id"] = "1";
      row["DateCol01"] = "2017/01/01 0:00:00";
      row["DateCol02"] = "2017/01/01 0:00:00";
      row["DatetimeCol01"] = "2017/01/01 0:00:00";
      row["DatetimeCol02"] = "2017/12/31 16:00:00";
      row["DatetimeCol03"] = "2017/12/31 16:00:00";
      row["DatetimeCol04"] = "2017/12/31 1:01:01";
      row["DatetimeCol05"] = "2017/12/31 0:00:00";
      row["DecCol01"] = "9999999.99"; // decimal(9,2)
      row["DecCol02"] = "9999999.90"; // decimal(9,2)
      row["DecCol03"] = "9999999.00"; // decimal(9,2)
      row["DecCol04"] = "9999999.00"; // decimal(9,2)
      row["DecCol05"] = "9999999.9900"; // decimal(11,4)
      row["DecCol06"] = "9999999.0000"; // decimal(11,4)



      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();

        // SETUP
        var setupSql = @"
          BEGIN
            TRUNCATE TABLE [dbo].[DataTypeAndFormatPattern];
            INSERT INTO DataTypeAndFormatPattern
                SELECT
                  1 ,
                  '2017/1/1', '2017/01/01',
                  '2017/1/1', '2017/12/31 16:00', '2017/12/ 31 16:00:00', '2017/12/31 1:1:1', '2017/12/31 00:00:00',
                  '9999999.99', '9999999.9', '9999999', '9999999.00','9999999.99', '9999999'
          END
        ";
        cn.Execute(setupSql);


        // execute
        using(var cmd = cn.CreateCommand())
        {
          cmd.CommandText = "SELECT * FROM DataTypeAndFormatPattern ORDER BY ID";
          cmd.CommandType = CommandType.Text;
          using(var dr = cmd.ExecuteReader())
          {
            string message;
            var ismatch = SSDTHelper.DataComparer.IsMatch(dt, dr, out message);

            Assert.AreEqual(true, ismatch, message);

          }
        }
      }

    }

    [Test]
    public void CompareUnmatchTest1()
    {
      var dt = SSDTHelper.ExcelReader.Read(Util.GetLocalFileFullPath("TestData.xlsx"), "People");

      var loader = new SSDTHelper.DataLoader();
      loader.ConnectionString = Config.ConnectionString;
      loader.Load(dt, true);

      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();
        var result = cn.Query(@"SELECT Id , null as NAME, Age FROM People ORDER BY ID");
        string message;
        var ismatch = SSDTHelper.DataComparer.IsMatch(dt, result, out message);

        Assert.AreEqual(false, ismatch);
      }
    }


  }
}
