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
    public class MotoController : Controller
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var motoCars = _context.Cars.Where(c => c.CarTur == "Motorcycle").ToList();
            return View(motoCars);
        }

        public IActionResult Discover(int id)
        {
            var motoCar = _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefault(c => c.ID == id);
            if (motoCar == null) return NotFound();
            return View(motoCar);
        }
    }
}