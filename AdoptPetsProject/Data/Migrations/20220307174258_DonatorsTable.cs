using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptPetsProject.Data.Migrations
{
    public partial class DonatorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kinds",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Donators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_DonatorId",
                table: "Pets",
                column: "DonatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donators_UserId",
                table: "Donators",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Donators_DonatorId",
                table: "Pets",
                column: "DonatorId",
                principalTable: "Donators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Donators_DonatorId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Donators");

            migrationBuilder.DropIndex(
                name: "IX_Pets_DonatorId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kinds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
