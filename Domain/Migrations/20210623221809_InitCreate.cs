using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderTaxProcessor.Domain.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PayTransNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    OrderAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    OrderTax = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    StateCode = table.Column<string>(type: "nchar(2)", nullable: false),
                    FreightCode = table.Column<string>(type: "nchar(1)", nullable: false),
                    IsRetrieved = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsPayProcessed = table.Column<bool>(type: "bit", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    Error = table.Column<bool>(type: "bit", nullable: true),
                    TransMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateModified = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    FreightDate = table.Column<DateTime>(type: "date", nullable: false),
                    FreightStarttime = table.Column<TimeSpan>(type: "time(3)", nullable: false),
                    FreightEndtime = table.Column<TimeSpan>(type: "time(3)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
