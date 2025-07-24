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
    public class VanController : Controller
    {
        private readonly AppDbContext _context;

        public VanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vanCars = _context.Cars.Where(c => c.CarTur == "Van").ToList();
            return View(vanCars);
        }

        public IActionResult Discover(int id)
        {
            var vanCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (vanCar == null) return NotFound();
            return View(vanCar);
        }
    }
}