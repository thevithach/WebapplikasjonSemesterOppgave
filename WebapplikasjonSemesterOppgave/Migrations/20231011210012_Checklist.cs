using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebapplikasjonSemesterOppgave.Migrations
{
    /// <inheritdoc />
    public partial class Checklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "ProductType",
                keyValue: null,
                column: "ProductType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "OrderEntity",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "OrderStatus",
                table: "OrderEntity",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ChecklistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClutchlamelerSlitasje = table.Column<int>(type: "int", nullable: false),
                    Bremser = table.Column<int>(type: "int", nullable: false),
                    LagerforTrommel = table.Column<int>(type: "int", nullable: false),
                    PTOogOpplagring = table.Column<int>(type: "int", nullable: false),
                    Kjedestrammer = table.Column<int>(type: "int", nullable: false),
                    Wire = table.Column<int>(type: "int", nullable: false),
                    PinionLager = table.Column<int>(type: "int", nullable: false),
                    KilepåKjedehjul = table.Column<int>(type: "int", nullable: false),
                    SylinderLekkasje = table.Column<int>(type: "int", nullable: false),
                    SlangeSkadeLekkasje = table.Column<int>(type: "int", nullable: false),
                    HydraulikkblokkTestbenk = table.Column<int>(type: "int", nullable: false),
                    SkiftOljeiTank = table.Column<int>(type: "int", nullable: false),
                    SkiftOljepåGirboks = table.Column<int>(type: "int", nullable: false),
                    Ringsylinder = table.Column<int>(type: "int", nullable: false),
                    Bremsesylinder = table.Column<int>(type: "int", nullable: false),
                    LedningsnettpåVinsj = table.Column<int>(type: "int", nullable: false),
                    TestRadio = table.Column<int>(type: "int", nullable: false),
                    Knappekasse = table.Column<int>(type: "int", nullable: false),
                    XxBar = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VinsjKjørAlleFunksjoner = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrekkraftKN = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BremsekraftKN = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_OrderId",
                table: "ChecklistItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItems");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "OrderEntity");

            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "OrderEntity",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
