using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UgbFront.Models;

namespace UgbFront.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly HttpClient _httpClient;

        public FornecedorController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7162/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Fornecedores");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var fornecedores = await JsonSerializer.DeserializeAsync<IEnumerable<FornecedorModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(fornecedores);
            }
            else
            {
                return View(new List<FornecedorModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FornecedorModel fornecedor)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(fornecedor), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Fornecedores", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(fornecedor);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Fornecedores/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var fornecedor = await JsonSerializer.DeserializeAsync<FornecedorModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(fornecedor);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FornecedorModel fornecedor)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(fornecedor), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Fornecedores/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(fornecedor);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Fornecedores/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var fornecedor = await JsonSerializer.DeserializeAsync<FornecedorModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(fornecedor);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Fornecedores/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}