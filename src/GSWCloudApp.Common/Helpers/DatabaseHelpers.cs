using Microsoft.EntityFrameworkCore.Migrations;

namespace GSWCloudApp.Common.Helpers;

public static class DatabaseHelpers
{
    public static void ExecuteSQLScriptFromAssembly(this MigrationBuilder migrationBuilder, string assetName)
    {
        var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLScripts", assetName);
        migrationBuilder.Sql(File.ReadAllText(sqlFile));
    }
}