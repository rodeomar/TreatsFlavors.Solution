using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var treats = await _context.Treats.ToListAsync();
            
            return View(treats);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Treat treat)
        {
            _context.Add(treat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treat = await _context.Treats.FirstOrDefaultAsync(t => t.Id == id);
            treat.TreatFlavors = _context.TreatFlavors.ToList();

            if (treat == null)
            {
                return NotFound();
            }
            Console.WriteLine(treat.Name);
            return View(new { Treat=treat, Context=_context });
        }
    }
}
