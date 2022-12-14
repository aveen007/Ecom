using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class AddiingValueTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Unique_Product_Specification",
                table: "Product_Specification");

            migrationBuilder.DropIndex(
                name: "Unique_Category_Specification",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "Product_Specification");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Product_Specification");

            migrationBuilder.AddColumn<int>(
                name: "ValueTypeId",
                table: "Specification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SpecificationName",
                table: "Product_Specification",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecificationName",
                table: "Specification",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ValueTypeId",
                table: "Product_Specification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ValueType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueName = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Unique_Product_Specification",
                table: "Product_Specification",
                column: "SpecificationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Category_Specification",
                table: "Specification",
                column: "SpecificationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Value_Type",
                table: "ValueType",
                column: "ValueName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Specification_ValueType",
                table: "Product_Specification",
                column: "ValueTypeId",
                principalTable: "ValueType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specification_ValueType",
                table: "Specification",
                column: "ValueTypeId",
                principalTable: "ValueType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Specification_ValueType",
                table: "Product_Specification");

            migrationBuilder.DropForeignKey(
                name: "FK_Specification_ValueType",
                table: "Specification");

            migrationBuilder.DropTable(
                name: "ValueType");

            migrationBuilder.DropIndex(
                name: "Unique_Product_Specification",
                table: "Product_Specification");

            migrationBuilder.DropIndex(
                name: "Unique_Category_Specification",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "ValueTypeId",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "SpecificationName",
                table: "Product_Specification");

            migrationBuilder.DropColumn(
                name: "SpecificationName",
                table: "Specification");

            migrationBuilder.DropColumn(
                name: "ValueTypeId",
                table: "Product_Specification");

            migrationBuilder.AddColumn<int>(
                name: "ValueType",
                table: "Specification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "Product_Specification",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "Specification",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ValueType",
                table: "Product_Specification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "Unique_Product_Specification",
                table: "Product_Specification",
                column: "Specification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Category_Specification",
                table: "Specification",
                column: "Specification",
                unique: true);
        }
    }
}
