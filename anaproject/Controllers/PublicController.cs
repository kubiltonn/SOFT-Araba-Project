using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using anaproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace anaproject.Controllers
{
    public class PublicController : Controller
    {
        private readonly AppDbContext _context;

        public PublicController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orCars = _context.Cars.Where(c => c.CarTur == "Taxi" || c.CarTur == "Otobus").ToList();
            return View(orCars);
        }

        public IActionResult Discover(int id)
        {
            var publicCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (publicCar == null) return NotFound();
            return View(publicCar);
        }
    }
}