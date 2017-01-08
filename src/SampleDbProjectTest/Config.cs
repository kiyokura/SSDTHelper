using System;
using System.Configuration;

namespace SampleDbProjectTest
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

    public static string DatabaseProjectFile
    {
      get
      {
        return ConfigurationManager.AppSettings["DatabaseProjectFile"];
      }
    }

    public static string DatabaseProjectConfiguration
    {
      get
      {
        return ConfigurationManager.AppSettings["DatabaseProjectConfiguration"];
      }
    }

  }
}
