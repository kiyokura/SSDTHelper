using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace SampleDbProjectTest
{
  [TestFixture]
  public class fncGetFirstDateOfMonthTest
  {
    [Test]
    public void fncGetFirstDateOfMonth_Execute()
    {
      using (var cn = new SqlConnection(Config.ConnectionString))
      {
        cn.Open();
        var sql = "SELECT dbo.fncGetFirstDateOfMonth(2017,1)";
        var result = cn.Query<DateTime>(sql).Single();
        Assert.AreEqual(new DateTime(2017, 1, 1), result);
      }
    }

  }
}
