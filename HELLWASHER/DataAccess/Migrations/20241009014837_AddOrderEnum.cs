using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WashStatuses_WashStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WashStatusId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "WashStatusId",
                table: "Orders",
                newName: "WashStatus");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WashingStatusId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WashingStatusId",
                table: "Orders",
                column: "WashingStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WashStatuses_WashingStatusId",
                table: "Orders",
                column: "WashingStatusId",
                principalTable: "WashStatuses",
                principalColumn: "WashingStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WashStatuses_WashingStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WashingStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WashingStatusId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "WashStatus",
                table: "Orders",
                newName: "WashStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WashStatusId",
                table: "Orders",
                column: "WashStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WashStatuses_WashStatusId",
                table: "Orders",
                column: "WashStatusId",
                principalTable: "WashStatuses",
                principalColumn: "WashingStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
