using Microsoft.EntityFrameworkCore;
using UgbApi.Models;

namespace UgbApi.Data
{
    public class UgbDbContext : DbContext
    {
        public UgbDbContext(DbContextOptions<UgbDbContext> options) : base(options) { }

        // Define o DbSet para a entidade ProdutoModel
        public DbSet<ProdutoModel> Produtos { get; set; }

        // Define o DbSet para a entidade ServicoModel
        public DbSet<ServicoModel> Servicos { get; set; }

        // Define o DbSet para a entidade UsuarioModel
        public DbSet<UsuarioModel> Usuarios { get; set; }

        // Define o DbSet para a entidade FornecedorModel
        public DbSet<FornecedorModel> Fornecedores { get; set; }

        // Define o DbSet para a entidade PedidoInternoModel
        public DbSet<PedidoInternoModel> PedidosInternos { get; set; }

        // Define o DbSet para a entidade EntradaEstoqueModel
        public DbSet<EntradaEstoqueModel> EntradasEstoque { get; set; }

        // Define o DbSet para a entidade SaidasEstoqueModel
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