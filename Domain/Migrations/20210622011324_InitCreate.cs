using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSenderApp.Domain.Migrations
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
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderAmount = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    OrderTax = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    IsRetrieved = table.Column<bool>(type: "bit", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    Error = table.Column<bool>(type: "bit", nullable: true),
                    TransMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderRequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStarttime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderEndtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FreightCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
