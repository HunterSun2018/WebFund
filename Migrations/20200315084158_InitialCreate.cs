using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFund.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<double>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ChangeInDay = table.Column<double>(nullable: false),
                    ChangeInWeek = table.Column<double>(nullable: false),
                    ChangeInMonth = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funds");
        }
    }
}
