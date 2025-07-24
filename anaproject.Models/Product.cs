using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace anaproject.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Kategori zorunludur")]
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty; // Merchandise, AutoParts
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; } = true;
        
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 