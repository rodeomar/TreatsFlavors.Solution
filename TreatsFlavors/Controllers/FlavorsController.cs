using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TreatsFlavors.Models;

namespace TreatsFlavors.Controllers
{
    public class FlavorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlavorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var flavors = await _context.Flavors.ToListAsync();
            return View(flavors);
        }

        // C - Create
        public async Task<IActionResult> Create([Bind("Name")] Flavor flavor)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            _context.Add(flavor);
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

            Flavor? flavor = await _context.Flavors.Include(f => f.TreatFlavors).ThenInclude(tf => tf.Treat).FirstOrDefaultAsync(f => f.Id == id);

            if (flavor == null)
            {
                return NotFound();
            }

            return View(new { Flavor = flavor, Context = _context });
        }

        // U - Update
        public async Task<IActionResult> Update(int flavorId, string Name)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            Flavor? flavor = await _context.Flavors.FindAsync(flavorId);

            if (flavor != null)
            {
                flavor.Name = Name;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = flavorId });
        }

        // D - Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var flavor = await _context.Flavors.FindAsync(id);
            if (flavor == null)
            {
                return NotFound();
            }

            _context.Flavors.Remove(flavor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTreat(int flavorId, int treatId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var flavor = await _context.Flavors.Include(f => f.TreatFlavors).FirstOrDefaultAsync(f => f.Id == flavorId);
            var treat = await _context.Treats.FirstOrDefaultAsync(t => t.Id == treatId);

            if (flavor != null || treat != null)
            {
                var treatFlavor = new TreatFlavor { FlavorId = flavorId, TreatId = treatId };


                try
                {
                    _context.TreatFlavors.Add(treatFlavor);
                    await _context.SaveChangesAsync();
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Details", new { id = flavorId });
                }

            }

            return RedirectToAction("Details", new { id = flavorId });
        }


        public async Task<IActionResult> RemoveTreat(int flavorId, int treatId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            Flavor? flavor = await _context.Flavors.Include(f => f.TreatFlavors).FirstOrDefaultAsync(f => f.Id == flavorId);
            TreatFlavor? treatToRemove = flavor.TreatFlavors.FirstOrDefault(tf => tf.TreatId == treatId);

            if (flavor != null && treatToRemove != null)
            {
                flavor.TreatFlavors.Remove(treatToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = flavorId });
        }



    }
}
