using Microsoft.EntityFrameworkCore.Migrations;

namespace GSWCloudApp.Common.Helpers;

public static class DatabaseHelpers
{
    /// <summary>
    /// Executes an SQL script from the specified assembly asset.
    /// </summary>
    /// <param name="migrationBuilder">The migration builder used to execute the SQL script.</param>
    /// <param name="assetName">The name of the SQL script asset to execute.</param>
    public static void ExecuteSQLScriptFromAssembly(this MigrationBuilder migrationBuilder, string assetName)
    {
        var sqlScriptFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLScripts", assetName);
        migrationBuilder.Sql(File.ReadAllText(sqlScriptFile));
    }
}