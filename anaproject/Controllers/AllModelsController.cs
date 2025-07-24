using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace anaproject.Controllers
{
    public class AllModelsController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 12;
        public AllModelsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var totalCount = await _context.Cars.CountAsync();
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);
            var cars = await _context.Cars
                .Include(c => c.Category)
                .OrderBy(c => c.ID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(cars);
        }
    }
} 