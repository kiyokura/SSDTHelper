using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.SqlLocalDb;
using System.Configuration;
using Dapper;

namespace SSDTHelperTest
{

  [SetUpFixture]
  public class Startup
  {
    [OneTimeSetUp]
    public void InitializeTest()
    {
      // create instance
      SSDTHelper.LocalDbUtility.CreateInstance(Config.LocalDbInstanceName, Config.LocalDbInstanceVersion, true);

      // Create database
      SSDTHelper.LocalDbUtility.CreateDatabase(Config.LocalDbInstanceName, Config.DatabaseName);

      // Careate Tables
      using (var cn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString))
      {
        cn.Open();
        var sql = @"

          CREATE TABLE [dbo].[People] (
              [Id]   INT           NOT NULL,
              [Name] NVARCHAR (50) NULL,
              [Age]  INT           NULL,
              PRIMARY KEY CLUSTERED ([Id] ASC)
          );

          CREATE TABLE [dbo].[Salary] (
              [Id]   INT           NOT NULL,
              [Salary]  INT           NULL,
              PRIMARY KEY CLUSTERED ([Id] ASC)
          );

          CREATE TABLE [dbo].[DataTypeAndFormatPattern] (
              [Id]   INT           NOT NULL,
              [DateCol01]  DATE           NULL,
              [DateCol02]  DATE           NULL,
              [DateTimeCol01]  DATETIME           NULL,
              [DateTimeCol02]  DATETIME           NULL,
              [DateTimeCol03]  DATETIME           NULL,
              [DateTimeCol04]  DATETIME           NULL,
              [DateTimeCol05]  DATETIME           NULL,
              [DecCol01]  DECIMAL(9,2)           NULL,
              [DecCol02]  DECIMAL(9,2)           NULL,
              [DecCol03]  DECIMAL(9,2)           NULL,
              [DecCol04]  DECIMAL(9,2)           NULL,
              [DecCol05]  DECIMAL(11,4)           NULL,
              [DecCol06]  DECIMAL(11,4)           NULL,
              PRIMARY KEY CLUSTERED ([Id] ASC)
          );
        ";
        cn.Execute(sql);
      }
    }

  }
}
