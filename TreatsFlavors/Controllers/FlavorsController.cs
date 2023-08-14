using Microsoft.AspNetCore.Mvc;
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
    }
}
