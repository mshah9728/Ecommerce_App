using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomApp.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createduserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modifyuserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Product");
        }
    }
}
