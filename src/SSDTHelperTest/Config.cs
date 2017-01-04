using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDTHelperTest
{
  static class Config
  {
    public static string ConnectionString
    {
      get
      {
        return String.Format(ConfigurationManager.AppSettings["ConnectionStringBase"], LocalDbInstanceName, DatabaseName);

      }
    }

    public static string LocalDbInstanceName
    {
      get
      {
        return ConfigurationManager.AppSettings["LocalDbInstanceName"];
      }
    }

    public static string LocalDbInstanceVersion
    {
      get
      {
        return ConfigurationManager.AppSettings["LocalDbInstanceVersion"];
      }
    }

    public static string DatabaseName
    {
      get
      {
        return ConfigurationManager.AppSettings["DatabaseName"];
      }
    }

  }
}

