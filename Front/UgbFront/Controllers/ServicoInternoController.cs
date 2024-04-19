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
    public class ServicoInternoController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly DataService _dataService;

        public ServicoInternoController(DataService dataService)
        {
            _dataService = dataService;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7162/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Servicos = await GetServicos();
            ViewBag.Usuarios = await GetUsuarios();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServicoInternoModel servicoInterno)
        {
            ViewBag.Usuarios = await _dataService.GetUsuarios();
            ViewBag.Servicos = await _dataService.GetServicos();


            var jsonContent = new StringContent(JsonSerializer.Serialize(servicoInterno), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("ServicosInternos", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(servicoInterno);
            }
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("ServicosInternos");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var servicosInternos = await JsonSerializer.DeserializeAsync<IEnumerable<ServicoInternoModel>>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(servicosInternos);
            }
            else
            {
                return View(new List<ServicoInternoModel>());
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Usuarios = await _dataService.GetUsuarios();
            ViewBag.Servicos = await _dataService.GetServicos();

            var response = await _httpClient.GetAsync($"ServicosInternos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var servicoInterno = await JsonSerializer.DeserializeAsync<ServicoInternoModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(servicoInterno);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ServicoInternoModel servicoInterno)
        {
            ViewBag.Usuarios = await _dataService.GetUsuarios();
            ViewBag.Servicos = await _dataService.GetServicos();


            var jsonContent = new StringContent(JsonSerializer.Serialize(servicoInterno), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"ServicosInternos/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(servicoInterno);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Usuarios = await _dataService.GetUsuarios();
            ViewBag.Servicos = await _dataService.GetServicos();

            var response = await _httpClient.GetAsync($"ServicosInternos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var servicoInterno = await JsonSerializer.DeserializeAsync<ServicoInternoModel>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var servicos = ViewBag.Servicos as IEnumerable<ServicoModel>;
                ServicoModel servico = null;
                if (servicos != null)
                {
                    foreach (var f in servicos)
                    {
                        if (f.ServicoId == servicoInterno.ServicoId)
                        {
                            servico = f;
                            break;
                        }
                    }
                }

                var usuarios = ViewBag.Usuarios as IEnumerable<UsuarioModel>;
                UsuarioModel usuario = null;
                if (usuarios != null)
                {
                    foreach (var f in usuarios)
                    {
                        if (f.UsuarioId == servicoInterno.UsuarioId)
                        {
                            usuario = f;
                            break;
                        }
                    }
                }

                ViewBag.Usuario = usuario;
                ViewBag.Servico = servico;

                return View(servicoInterno);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"ServicosInternos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task<IEnumerable<ServicoModel>> GetServicos()
        {
            try
            {
                var response = await _httpClient.GetAsync("Servicos");
                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<IEnumerable<ServicoModel>>(contentStream, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    return new List<ServicoModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<ServicoModel>();
            }
        }

        private async Task<IEnumerable<UsuarioModel>> GetUsuarios()
        {
            try
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
            catch (Exception ex)
            {
                return new List<UsuarioModel>();
            }
        }
    }
}