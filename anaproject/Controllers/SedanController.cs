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
    public class SedanController : Controller
    {
        private readonly AppDbContext _context;

        public SedanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sedCars = _context.Cars.Where(c => c.CarTur == "Sedan").ToList();
            return View(sedCars);
        }

        public IActionResult Discover(int id)
        {
            var sedanCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (sedanCar == null) return NotFound();
            return View(sedanCar);
        }
    }
}