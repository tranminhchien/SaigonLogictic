using Microsoft.EntityFrameworkCore.Migrations;

namespace Logictics.DAL.Migrations
{
    public partial class Default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryProductTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateDate = table.Column<double>(nullable: true),
                    ModifyDate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProductCategoryId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quality = table.Column<int>(nullable: true),
                    Weight = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreateDate = table.Column<double>(nullable: true),
                    ModifyDate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StoreId = table.Column<string>(nullable: true),
                    TotalWeight = table.Column<int>(nullable: false),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreateDate = table.Column<double>(nullable: true),
                    ModifyDate = table.Column<double>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    RecipientId = table.Column<string>(nullable: true),
                    CustomerConfirmId = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Shipment = table.Column<string>(nullable: true),
                    PickupDate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreTbl",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateDate = table.Column<double>(nullable: true),
                    ModifyDate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    PassWord = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Role = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreateDate = table.Column<double>(nullable: true),
                    ModifyDate = table.Column<double>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdmin", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserAdmin",
                columns: new[] { "Id", "Address", "CreateDate", "FullName", "ModifyDate", "PassWord", "Phone", "Role", "Status", "UserName" },
                values: new object[] { "69bd714f-9576-45ba-b5b7-f00649be00de", null, null, null, null, "7c4a8d9ca3762af61e59520943dc26494f8941b", null, "ADMIN", "ACTIVE", "admin" });

            migrationBuilder.InsertData(
                table: "UserAdmin",
                columns: new[] { "Id", "Address", "CreateDate", "FullName", "ModifyDate", "PassWord", "Phone", "Role", "Status", "UserName" },
                values: new object[] { "69bd714f-9576-45ba-b5b7-f00649be00df", null, 1.0, null, null, "7c4a8d9ca3762af61e59520943dc26494f8941b", null, "CLIENT", "ACTIVE", "ChienClient" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProductTbl");

            migrationBuilder.DropTable(
                name: "OrderDetailTbl");

            migrationBuilder.DropTable(
                name: "OrderTbl");

            migrationBuilder.DropTable(
                name: "StoreTbl");

            migrationBuilder.DropTable(
                name: "UserAdmin");
        }
    }
}
