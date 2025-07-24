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
    public class CoupeController : Controller
    {
        private readonly AppDbContext _context;

        public CoupeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var coupCars = _context.Cars.Where(c => c.CarTur == "Coupe").ToList();
            return View(coupCars);
        }
        public IActionResult Discover(int id)
        {
            var coupeCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (coupeCar == null) return NotFound();
            return View(coupeCar);
        }
    }
}