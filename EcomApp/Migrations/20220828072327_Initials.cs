using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomApp.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Customer",
                columns: table => new
                {
                    Customer_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Customer_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Customer_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Createdby_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy_UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Customer", x => x.Customer_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product_Category",
                columns: table => new
                {
                    P_Category_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    P_Category_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    P_Category_description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    createdTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createdby_Userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modifyby_userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleteby_userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product_Category", x => x.P_Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Supplier",
                columns: table => new
                {
                    Supplier_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Supplier_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Supplier_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Supplier_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Createdby_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy_UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Supplier", x => x.Supplier_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Customer_Billing_Detail",
                columns: table => new
                {
                    C_BillingDetail_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C_BillingDetail_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    C_billingDetail_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Customer_Billing_Detail", x => x.C_BillingDetail_ID);
                    table.ForeignKey(
                        name: "FK_tbl_Customer_Billing_Detail_tbl_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "tbl_Customer",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product",
                columns: table => new
                {
                    Product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product_UPC_Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Product_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    product_image_URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    discounted_percentage = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    item_in_stock = table.Column<bool>(type: "bit", maxLength: 100, nullable: false),
                    quantity = table.Column<long>(type: "bigint", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    createdTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createduserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modifyuserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTimeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product", x => x.Product_id);
                    table.ForeignKey(
                        name: "FK_tbl_Product_tbl_Product_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "tbl_Product_Category",
                        principalColumn: "P_Category_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Product_tbl_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "tbl_Supplier",
                        principalColumn: "Supplier_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product_Location",
                columns: table => new
                {
                    P_L_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product_location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy_userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product_Location", x => x.P_L_ID);
                    table.ForeignKey(
                        name: "FK_tbl_Product_Location_tbl_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tbl_Product",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Customer_Billing_Detail_CustomerID",
                table: "tbl_Customer_Billing_Detail",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_CategoryID",
                table: "tbl_Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_SupplierID",
                table: "tbl_Product",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_Location_ProductID",
                table: "tbl_Product_Location",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Customer_Billing_Detail");

            migrationBuilder.DropTable(
                name: "tbl_Product_Location");

            migrationBuilder.DropTable(
                name: "tbl_Customer");

            migrationBuilder.DropTable(
                name: "tbl_Product");

            migrationBuilder.DropTable(
                name: "tbl_Product_Category");

            migrationBuilder.DropTable(
                name: "tbl_Supplier");
        }
    }
}
