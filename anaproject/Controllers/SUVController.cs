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
    public class SUVController : Controller
    {
        private readonly AppDbContext _context;

        public SUVController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var suvCars = _context.Cars.Where(c => c.CarTur == "SUV").ToList();
            return View(suvCars);
        }

        public IActionResult Discover(int id)
        {
            var suvCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (suvCar == null) return NotFound();
            return View(suvCar);
        }
    }
}