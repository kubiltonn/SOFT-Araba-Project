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
    public class GTXController : Controller
    {
        private readonly AppDbContext _context;

        public GTXController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var gtxCars = _context.Cars.Where(c => c.CarTur == "GTX").ToList();
            return View(gtxCars);
        }

        public IActionResult Discover(int id)
        {
            var gtxCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (gtxCar == null) return NotFound();
            return View(gtxCar);
        }
    }
}

