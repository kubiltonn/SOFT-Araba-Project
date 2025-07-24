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
    public class OffRoadController : Controller
    {
        private readonly AppDbContext _context;

        public OffRoadController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orCars = _context.Cars.Where(c => c.CarTur == "Off-Road").ToList();
            return View(orCars);
        }

        public IActionResult Discover(int id)
        {
            var offroadCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (offroadCar == null) return NotFound();
            return View(offroadCar);
        }
    }
}