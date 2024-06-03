using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datahub.Metadata.Migrations
{
    /// <inheritdoc />
    public partial class AddedAreasOfBusiness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Catalog_Departments",
                table: "CatalogObjects",
                newName: "Areas_Of_Business");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Areas_Of_Business",
                table: "CatalogObjects",
                newName: "Catalog_Departments");
        }
    }
}
