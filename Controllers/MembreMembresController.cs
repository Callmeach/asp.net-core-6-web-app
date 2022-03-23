#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectFirstSteps.Models;

namespace ProjectFirstSteps.Controllers
{
    public class MembreMembresController : Controller
    {
        private readonly MyContext _context;

        public MembreMembresController(MyContext context)
        {
            _context = context;
        }

        // GET: MembreMembres
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembreMembres.ToListAsync());
        }

        // GET: MembreMembres/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreMembre = await _context.MembreMembres
                .FirstOrDefaultAsync(m => m.MailMembre2 == id);
            if (membreMembre == null)
            {
                return NotFound();
            }

            return View(membreMembre);
        }

        // GET: MembreMembres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembreMembres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MailMembre1,MailMembre2")] MembreMembre membreMembre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membreMembre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membreMembre);
        }

        // GET: MembreMembres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreMembre = await _context.MembreMembres.FindAsync(id);
            if (membreMembre == null)
            {
                return NotFound();
            }
            return View(membreMembre);
        }

        // POST: MembreMembres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MailMembre1,MailMembre2")] MembreMembre membreMembre)
        {
            if (id != membreMembre.MailMembre2)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membreMembre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreMembreExists(membreMembre.MailMembre2))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(membreMembre);
        }

        // GET: MembreMembres/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreMembre = await _context.MembreMembres
                .FirstOrDefaultAsync(m => m.MailMembre2 == id);
            if (membreMembre == null)
            {
                return NotFound();
            }

            return View(membreMembre);
        }

        // POST: MembreMembres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var membreMembre = await _context.MembreMembres.FindAsync(id);
            _context.MembreMembres.Remove(membreMembre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembreMembreExists(string id)
        {
            return _context.MembreMembres.Any(e => e.MailMembre2 == id);
        }
    }
}
