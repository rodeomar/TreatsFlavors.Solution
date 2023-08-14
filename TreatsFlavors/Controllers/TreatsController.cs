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

        // C - Create
        public async Task<IActionResult> Create([Bind("Name")] Treat treat)
        {
            _context.Add(treat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // R - Read
        [ActionName("Details")]
        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Treat? treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == id);

            if (treat == null)
            {
                return NotFound();
            }

            return View(new { Treat = treat, Context = _context });
        }


        // U - Update
        public async Task<IActionResult> Update(int id, string Name)
        {
            Treat? treat = await _context.Treats.FindAsync(id);

            if (treat != null)
            {
                treat.Name = Name;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { Id = id });
        }

        // D - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Treat? treat = await _context.Treats.FirstOrDefaultAsync(t => t.Id == id);
            if (treat == null)
            {
                return NotFound();
            }

            List<TreatFlavor> treatFlavors = _context.TreatFlavors.Where(tf => tf.TreatId == id).ToList();
            _context.TreatFlavors.RemoveRange(treatFlavors);

            _context.Treats.Remove(treat);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AddFlavor(int TreatId, int FlavorId)
        {
            Treat? treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == TreatId);
            Flavor? flavor = await _context.Flavors.FindAsync(FlavorId);

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
            Treat? treat = await _context.Treats.Include(t => t.TreatFlavors).FirstOrDefaultAsync(t => t.Id == treatId);
            TreatFlavor? flavorToRemove = treat.TreatFlavors.FirstOrDefault(tf => tf.FlavorId == flavorId);

            if (treat != null && flavorToRemove != null)
            {
                treat.TreatFlavors.Remove(flavorToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = treatId });
        }

    }
}
