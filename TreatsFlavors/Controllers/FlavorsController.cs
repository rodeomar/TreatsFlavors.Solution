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

        public IActionResult Index()
        {
            var flavors = _context.Flavors.ToList();
            return View(flavors);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Flavor flavor)
        {
            _context.Add(flavor);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Details(int? id)
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


        public async Task<IActionResult> Delete(int id)
        {
            var flavor = await _context.Flavors.FindAsync(id);
            if (flavor == null)
            {
                return NotFound();
            }

            _context.Flavors.Remove(flavor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
