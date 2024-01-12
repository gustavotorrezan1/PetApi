using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetApi.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    Ativo = table.Column<int>(type: "INT", maxLength: 1, nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedidas",
                columns: table => new
                {
                    UnidadeMedidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<int>(type: "INT", maxLength: 1, nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedidas", x => x.UnidadeMedidaId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorias",
                columns: table => new
                {
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    Ativo = table.Column<int>(type: "INT", maxLength: 1, nullable: false, defaultValue: 1),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorias", x => x.SubCategoriaId);
                    table.ForeignKey(
                        name: "FK_SubCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    PrecoCusto = table.Column<double>(type: "FLOAT", nullable: false),
                    PrecoVenda = table.Column<double>(type: "FLOAT", nullable: false),
                    CodBarras = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: true),
                    Ativo = table.Column<int>(type: "INT", maxLength: 1, nullable: false, defaultValue: 1),
                    UnidadeMedidaId = table.Column<int>(type: "int", nullable: true),
                    SubCategoriaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                    table.ForeignKey(
                        name: "FK_Produtos_SubCategorias_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategorias",
                        principalColumn: "SubCategoriaId");
                    table.ForeignKey(
                        name: "FK_Produtos_UnidadeMedidas_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalTable: "UnidadeMedidas",
                        principalColumn: "UnidadeMedidaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_SubCategoriaId",
                table: "Produtos",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UnidadeMedidaId",
                table: "Produtos",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorias_CategoriaId",
                table: "SubCategorias",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "SubCategorias");

            migrationBuilder.DropTable(
                name: "UnidadeMedidas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
