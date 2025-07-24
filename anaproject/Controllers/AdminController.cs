using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace anaproject.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7232/api";

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ServiceAppointments()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/serviceappointment");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var appointments = JsonSerializer.Deserialize<List<ServiceAppointment>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(appointments ?? new List<ServiceAppointment>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Veriler yüklenirken hata oluştu: " + ex.Message;
            }

            return View(new List<ServiceAppointment>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/serviceappointment/{id}",
                    new StringContent(JsonSerializer.Serialize(status), System.Text.Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Durum başarıyla güncellendi.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Güncelleme sırasında hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata: " + ex.Message;
            }

            return RedirectToAction(nameof(ServiceAppointments));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/serviceappointment/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Silme sırasında hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata: " + ex.Message;
            }

            return RedirectToAction(nameof(ServiceAppointments));
        }

        public async Task<IActionResult> Products()
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
                    return View(products ?? new List<Product>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürünler yüklenirken hata oluştu: " + ex.Message;
            }

            return View(new List<Product>());
        }

        public IActionResult CreateProduct()
        {
            return View(new Product());
        }

        // POST: Admin/CreateProduct
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            product.Id = 0;
            product.CreatedAt = DateTime.UtcNow;

            Console.WriteLine($"CreateProduct - Received Product from Model Binding:");
            Console.WriteLine($"  ID: {product.Id}");
            Console.WriteLine($"  Name: '{product.Name}' (Length: {product.Name?.Length ?? 0})");
            Console.WriteLine($"  Category: '{product.Category}' (Length: {product.Category?.Length ?? 0})");
            Console.WriteLine($"  Price: {product.Price}");
            Console.WriteLine($"  Description: '{product.Description}' (Length: {product.Description?.Length ?? 0})");
            Console.WriteLine($"  ImageUrl: '{product.ImageUrl}' (Length: {product.ImageUrl?.Length ?? 0})");
            Console.WriteLine($"  IsAvailable: {product.IsAvailable}");

            var form = Request.Form;
            Console.WriteLine($"Form Keys: {string.Join(", ", form.Keys)}");
            foreach (var key in form.Keys)
            {
                Console.WriteLine($"Form[{key}]: '{form[key]}'");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                ViewBag.ErrorMessage = "Ürün adı gereklidir!";
                Console.WriteLine("Validation failed: Name is empty");
                return View(product);
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                ViewBag.ErrorMessage = "Kategori seçimi gereklidir!";
                Console.WriteLine("Validation failed: Category is empty");
                return View(product);
            }

            if (product.Price <= 0)
            {
                ViewBag.ErrorMessage = "Fiyat 0'dan büyük olmalıdır!";
                Console.WriteLine("Validation failed: Price is invalid");
                return View(product);
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                product.Description = "Açıklama bulunmuyor";
                Console.WriteLine("Setting default Description");
            }

            if (string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = "/img/default-product.jpg";
                Console.WriteLine("Setting default ImageUrl");
            }

            try
            {
                Console.WriteLine("=== JSON Serialization Check ===");
                Console.WriteLine($"Product object - Name: '{product.Name}' (IsNull: {product.Name == null})");
                Console.WriteLine($"Product object - Category: '{product.Category}' (IsNull: {product.Category == null})");
                Console.WriteLine($"Product object - Price: {product.Price}");
                Console.WriteLine($"Product object - Description: '{product.Description}' (IsNull: {product.Description == null})");
                Console.WriteLine($"Product object - ImageUrl: '{product.ImageUrl}' (IsNull: {product.ImageUrl == null})");

                var json = JsonSerializer.Serialize(product);
                Console.WriteLine($"Create Product JSON: {json}");

                var deserializedProduct = JsonSerializer.Deserialize<Product>(json);
                Console.WriteLine("=== Deserialized Product Check ===");
                Console.WriteLine($"Deserialized - Name: '{deserializedProduct?.Name}' (IsNull: {deserializedProduct?.Name == null})");
                Console.WriteLine($"Deserialized - Category: '{deserializedProduct?.Category}' (IsNull: {deserializedProduct?.Category == null})");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/products", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Ürün başarıyla eklendi.";
                    return RedirectToAction(nameof(Products));
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = "Ürün eklenirken hata oluştu: " + errorContent;
                    Console.WriteLine($"API Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürün eklenirken hata oluştu: " + ex.Message;
                Console.WriteLine($"Exception: {ex}");
            }

            return View(product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonString}"); // Debug için

                    var product = JsonSerializer.Deserialize<Product>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    Console.WriteLine($"Deserialized Product - ID: {product.Id}, Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");

                    return View(product);
                }
                else
                {
                    ViewBag.ErrorMessage = $"API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürün yüklenirken hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        public async Task<IActionResult> EditProductPost(int id, Product product)
        {
            product.Id = id;

            Console.WriteLine($"EditProductPost - Received Product from Model Binding:");
            Console.WriteLine($"  ID: {product.Id}");
            Console.WriteLine($"  Name: '{product.Name}' (Length: {product.Name?.Length ?? 0})");
            Console.WriteLine($"  Category: '{product.Category}' (Length: {product.Category?.Length ?? 0})");
            Console.WriteLine($"  Price: {product.Price}");
            Console.WriteLine($"  Description: '{product.Description}' (Length: {product.Description?.Length ?? 0})");
            Console.WriteLine($"  ImageUrl: '{product.ImageUrl}' (Length: {product.ImageUrl?.Length ?? 0})");
            Console.WriteLine($"  IsAvailable: {product.IsAvailable}");

            var form = Request.Form;
            Console.WriteLine($"Form Keys: {string.Join(", ", form.Keys)}");
            foreach (var key in form.Keys)
            {
                Console.WriteLine($"Form[{key}]: '{form[key]}'");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                ViewBag.ErrorMessage = "Ürün adı gereklidir!";
                Console.WriteLine("Validation failed: Name is empty");
                return View("EditProduct", product);
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                ViewBag.ErrorMessage = "Kategori seçimi gereklidir!";
                Console.WriteLine("Validation failed: Category is empty");
                return View("EditProduct", product);
            }

            if (product.Price <= 0)
            {
                ViewBag.ErrorMessage = "Fiyat 0'dan büyük olmalıdır!";
                Console.WriteLine("Validation failed: Price is invalid");
                return View("EditProduct", product);
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                product.Description = "Açıklama bulunmuyor";
                Console.WriteLine("Setting default Description");
            }

            if (string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = "/img/default-product.jpg";
                Console.WriteLine("Setting default ImageUrl");
            }

            try
            {
                Console.WriteLine("=== JSON Serialization Check ===");
                Console.WriteLine($"Product object - Name: '{product.Name}' (IsNull: {product.Name == null})");
                Console.WriteLine($"Product object - Category: '{product.Category}' (IsNull: {product.Category == null})");
                Console.WriteLine($"Product object - Price: {product.Price}");
                Console.WriteLine($"Product object - Description: '{product.Description}' (IsNull: {product.Description == null})");
                Console.WriteLine($"Product object - ImageUrl: '{product.ImageUrl}' (IsNull: {product.ImageUrl == null})");

                var json = JsonSerializer.Serialize(product);
                Console.WriteLine($"EditProduct JSON: {json}");

                var deserializedProduct = JsonSerializer.Deserialize<Product>(json);
                Console.WriteLine("=== Deserialized Product Check ===");
                Console.WriteLine($"Deserialized - Name: '{deserializedProduct?.Name}' (IsNull: {deserializedProduct?.Name == null})");
                Console.WriteLine($"Deserialized - Category: '{deserializedProduct?.Category}' (IsNull: {deserializedProduct?.Category == null})");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/products/{id}", content);
                Console.WriteLine($"API Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Ürün başarıyla güncellendi.";
                    return RedirectToAction(nameof(Products));
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = "Ürün güncellenirken hata oluştu: " + errorContent;
                    Console.WriteLine($"API Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ürün güncellenirken hata oluştu: " + ex.Message;
                Console.WriteLine($"Exception: {ex}");
            }

            return View("EditProduct", product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = "Ürün silinirken hata oluştu: " + errorContent;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ürün silinirken hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Products));
        }
    }
}