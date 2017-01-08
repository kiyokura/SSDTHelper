using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleDbProjectTest
{
  [SetUpFixture]
  public class Startup
  {
    [OneTimeSetUp]
    public void InitializeTest()
    {
      //// create instance
      SSDTHelper.LocalDbUtility.CreateInstance(Config.LocalDbInstanceName, Config.LocalDbInstanceVersion, true);

      //// Create database
      SSDTHelper.LocalDbUtility.CreateDatabase(Config.LocalDbInstanceName, Config.DatabaseName);

      // Deploy database project
      var service = new SSDTHelper.SqlDatabaseTestService();
      var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      service.DeployDatabaseProject(Path.Combine(currentPath, Config.DatabaseProjectFile), Config.DatabaseProjectConfiguration, Config.ConnectionString);
    }

  }
}
