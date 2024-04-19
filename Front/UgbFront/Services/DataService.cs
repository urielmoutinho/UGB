using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UgbFront.Models;

namespace UgbFront.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutos()
        {
            var response = await _httpClient.GetAsync("/Produtos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ProdutoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<IEnumerable<FornecedorModel>> GetFornecedores()
        {
            var response = await _httpClient.GetAsync("/Fornecedores");

            var content = await response.Content.ReadAsStringAsync();
            var fornecedores = JsonSerializer.Deserialize<IEnumerable<FornecedorModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return fornecedores;
        }
        public async Task<FornecedorModel> GetFornecedorById(int fornecedorId)
        {
            var response = await _httpClient.GetAsync($"Fornecedores/{fornecedorId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var fornecedor = JsonSerializer.Deserialize<FornecedorModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return fornecedor;
            }
            else
            {
                return null;
            }
        }


        public async Task<IEnumerable<ServicoModel>> GetServicos()
        {
            var response = await _httpClient.GetAsync("/Servicos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ServicoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<IEnumerable<ServicoInternoModel>> GetServicosInternos()
        {
            var response = await _httpClient.GetAsync("/ServicosInternos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ServicoInternoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<IEnumerable<PedidoInternoModel>> GetPedidosInternos()
        {
            var response = await _httpClient.GetAsync("/PedidosInternos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PedidoInternoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<IEnumerable<UsuarioModel>> GetUsuarios()
        {
            var response = await _httpClient.GetAsync("/Usuarios");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UsuarioModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}