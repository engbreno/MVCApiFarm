using MvcApiFarm.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Recurso> Recursos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Fazenda> Fazendas { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<AreaPlantio> AreasPlantio { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<ItemCompra> ItemsCompra { get; set; }
    public DbSet<ItemVenda> ItemsVenda { get; set; }
    public DbSet<NFEntrada> NFEntradas { get; set; }
    public DbSet<NFSaida> NFSaidas { get; set; }
    public DbSet<ItemNFE> ItemsNFE { get; set; }
    public DbSet<ItemNFS> ItemsNFS { get; set; }
    public DbSet<Plantio> Plantios { get; set; }
    public DbSet<ItemPlantio> ItemsPlantio { get; set; }
    public DbSet<Manutencao> Manutencoes { get; set; }
}