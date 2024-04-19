using Microsoft.EntityFrameworkCore;
using UgbApi.Models;

namespace UgbApi.Data
{
    public class UgbDbContext : DbContext
    {
        public UgbDbContext(DbContextOptions<UgbDbContext> options) : base(options) { }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ServicoModel> Servicos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<PedidoInternoModel> PedidosInternos { get; set; }
        public DbSet<ServicoInternoModel> ServicosInternos { get; set; }
        public DbSet<EntradaEstoqueModel> EntradasEstoque { get; set; }
        public DbSet<SaidaEstoqueModel> SaidasEstoque { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verifica se as opções do DbContext já foram configuradas
            if (!optionsBuilder.IsConfigured)
            {
                // Lê as configurações do arquivo appsettings.json para obter a string de conexão
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Configura o DbContext para usar o SQL Server e a string de conexão 
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}