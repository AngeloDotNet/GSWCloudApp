using GSWCloudApp.Common.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutenticazioneSvc.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ExecuteSQLScriptFromAssembly("01_create_system_user.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE UserName = 'SYSTEM'");
        }
    }
}
