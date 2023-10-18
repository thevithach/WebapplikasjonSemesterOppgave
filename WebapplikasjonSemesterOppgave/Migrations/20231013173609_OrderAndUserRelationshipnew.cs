using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebapplikasjonSemesterOppgave.Migrations
{
    /// <inheritdoc />
    public partial class OrderAndUserRelationshipnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_AspNetUsers_UserId",
                table: "OrderEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntity_UserId",
                table: "OrderEntity");

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderEntity",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_UserId",
                table: "OrderEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_AspNetUsers_UserId",
                table: "OrderEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_AspNetUsers_UserId",
                table: "OrderEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntity_UserId",
                table: "OrderEntity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderEntity",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_UserId",
                table: "OrderEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_AspNetUsers_UserId",
                table: "OrderEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
