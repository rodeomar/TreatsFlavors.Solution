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

            var treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == id);

            if (treat == null)
            {
                return NotFound();
            }

            return View(new { Treat = treat, Context = _context });
        }


        
        public async Task<IActionResult> AddFlavor(int TreatId, int FlavorId)
        {
            var treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == TreatId);
            var flavor = await _context.Flavors.FindAsync(FlavorId);

            if (treat != null && flavor != null)
            {
                try
                {
                    treat.TreatFlavors.Add(new TreatFlavor { TreatId = treat.Id, FlavorId = flavor.Id });
                    await _context.SaveChangesAsync();
                }
                catch (InvalidOperationException)
                {

                    return RedirectToAction("Details", new { id = TreatId });

                }
            }

            return RedirectToAction("Details", new { id = TreatId });
        }


        
        public async Task<IActionResult> RemoveFlavor(int treatId, int flavorId)
        {
            var treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == treatId);
            var flavorToRemove = treat.TreatFlavors.FirstOrDefault(tf => tf.FlavorId == flavorId);

            if (treat != null && flavorToRemove != null)
            {
                treat.TreatFlavors.Remove(flavorToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = treatId });
        }

    }
}
