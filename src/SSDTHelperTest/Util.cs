using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSDTHelperTest
{
  internal static class Util
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathFromProjectRoot"></param>
    /// <returns></returns>
    internal static string GetLocalFileFullPath(string pathFromProjectRoot)
    {
      var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var path = Path.Combine(execPath, "..\\..\\", pathFromProjectRoot);
      return path;
    }
  }
}
