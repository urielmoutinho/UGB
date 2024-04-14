using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgbApi.Controllers;
using UgbApi.Data;
using UgbApi.Models;

namespace UgbApi.Tests.Controllers
{
    public class FornecedoresControllerTests
    {
        // Teste para verificar se o método GetAll retorna todos os fornecedores
        [Fact]
        public async Task GetFornecedores_RetornaTodosFornecedores()
        {
            // Arrange: Configuração do ambiente de teste
            var fornecedores = new List<FornecedorModel>
            {
                new FornecedorModel { FornecedorId = 1, Nome = "Fornecedor 1" },
                new FornecedorModel { FornecedorId = 2, Nome = "Fornecedor 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<FornecedorModel>>();
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.Provider).Returns(fornecedores.Provider);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.Expression).Returns(fornecedores.Expression);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.ElementType).Returns(fornecedores.ElementType);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.GetEnumerator()).Returns(fornecedores.GetEnumerator());

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Fornecedores).Returns(mockDbSet.Object);

            var controller = new FornecedoresController(mockContext.Object);

            // Act: Execução do método GetAll
            var result = await controller.GetAll();

            // Assert: Verificação dos resultados
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<FornecedorModel>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }

        // Teste para verificar se o método GetFornecedor retorna um fornecedor específico
        [Fact]
        public async Task GetFornecedor_RetornaFornecedorEspecifico()
        {
            // Arrange: Configuração do ambiente de teste
            var fornecedores = new List<FornecedorModel>
            {
                new FornecedorModel { FornecedorId = 1, Nome = "Fornecedor 1" },
                new FornecedorModel { FornecedorId = 2, Nome = "Fornecedor 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<FornecedorModel>>();
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.Provider).Returns(fornecedores.Provider);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.Expression).Returns(fornecedores.Expression);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.ElementType).Returns(fornecedores.ElementType);
            mockDbSet.As<IQueryable<FornecedorModel>>().Setup(m => m.GetEnumerator()).Returns(fornecedores.GetEnumerator());

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Fornecedores).Returns(mockDbSet.Object);

            var controller = new FornecedoresController(mockContext.Object);

            // Act: Execução do método GetFornecedor
            var result = await controller.GetById(1);

            // Assert: Verificação dos resultados
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<FornecedorModel>(viewResult.Value);
            Assert.Equal(1, model.FornecedorId);
        }

        // Teste para verificar se o método PostFornecedor adiciona um novo fornecedor
        [Fact]
        public async Task PostFornecedor_AdicionaNovoFornecedor()
        {
            // Arrange: Configuração do ambiente de teste
            var novoFornecedor = new FornecedorModel { FornecedorId = 3, Nome = "Novo Fornecedor" };

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Fornecedores.Add(novoFornecedor));

            var controller = new FornecedoresController(mockContext.Object);

            // Act: Execução do método PostFornecedor
            var result = await controller.Add(novoFornecedor);

            // Assert: Verificação dos resultados
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Fornecedores", createdAtActionResult.ActionName);
        }

        // Teste para verificar se o método Put atualiza um fornecedor existente
        [Fact]
        public async Task Put_AtualizaFornecedorExistente()
        {
            // Arrange: Configuração do ambiente de teste
            var fornecedorExistente = new FornecedorModel { FornecedorId = 1, Nome = "Fornecedor Existente" };

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Fornecedores.FindAsync(1)).ReturnsAsync(fornecedorExistente);

            var controller = new FornecedoresController(mockContext.Object);

            // Act: Execução do método Put
            var result = await controller.Edit(1, fornecedorExistente);

            // Assert: Verificação dos resultados
            Assert.IsType<OkResult>(result);
        }

        // Teste para verificar se o método Delete remove um fornecedor existente
        [Fact]
        public async Task Delete_RemoveFornecedorExistente()
        {
            // Arrange: Configuração do ambiente de teste
            var fornecedorExistente = new FornecedorModel { FornecedorId = 1, Nome = "Fornecedor Existente" };

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Fornecedores.FindAsync(1)).ReturnsAsync(fornecedorExistente);

            var controller = new FornecedoresController(mockContext.Object);

            // Act: Execução do método Delete
            var result = await controller.Delete(1);

            // Assert: Verificação dos resultados
            var okResult = Assert.IsType<OkResult>(result);
        }
    }


    namespace UgbApi.Tests.Controllers
    {
        public class PedidosInternosControllerTestes
        {
            // Teste para verificar se o método GetPedidosInternos retorna todos os pedidos internos
            [Fact]
            public async Task GetPedidosInternos_RetornaTodosPedidosInternos()
            {
                // Arrange: Configuração do ambiente de teste
                var pedidosInternos = new List<PedidoInternoModel>
            {
                new PedidoInternoModel { PedidoInternoId = 1, Descricao = "Pedido Interno 1" },
                new PedidoInternoModel { PedidoInternoId = 2, Descricao = "Pedido Interno 2" }
            }.AsQueryable();

                var mockDbSet = new Mock<DbSet<PedidoInternoModel>>();
                mockDbSet.As<IQueryable<PedidoInternoModel>>().Setup(m => m.Provider).Returns(pedidosInternos.Provider);
                mockDbSet.As<IQueryable<PedidoInternoModel>>().Setup(m => m.Expression).Returns(pedidosInternos.Expression);
                mockDbSet.As<IQueryable<PedidoInternoModel>>().Setup(m => m.ElementType).Returns(pedidosInternos.ElementType);
                mockDbSet.As<IQueryable<PedidoInternoModel>>().Setup(m => m.GetEnumerator()).Returns(pedidosInternos.GetEnumerator());

                var mockContext = new Mock<UgbDbContext>();
                mockContext.Setup(c => c.PedidosInternos).Returns(mockDbSet.Object);

                var controller = new PedidosInternosController(mockContext.Object);

                // Act: Execução do método GetPedidosInternos
                var result = await controller.GetPedidosInternos();

                // Assert: Verificação dos resultados
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<PedidoInternoModel>>(viewResult.Value);
                Assert.Equal(2, model.Count());
            }

            // Teste para verificar se o método GetPedidoInterno retorna um pedido interno por ID
            [Fact]
            public async Task GetPedidoInterno_RetornaPedidoInternoPorId()
            {
                // Arrange: Configuração do ambiente de teste
                var pedidoInterno = new PedidoInternoModel { PedidoInternoId = 1, Descricao = "Pedido Interno 1" };

                var mockDbSet = new Mock<DbSet<PedidoInternoModel>>();
                mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(pedidoInterno);

                var mockContext = new Mock<UgbDbContext>();
                mockContext.Setup(c => c.PedidosInternos).Returns(mockDbSet.Object);

                var controller = new PedidosInternosController(mockContext.Object);

                // Act: Execução do método GetPedidoInterno
                var result = await controller.GetPedidoInterno(1);

                // Assert: Verificação dos resultados
                var viewResult = Assert.IsType<OkObjectResult>(result.Result);
                var model = Assert.IsAssignableFrom<PedidoInternoModel>(viewResult.Value);
                Assert.Equal(1, model.PedidoInternoId);
            }

            // Teste para verificar se o método PostPedidoInterno retorna uma resposta criada
            [Fact]
            public async Task PostPedidoInterno_RetornaRespostaCriada()
            {
                // Arrange: Configuração do ambiente de teste
                var pedidoInterno = new PedidoInternoModel { PedidoInternoId = 1, Descricao = "Pedido Interno 1" };

                var mockDbSet = new Mock<DbSet<PedidoInternoModel>>();

                var mockContext = new Mock<UgbDbContext>();
                mockContext.Setup(c => c.PedidosInternos).Returns(mockDbSet.Object);

                var controller = new PedidosInternosController(mockContext.Object);

                // Act: Execução do método PostPedidoInterno
                var result = await controller.PostPedidoInterno(pedidoInterno);

                // Assert: Verificação dos resultados
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var model = Assert.IsAssignableFrom<PedidoInternoModel>(createdAtActionResult.Value);
                Assert.Equal(1, model.PedidoInternoId);
            }
        }
    }
}
namespace UgbApi.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        [Fact]
        public async Task GetProdutos_ReturnsAllProdutos()
        {
            var produtos = new List<ProdutoModel>
            {
                new ProdutoModel { ProdutoId = 1, Nome = "Produto 1" },
                new ProdutoModel { ProdutoId = 2, Nome = "Produto 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ProdutoModel>>();
            mockDbSet.As<IQueryable<ProdutoModel>>().Setup(m => m.Provider).Returns(produtos.Provider);
            mockDbSet.As<IQueryable<ProdutoModel>>().Setup(m => m.Expression).Returns(produtos.Expression);
            mockDbSet.As<IQueryable<ProdutoModel>>().Setup(m => m.ElementType).Returns(produtos.ElementType);
            mockDbSet.As<IQueryable<ProdutoModel>>().Setup(m => m.GetEnumerator()).Returns(produtos.GetEnumerator());

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Produtos).Returns(mockDbSet.Object);

            var controller = new ProdutosController(mockContext.Object);

            var result = await controller.GetAll();

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProdutoModel>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }
    }
}

namespace UgbApi.Tests.Controllers
{
    public class ServicosControllerTests
    {
        [Fact]
        public async Task GetServicos_ReturnsAllServicos()
        {
            var servicos = new List<ServicoModel>
            {
                new ServicoModel { ServicoId = 1, Nome = "Servico 1" },
                new ServicoModel { ServicoId = 2, Nome = "Servico 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ServicoModel>>();
            mockDbSet.As<IQueryable<ServicoModel>>().Setup(m => m.Provider).Returns(servicos.Provider);
            mockDbSet.As<IQueryable<ServicoModel>>().Setup(m => m.Expression).Returns(servicos.Expression);
            mockDbSet.As<IQueryable<ServicoModel>>().Setup(m => m.ElementType).Returns(servicos.ElementType);
            mockDbSet.As<IQueryable<ServicoModel>>().Setup(m => m.GetEnumerator()).Returns(servicos.GetEnumerator());

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Servicos).Returns(mockDbSet.Object);

            var controller = new ServicosController(mockContext.Object);

            var result = await controller.GetAll();

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ServicoModel>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }
    }
}

namespace UgbApi.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        [Fact]
        public async Task GetUsuarios_ReturnsAllUsuarios()
        {
            var usuarios = new List<UsuarioModel>
            {
                new UsuarioModel { UsuarioId = 1, Nome = "Usuario 1" },
                new UsuarioModel { UsuarioId = 2, Nome = "Usuario 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<UsuarioModel>>();
            mockDbSet.As<IQueryable<UsuarioModel>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSet.As<IQueryable<UsuarioModel>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSet.As<IQueryable<UsuarioModel>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSet.As<IQueryable<UsuarioModel>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            var mockContext = new Mock<UgbDbContext>();
            mockContext.Setup(c => c.Usuarios).Returns(mockDbSet.Object);

            var controller = new UsuariosController(mockContext.Object);

            var result = await controller.GetAll();

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioModel>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }
    }
}