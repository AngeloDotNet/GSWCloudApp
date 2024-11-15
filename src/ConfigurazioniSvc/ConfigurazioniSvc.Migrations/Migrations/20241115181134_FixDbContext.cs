using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurazioniSvc.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_configurazione",
                table: "Configurazione");

            migrationBuilder.RenameColumn(
                name: "valore",
                table: "Configurazione",
                newName: "Valore");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Configurazione",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "posizione",
                table: "Configurazione",
                newName: "Posizione");

            migrationBuilder.RenameColumn(
                name: "obbligatorio",
                table: "Configurazione",
                newName: "Obbligatorio");

            migrationBuilder.RenameColumn(
                name: "chiave",
                table: "Configurazione",
                newName: "Chiave");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Configurazione",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "modified_date_time",
                table: "Configurazione",
                newName: "ModifiedDateTime");

            migrationBuilder.RenameColumn(
                name: "modified_by_user_id",
                table: "Configurazione",
                newName: "ModifiedByUserId");

            migrationBuilder.RenameColumn(
                name: "festa_id",
                table: "Configurazione",
                newName: "FestaId");

            migrationBuilder.RenameColumn(
                name: "deleted_date_time",
                table: "Configurazione",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "deleted_by_user_id",
                table: "Configurazione",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "created_date_time",
                table: "Configurazione",
                newName: "CreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "created_by_user_id",
                table: "Configurazione",
                newName: "CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configurazione",
                table: "Configurazione",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Configurazione_Chiave",
                table: "Configurazione",
                column: "Chiave",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Configurazione",
                table: "Configurazione");

            migrationBuilder.DropIndex(
                name: "IX_Configurazione_Chiave",
                table: "Configurazione");

            migrationBuilder.RenameColumn(
                name: "Valore",
                table: "Configurazione",
                newName: "valore");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Configurazione",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Posizione",
                table: "Configurazione",
                newName: "posizione");

            migrationBuilder.RenameColumn(
                name: "Obbligatorio",
                table: "Configurazione",
                newName: "obbligatorio");

            migrationBuilder.RenameColumn(
                name: "Chiave",
                table: "Configurazione",
                newName: "chiave");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Configurazione",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ModifiedDateTime",
                table: "Configurazione",
                newName: "modified_date_time");

            migrationBuilder.RenameColumn(
                name: "ModifiedByUserId",
                table: "Configurazione",
                newName: "modified_by_user_id");

            migrationBuilder.RenameColumn(
                name: "FestaId",
                table: "Configurazione",
                newName: "festa_id");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "Configurazione",
                newName: "deleted_date_time");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Configurazione",
                newName: "deleted_by_user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Configurazione",
                newName: "created_date_time");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Configurazione",
                newName: "created_by_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_configurazione",
                table: "Configurazione",
                column: "id");
        }
    }
}
