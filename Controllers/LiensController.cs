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
    public class LiensController : Controller
    {
        private readonly MyContext _context;

        public LiensController(MyContext context)
        {
            _context = context;
        }

        // GET: Liens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Liens.ToListAsync());
        }

        // GET: Liens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lien = await _context.Liens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lien == null)
            {
                return NotFound();
            }

            return View(lien);
        }

        // GET: Liens/Create
        public IActionResult Create()
        {
            Lien lien = new Lien();
            return PartialView("_Lien",lien);
        }

        // POST: Liens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,Id,nomRessource")] Lien lien)
        {
            if (ModelState.IsValid)
            {
                var EmailSession = HttpContext.Session.GetString("email");
                _context.Add(lien);
                Publication publication = new Publication
                {
                    dateDePublication = DateTime.Now,
                    Ressource = lien,
                    Membre = _context.Membres.Find(EmailSession)
                };
                _context.Add(publication);
                PersonalizedClass personalized = new PersonalizedClass
                {
                    UserName = User.Identity.Name,
                    UserMail = EmailSession,
                    PublicationDate = DateTime.Now,
                    RessourceName = lien.nomRessource,
                    Url = lien.Url
                };
                _context.Add(personalized);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lien);
        }

        // GET: Liens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lien = await _context.Liens.FindAsync(id);
            if (lien == null)
            {
                return NotFound();
            }
            return View(lien);
        }

        // POST: Liens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,Id,nomRessource")] Lien lien)
        {
            if (id != lien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LienExists(lien.Id))
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
            return View(lien);
        }

        // GET: Liens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lien = await _context.Liens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lien == null)
            {
                return NotFound();
            }

            return View(lien);
        }

        // POST: Liens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lien = await _context.Liens.FindAsync(id);
            _context.Liens.Remove(lien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LienExists(int id)
        {
            return _context.Liens.Any(e => e.Id == id);
        }
    }
}
