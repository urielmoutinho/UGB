using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UgbFront.Models;
using UgbFront.Services;

namespace UgbFront.Controllers
{
    public class PedidoInternoController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly DataService _dataService;

        public PedidoInternoController(DataService dataService)
        {
            _dataService = dataService;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7162/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("PedidosInternos");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var pedidos = await JsonSerializer.DeserializeAsync<IEnumerable<PedidoInternoModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(pedidos);
            }
            else
            {
                return View(new List<PedidoInternoModel>());
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Produtos = await _dataService.GetProdutos();
            ViewBag.Usuarios = await _dataService.GetUsuarios();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoInternoModel pedidoInterno)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(pedidoInterno), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("PedidosInternos", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(pedidoInterno);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Produtos = await _dataService.GetProdutos();
            ViewBag.Usuarios = await _dataService.GetUsuarios();

            // Obter o pedido interno pelo ID
            var response = await _httpClient.GetAsync($"PedidosInternos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var contentStream = await response.Content.ReadAsStreamAsync();
            var pedidoInterno = await JsonSerializer.DeserializeAsync<PedidoInternoModel>(contentStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(pedidoInterno);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PedidoInternoModel pedidoInterno)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(pedidoInterno), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"PedidosInternos/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(pedidoInterno);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Produtos = await _dataService.GetProdutos();
            ViewBag.Usuarios = await _dataService.GetUsuarios();

            var response = await _httpClient.GetAsync($"PedidosInternos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var pedidoInterno = await JsonSerializer.DeserializeAsync<PedidoInternoModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var produtos = ViewBag.Produtos as IEnumerable<ProdutoModel>;
                ProdutoModel produto = null;
                foreach (var f in produtos)
                {
                    if (f.ProdutoId == pedidoInterno.ProdutoId)
                    {
                        produto = f;
                        break;
                    }
                }
                var usuarios = ViewBag.Usuarios as IEnumerable<UsuarioModel>;
                UsuarioModel usuario = null;
                foreach (var f in usuarios)
                {
                    if (f.UsuarioId == pedidoInterno.UsuarioId)
                    {
                        usuario = f;
                        break;
                    }
                }

                ViewBag.Usuario = usuario;
                ViewBag.Produto = produto;



                return View(pedidoInterno);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"PedidosInternos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task<IEnumerable<ProdutoModel>> GetProdutos()
        {
            var response = await _httpClient.GetAsync("Produtos");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                return new List<ProdutoModel>();
            }
        }
        private async Task<IEnumerable<UsuarioModel>> GetUsuarios()
        {
            var response = await _httpClient.GetAsync("Usuarios");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<UsuarioModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                return new List<UsuarioModel>();
            }
        }
    }
}
