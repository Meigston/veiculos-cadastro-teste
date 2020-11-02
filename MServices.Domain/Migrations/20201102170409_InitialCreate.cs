using Microsoft.EntityFrameworkCore.Migrations;

namespace MServices.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Chassi = table.Column<string>(maxLength: 17, nullable: false),
                    Tipo = table.Column<int>(maxLength: 2, nullable: false),
                    QuantidadePassageiros = table.Column<int>(maxLength: 2, nullable: false),
                    Cor = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => new { x.Id, x.Chassi });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
