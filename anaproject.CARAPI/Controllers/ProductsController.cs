using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace anaproject.CARAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _context.Products.OrderBy(p => p.Category).ThenBy(p => p.Name).ToListAsync();
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/products
        [HttpPost("")]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is null");
                }

                // Gelen JSON'u logla
                Request.EnableBuffering();
                Request.Body.Position = 0;
                using var reader = new StreamReader(Request.Body, leaveOpen: true);
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine($"API Create - Raw Request Body: {requestBody}");
                Request.Body.Position = 0;

                // Debug için gelen veriyi logla
                Console.WriteLine($"API Create - Received Product:");
                Console.WriteLine($"  Name: '{product.Name}' (Length: {product.Name?.Length ?? 0})");
                Console.WriteLine($"  Category: '{product.Category}' (Length: {product.Category?.Length ?? 0})");
                Console.WriteLine($"  Price: {product.Price}");
                Console.WriteLine($"  Description: '{product.Description}' (Length: {product.Description?.Length ?? 0})");
                Console.WriteLine($"  ImageUrl: '{product.ImageUrl}' (Length: {product.ImageUrl?.Length ?? 0})");
                Console.WriteLine($"  IsAvailable: {product.IsAvailable}");

                // Validation
                if (string.IsNullOrEmpty(product.Name))
                {
                    Console.WriteLine("API Validation failed: Product name is empty");
                    return BadRequest("Product name is required");
                }

                if (string.IsNullOrEmpty(product.Category))
                {
                    Console.WriteLine("API Validation failed: Product category is empty");
                    return BadRequest("Product category is required");
                }

                if (product.Price <= 0)
                {
                    Console.WriteLine("API Validation failed: Product price is invalid");
                    return BadRequest("Product price must be greater than 0");
                }

                // Default değerler
                if (string.IsNullOrEmpty(product.Description))
                {
                    product.Description = "Açıklama bulunmuyor";
                    Console.WriteLine("API Setting default Description");
                }

                if (string.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = "/img/default-product.jpg";
                    Console.WriteLine("API Setting default ImageUrl");
                }

                // Yeni ürün için ID'yi 0 yap (auto-increment için)
                product.Id = 0;
                
                // CreatedAt alanını şu anki zaman olarak ayarla
                product.CreatedAt = DateTime.UtcNow;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                
                Console.WriteLine($"Product created with ID: {product.Id}");
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Create Error: {ex.Message}");
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                // Gelen JSON'u logla
                Request.EnableBuffering();
                Request.Body.Position = 0;
                using var reader = new StreamReader(Request.Body, leaveOpen: true);
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine($"API Update - Raw Request Body: {requestBody}");
                Request.Body.Position = 0;
                
                // Önce mevcut ürünü bul
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Debug için gelen veriyi logla
                Console.WriteLine($"API Update - Received Product:");
                Console.WriteLine($"  ID: {product.Id}");
                Console.WriteLine($"  Name: '{product.Name}' (Length: {product.Name?.Length ?? 0})");
                Console.WriteLine($"  Category: '{product.Category}' (Length: {product.Category?.Length ?? 0})");
                Console.WriteLine($"  Price: {product.Price} (Type: {product.Price.GetType().Name})");
                Console.WriteLine($"  Description: '{product.Description}' (Length: {product.Description?.Length ?? 0})");
                Console.WriteLine($"  ImageUrl: '{product.ImageUrl}' (Length: {product.ImageUrl?.Length ?? 0})");
                Console.WriteLine($"  IsAvailable: {product.IsAvailable}");
                
                // Mevcut ürünün fiyatını da logla
                Console.WriteLine($"Existing Product Price: {existingProduct.Price} (Type: {existingProduct.Price.GetType().Name})");

                // Validation
                if (string.IsNullOrEmpty(product.Name))
                {
                    Console.WriteLine("API Validation failed: Product name is empty");
                    return BadRequest("Product name is required");
                }

                if (string.IsNullOrEmpty(product.Category))
                {
                    Console.WriteLine("API Validation failed: Product category is empty");
                    return BadRequest("Product category is required");
                }

                if (product.Price <= 0)
                {
                    Console.WriteLine("API Validation failed: Product price is invalid");
                    return BadRequest("Product price must be greater than 0");
                }

                // Default değerler
                if (string.IsNullOrEmpty(product.Description))
                {
                    product.Description = "Açıklama bulunmuyor";
                    Console.WriteLine("API Setting default Description");
                }

                if (string.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = "/img/default-product.jpg";
                    Console.WriteLine("API Setting default ImageUrl");
                }

                // Sadece değişen alanları güncelle
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                
                // Fiyat güncelleme
                Console.WriteLine($"Updating Price: {existingProduct.Price} -> {product.Price}");
                existingProduct.Price = product.Price;
                Console.WriteLine($"Price Updated: {existingProduct.Price}");
                
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.IsAvailable = product.IsAvailable;
                // CreatedAt alanını koru (değiştirme)

                await _context.SaveChangesAsync();
                Console.WriteLine("Product updated successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Update Error: {ex.Message}");
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
} 