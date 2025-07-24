using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using anaproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace anaproject.Controllers
{
    public class CollectionController : Controller
    {
        private readonly AppDbContext _context;

        public CollectionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var collectionCars = _context.Cars
                .Where(c => c.CarTur == "Collection")
                .Include(c => c.CarImages)
                .Include(c => c.Category)
                .Include(c => c.Color)
                .Include(c => c.Package)
                .ToList();
            return View(collectionCars);
        }

        public IActionResult Discover(int id)
        {
            var collectionCar = _context.Cars
                .Include(c => c.CarImages)
                .Include(c => c.Category)
                .Include(c => c.Color)
                .Include(c => c.Package)
                .FirstOrDefault(c => c.ID == id);
            if (collectionCar == null) return NotFound();
            return View(collectionCar);
        }
    }
} 