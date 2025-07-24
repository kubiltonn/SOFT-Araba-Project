using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using System.Net.Http;
using System.Text.Json;

namespace anaproject.Controllers
{
    public class ShopController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7232/api";

        public ShopController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(products?.Where(p => p.IsAvailable).OrderBy(p => p.Category).ThenBy(p => p.Name).ToList() ?? new List<Product>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürünler yüklenirken hata oluştu: " + ex.Message;
            }
            
            return View(new List<Product>());
        }

        public async Task<IActionResult> Category(string category)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var allProducts = JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    var products = allProducts?.Where(p => p.Category == category && p.IsAvailable).OrderBy(p => p.Name).ToList() ?? new List<Product>();
                    ViewBag.Category = category;
                    return View(products);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürünler yüklenirken hata oluştu: " + ex.Message;
            }
            
            ViewBag.Category = category;
            return View(new List<Product>());
        }
    }
} 