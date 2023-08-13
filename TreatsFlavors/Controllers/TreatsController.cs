using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TreatsFlavors.Models;

namespace TreatsFlavors.Controllers
{
    public class TreatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var treats = _context.Treats.ToList();
            return View(treats);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Treat treat)
        {
            
            _context.Add(treat);
            _context.SaveChanges();
            

            return RedirectToAction(nameof(Index));
        }
    }
}
