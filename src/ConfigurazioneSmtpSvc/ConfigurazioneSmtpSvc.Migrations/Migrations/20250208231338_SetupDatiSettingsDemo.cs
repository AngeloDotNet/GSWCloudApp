using GSWCloudApp.Common.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurazioneSmtpSvc.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class SetupDatiSettingsDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ExecuteSQLScriptFromAssembly("01_insert_demo_settings_sender.sql");
            migrationBuilder.ExecuteSQLScriptFromAssembly("02_insert_demo_settings_smtp.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM SettingSender");
            migrationBuilder.Sql("DELETE FROM SettingSmtp");
        }
    }
}