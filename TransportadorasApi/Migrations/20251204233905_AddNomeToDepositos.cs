using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportadorasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeToDepositos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "valorTotal",
                table: "Pedidos",
                newName: "ValorTotal");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Depositos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Depositos");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "Pedidos",
                newName: "valorTotal");
        }
    }
}
