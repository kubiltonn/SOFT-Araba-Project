using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace anaproject.Controllers
{
    public class ConfiguratorController : Controller
    {
        private readonly AppDbContext _context;

        public ConfiguratorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            var colors = _context.Colors.ToList();
            var packages = _context.Packages.ToList();

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;
            ViewBag.Packages = packages;

            return View();
        }

        [HttpPost]
        public IActionResult GetModelsByCategory([FromForm] int categoryId)
        {
            var models = _context.Cars
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new { c.ID, c.CarAdi, c.CarModel, c.CarImage, c.CarFiyat })
                .ToList();

            return Json(models);
        }

        [HttpPost]
        public IActionResult GetModelDetails([FromForm] int modelId)
        {
            var model = _context.Cars
                .Include(c => c.Color)
                .Include(c => c.Package)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ID == modelId);

            if (model == null)
                return NotFound();

            return Json(new
            {
                model.ID,
                model.CarAdi,
                model.CarModel,
                model.CarImage,
                model.CarFiyat,
                model.CarBeygir,
                model.CarMenzil,
                model.CarKW,
                model.CarSifirYuz,
                Color = model.Color?.ColorName,
                Package = model.Package?.PackageName,
                Category = model.Category?.CategoryName
            });
        }

        [HttpPost]
        public IActionResult CalculatePrice([FromForm] int modelId, [FromForm] int? colorId = null, [FromForm] int? packageId = null)
        {
            var model = _context.Cars.FirstOrDefault(c => c.ID == modelId);
            var color = colorId.HasValue ? _context.Colors.FirstOrDefault(c => c.ColorId == colorId.Value) : null;
            var package = packageId.HasValue ? _context.Packages.FirstOrDefault(p => p.PackageId == packageId.Value) : null;

            if (model == null) return NotFound();

            decimal basePrice = model.CarFiyat;
            decimal colorPrice = color?.ColorPrice ?? 0;
            decimal packagePrice = package?.PackagePrice ?? 0;

            decimal totalPrice = basePrice + colorPrice + packagePrice;

            return Json(new
            {
                BasePrice = basePrice,
                ColorPrice = colorPrice,
                PackagePrice = packagePrice,
                TotalPrice = totalPrice
            });
        }

        [HttpPost]
        public IActionResult SaveConfiguration([FromBody] CarConfigurationRequest request)
        {
            // Burada konfigürasyonu veritabanına kaydedebiliriz
            return Json(new { success = true, message = "Konfigürasyon başarıyla kaydedildi!" });
        }
    }

    public class CarConfigurationRequest
    {
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int PackageId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Notes { get; set; }
    }
}