namespace SSDTHelper
{
  /// <summary>
  /// Derived class of Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestService.
  /// </summary>
  public class SqlDatabaseTestService : Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestService
  {
    /// <summary>
    /// Deploy a database project to the database, independent of the referenced configuration.
    /// </summary>
    /// <param name="databaseProjectFileName">The path of the database project file. absolute path or relative path from the currently executing assembly.</param>
    /// <param name="configuration">The solution build configuration that is used the deployment MSBuild task is executed.</param>
    /// <param name="connectionString">The connection string for the target database. This must be a connection string to a SQL database.</param>
    /// <remarks>
    /// To use overload of SqlDatabaseTestService.DeployDatabaseProject.
    /// The arguments roughly matches the SqlUnitTesting section of app.config, so reference it.
    /// See also: https://msdn.microsoft.com/en-us/library/microsoft.data.tools.schema.sql.unittesting.sqldatabasetestservice.deploydatabaseproject.aspx
    /// </remarks>
    public void DeployDatabaseProject(string databaseProjectFileName, string configuration, string connectionString)
    {
      Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestService.DeployDatabaseProject(databaseProjectFileName, configuration, "System.Data.SqlClient", connectionString);
    }
  }
}
