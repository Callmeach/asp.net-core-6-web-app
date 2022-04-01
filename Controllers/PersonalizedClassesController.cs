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
    public class PersonalizedClassesController : Controller
    {
        private readonly MyContext _context;

        public PersonalizedClassesController(MyContext context)
        {
            _context = context;
        }

        // GET: PersonalizedClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personalizeds.ToListAsync());
        }

        // GET: PersonalizedClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedClass = await _context.Personalizeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalizedClass == null)
            {
                return NotFound();
            }

            return View(personalizedClass);
        }

        // GET: PersonalizedClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalizedClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,UserMail,PublicationDate,RessourceName,Url,Libelle,Legende,Path")] PersonalizedClass personalizedClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalizedClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalizedClass);
        }

        // GET: PersonalizedClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedClass = await _context.Personalizeds.FindAsync(id);
            if (personalizedClass == null)
            {
                return NotFound();
            }
            return View(personalizedClass);
        }

        // POST: PersonalizedClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,UserMail,PublicationDate,RessourceName,Url,Libelle,Legende,Path")] PersonalizedClass personalizedClass)
        {
            if (id != personalizedClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalizedClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalizedClassExists(personalizedClass.Id))
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
            return View(personalizedClass);
        }

        // GET: PersonalizedClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedClass = await _context.Personalizeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalizedClass == null)
            {
                return NotFound();
            }

            return View(personalizedClass);
        }

        // POST: PersonalizedClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notifications = _context.Notifications.Where(n => n.PersonalizedClass.Id == id).FirstOrDefault();
            var personalizedClass = await _context.Personalizeds.FindAsync(id);
            var publications = await _context.Publications.FindAsync(id);
            var ressources = await _context.Ressource.FindAsync(id);

            if(notifications != null)
            {
                _context.Notifications.Remove(notifications);
            }
            _context.Personalizeds.Remove(personalizedClass);
            _context.Publications.Remove(publications);
            _context.Ressource.Remove(ressources);
            await _context.SaveChangesAsync();
            if (User.IsInRole("Utilisateur") || User.IsInRole("Moderateur"))
                return RedirectToAction("MesPublications","Membres");
            
            return RedirectToAction("Index", "Home");
        }

        private bool PersonalizedClassExists(int id)
        {
            return _context.Personalizeds.Any(e => e.Id == id);
        }
    }
}
