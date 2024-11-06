using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcVendas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fazendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Hectares = table.Column<decimal>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fazendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Representante = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    UnidadeMedida = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", nullable: true),
                    DataAquisicao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    FormaPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    FormaEntrega = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreasPlantio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FazendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Hectares = table.Column<decimal>(type: "TEXT", nullable: false),
                    Localizacao = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasPlantio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreasPlantio_Fazendas_FazendaId",
                        column: x => x.FazendaId,
                        principalTable: "Fazendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FazendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Cpf = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Funcao = table.Column<string>(type: "TEXT", nullable: true),
                    Salario = table.Column<decimal>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Fazendas_FazendaId",
                        column: x => x.FazendaId,
                        principalTable: "Fazendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsVenda_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsVenda_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plantios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataPlantio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AreaPlantioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdutoFinalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantios_AreasPlantio_AreaPlantioId",
                        column: x => x.AreaPlantioId,
                        principalTable: "AreasPlantio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plantios_Recursos_ProdutoFinalId",
                        column: x => x.ProdutoFinalId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    FormaPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    FormaEntrega = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manutencoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Custos = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutencoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NFEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroNota = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Impostos = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFEntradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFEntradas_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NFEntradas_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NFSaidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroNota = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Impostos = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFSaidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFSaidas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NFSaidas_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPlantio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlantioId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPlantio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPlantio_Plantios_PlantioId",
                        column: x => x.PlantioId,
                        principalTable: "Plantios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsPlantio_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompraId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrevisaoEntrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCompra_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCompra_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsNFE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemVendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    NFEntradaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsNFE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsNFE_ItemsVenda_ItemVendaId",
                        column: x => x.ItemVendaId,
                        principalTable: "ItemsVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsNFE_NFEntradas_NFEntradaId",
                        column: x => x.NFEntradaId,
                        principalTable: "NFEntradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsNFS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemCompraId = table.Column<int>(type: "INTEGER", nullable: false),
                    NFSaidaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsNFS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsNFS_ItemsCompra_ItemCompraId",
                        column: x => x.ItemCompraId,
                        principalTable: "ItemsCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsNFS_NFSaidas_NFSaidaId",
                        column: x => x.NFSaidaId,
                        principalTable: "NFSaidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreasPlantio_FazendaId",
                table: "AreasPlantio",
                column: "FazendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FornecedorId",
                table: "Compras",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FuncionarioId",
                table: "Compras",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_FazendaId",
                table: "Funcionarios",
                column: "FazendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCompra_CompraId",
                table: "ItemsCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCompra_RecursoId",
                table: "ItemsCompra",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsNFE_ItemVendaId",
                table: "ItemsNFE",
                column: "ItemVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsNFE_NFEntradaId",
                table: "ItemsNFE",
                column: "NFEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsNFS_ItemCompraId",
                table: "ItemsNFS",
                column: "ItemCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsNFS_NFSaidaId",
                table: "ItemsNFS",
                column: "NFSaidaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPlantio_PlantioId",
                table: "ItemsPlantio",
                column: "PlantioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPlantio_RecursoId",
                table: "ItemsPlantio",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsVenda_RecursoId",
                table: "ItemsVenda",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsVenda_VendaId",
                table: "ItemsVenda",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_FuncionarioId",
                table: "Manutencoes",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_RecursoId",
                table: "Manutencoes",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_NFEntradas_FornecedorId",
                table: "NFEntradas",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_NFEntradas_FuncionarioId",
                table: "NFEntradas",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSaidas_ClienteId",
                table: "NFSaidas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSaidas_FuncionarioId",
                table: "NFSaidas",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantios_AreaPlantioId",
                table: "Plantios",
                column: "AreaPlantioId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantios_ProdutoFinalId",
                table: "Plantios",
                column: "ProdutoFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsNFE");

            migrationBuilder.DropTable(
                name: "ItemsNFS");

            migrationBuilder.DropTable(
                name: "ItemsPlantio");

            migrationBuilder.DropTable(
                name: "Manutencoes");

            migrationBuilder.DropTable(
                name: "ItemsVenda");

            migrationBuilder.DropTable(
                name: "NFEntradas");

            migrationBuilder.DropTable(
                name: "ItemsCompra");

            migrationBuilder.DropTable(
                name: "NFSaidas");

            migrationBuilder.DropTable(
                name: "Plantios");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "AreasPlantio");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Fazendas");
        }
    }
}
