using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetMVC_P324.Migrations
{
    public partial class addingdiscountpersantege : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPersantege",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPersantege",
                table: "Products");
        }
    }
}
