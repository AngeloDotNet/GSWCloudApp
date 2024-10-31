using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurazioniSvc.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurazione",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    festa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valore = table.Column<string>(type: "text", nullable: true),
                    tipo = table.Column<string>(type: "text", nullable: true),
                    posizione = table.Column<int>(type: "integer", nullable: false),
                    obbligatorio = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Scope = table.Column<string>(type: "text", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configurazione", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurazione");
        }
    }
}
