using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using anaproject.Models;

namespace anaproject.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars
                .Include(c => c.Category)
                .Include(c => c.Color)
                .Include(c => c.Package)
                .ToListAsync();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(cars);
        }

        public IActionResult TestDrive()
        {
            return View();
        }

        public IActionResult Financing()
        {
            return View();
        }

        public IActionResult Dealerships()
        {
            var dealerships = new List<object>
            {
                new {
                    name = "SOFT İstanbul Merkez",
                    address = "Maslak, İstanbul",
                    phone = "0212 555 0001",
                    email = "istanbul@soft.com.tr",
                    hours = "09:00 - 18:00",
                    services = new[] { "Satış", "Servis", "Test Sürüşü", "Finansman" }
                },
                new {
                    name = "SOFT Ankara",
                    address = "Çankaya, Ankara",
                    phone = "0312 555 0002",
                    email = "ankara@soft.com.tr",
                    hours = "09:00 - 18:00",
                    services = new[] { "Satış", "Servis", "Test Sürüşü" }
                },
                new {
                    name = "SOFT İzmir",
                    address = "Konak, İzmir",
                    phone = "0232 555 0003",
                    email = "izmir@soft.com.tr",
                    hours = "09:00 - 18:00",
                    services = new[] { "Satış", "Servis", "Test Sürüşü", "Finansman" }
                }
            };

            return View(dealerships);
        }

        [HttpPost]
        public IActionResult RequestTestDrive([FromBody] TestDriveRequestModel model)
        {
            // Test sürüşü talebi işleme
            return Json(new { success = true, message = "Test sürüşü talebiniz alındı. En kısa sürede size ulaşacağız." });
        }

        [HttpPost]
        public IActionResult CalculateFinancing([FromBody] FinancingRequestModel model)
        {
            // Finansman hesaplama
            var monthlyPayment = model.CarPrice * 0.02m;
            var totalInterest = monthlyPayment * model.Months - model.CarPrice;

            return Json(new
            {
                success = true,
                monthlyPayment = monthlyPayment.ToString("N2"),
                totalInterest = totalInterest.ToString("N2"),
                totalAmount = (monthlyPayment * model.Months).ToString("N2")
            });
        }

        [HttpPost]
        public IActionResult RequestQuote([FromBody] QuoteRequestModel model)
        {
            // Fiyat teklifi talebi
            return Json(new { success = true, message = "Fiyat teklifiniz hazırlanıyor. En kısa sürede size ulaşacağız." });
        }
    }

    public class TestDriveRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CarModel { get; set; }
        public string PreferredDate { get; set; }
        public string PreferredTime { get; set; }
        public string Dealership { get; set; }
    }

    public class FinancingRequestModel
    {
        public decimal CarPrice { get; set; }
        public decimal DownPayment { get; set; }
        public int Months { get; set; }
        public decimal InterestRate { get; set; }
    }

    public class QuoteRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CarModel { get; set; }
        public string Color { get; set; }
        public string Package { get; set; }
        public string TradeIn { get; set; }
    }
}