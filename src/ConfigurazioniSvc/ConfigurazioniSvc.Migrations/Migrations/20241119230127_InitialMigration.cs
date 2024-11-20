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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FestaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Chiave = table.Column<string>(type: "text", nullable: false),
                    Valore = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Posizione = table.Column<int>(type: "integer", nullable: false),
                    Obbligatorio = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Scope = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurazione", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurazione_Chiave",
                table: "Configurazione",
                column: "Chiave",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurazione");
        }
    }
}
