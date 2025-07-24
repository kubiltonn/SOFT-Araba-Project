using Microsoft.AspNetCore.Mvc;
using anaproject.Models;

namespace anaproject.Controllers
{
    public class EmergencyController : Controller
    {
        private readonly AppDbContext _context;

        public EmergencyController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmergencyAlert([FromBody] EmergencyAlertModel model)
        {
            // Acil durum bildirimi gönderme işlemi
            // Bu kısım gerçek uygulamada SMS, email, push notification gönderecek
            return Json(new { success = true, message = "Acil durum bildirimi gönderildi" });
        }

        [HttpGet]
        public IActionResult GetNearbyServices(double latitude, double longitude)
        {
            // Yakındaki servisleri getirme işlemi
            // Bu kısım gerçek uygulamada harita API'si kullanacak
            var services = new List<object>
            {
                new { name = "SOFT Servis Merkezi", distance = "2.5 km", type = "servis" },
                new { name = "Elektrikli Şarj İstasyonu", distance = "1.8 km", type = "sarj" },
                new { name = "Çekici Hizmeti", distance = "3.2 km", type = "cekici" }
            };
            
            return Json(services);
        }
    }

    public class EmergencyAlertModel
    {
        public string CarModel { get; set; }
        public string LicensePlate { get; set; }
        public string ProblemType { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
} 