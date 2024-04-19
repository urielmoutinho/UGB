using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UgbFront.Models;
using UgbFront.Services;

namespace UgbFront.Controllers
{
    public class ServicoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly DataService _dataService;

        public ServicoController(DataService dataService)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7162/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Servicos");

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var servicos = await JsonSerializer.DeserializeAsync<IEnumerable<ServicoModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(servicos);
            }
            else
            {
                return View(new List<ServicoModel>());
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Fornecedores = await GetFornecedores();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServicoModel servico)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(servico), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Servicos", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(servico);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var responseServico = await _httpClient.GetAsync($"Servicos/{id}");
            var fornecedores = await _dataService.GetFornecedores();

            if (responseServico.IsSuccessStatusCode && fornecedores != null)
            {
                var contentStreamServico = await responseServico.Content.ReadAsStreamAsync();

                var servico = await JsonSerializer.DeserializeAsync<ServicoModel>(contentStreamServico, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                ViewBag.Fornecedores = fornecedores;

                return View(servico);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ServicoModel servico)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(servico), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Servicos/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(servico);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Fornecedores = await _dataService.GetFornecedores();
            var response = await _httpClient.GetAsync($"Servicos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var servico = await JsonSerializer.DeserializeAsync<ServicoModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var fornecedores = ViewBag.Fornecedores as IEnumerable<FornecedorModel>;
                FornecedorModel fornecedor = null;
                foreach (var f in fornecedores)
                {
                    if (f.FornecedorId == servico.FornecedorId)
                    {
                        fornecedor = f;
                        break;
                    }
                }
                ViewBag.Fornecedor = fornecedor;

                return View(servico);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Servicos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task<IEnumerable<FornecedorModel>> GetFornecedores()
        {
            var response = await _httpClient.GetAsync("Fornecedores");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<FornecedorModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                return new List<FornecedorModel>();
            }
        }
    }
}