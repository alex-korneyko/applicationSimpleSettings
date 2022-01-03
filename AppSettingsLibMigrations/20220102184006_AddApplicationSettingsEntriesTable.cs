using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationSimpleSettings.AppSettingsLibMigrations
{
    public partial class AddApplicationSettingsEntriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSettingsEntries",
                columns: table => new
                {
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettingsEntries", x => x.TypeName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSettingsEntries");
        }
    }
}
