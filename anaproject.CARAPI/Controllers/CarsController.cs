using anaproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static anaproject.Models.Cars;

namespace anaproject.CARAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cars/{id}/images
        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetCarImages(int id)
        {
            var car = await _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (car == null)
                return NotFound();

            var imageUrls = car.CarImages?.Select(img => img.ImageUrl).ToList() ?? new List<string>();
            return Ok(imageUrls);
        }

        // GET: api/cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _context.Cars
                .Select(c => new {
                    c.ID,
                    c.CarAdi,
                    c.CarModel,
                    c.CarYil,
                    c.CarFiyat,
                    c.CarTur,
                    c.CarImage
                })
                .ToListAsync();
            return Ok(cars);
        }
    }
}