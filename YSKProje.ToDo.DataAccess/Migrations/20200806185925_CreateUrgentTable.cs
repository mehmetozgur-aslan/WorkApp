using Microsoft.EntityFrameworkCore.Migrations;

namespace YSKProje.ToDo.DataAccess.Migrations
{
    public partial class CreateUrgentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrgentId",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Urgents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urgents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UrgentId",
                table: "Tasks",
                column: "UrgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Urgents_UrgentId",
                table: "Tasks",
                column: "UrgentId",
                principalTable: "Urgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Urgents_UrgentId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Urgents");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UrgentId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UrgentId",
                table: "Tasks");
        }
    }
}
