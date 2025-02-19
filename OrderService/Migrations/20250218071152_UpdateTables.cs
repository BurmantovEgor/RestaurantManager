using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OraderDetails_Order_OrderId",
                table: "OraderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OraderDetails",
                table: "OraderDetails");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "OraderDetails");

            migrationBuilder.RenameTable(
                name: "OraderDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OraderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OraderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OraderDetails",
                newName: "IX_OraderDetails_OrderId");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "OraderDetails",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OraderDetails",
                table: "OraderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OraderDetails_Order_OrderId",
                table: "OraderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
