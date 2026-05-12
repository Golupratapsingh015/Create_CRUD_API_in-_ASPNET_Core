using Create_CRUD_API_in__ASPNET_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Create_CRUD_API_in__ASPNET_Core.Controllers
{
    public class ProductUIController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductUIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/");
        }

        public async Task<IActionResult> Index()
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductVM>>("api/products");
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _httpClient.PostAsJsonAsync("api/products", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductVM>($"api/products/{id}");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _httpClient.PutAsJsonAsync($"api/products/{model.Id}", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/products/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

