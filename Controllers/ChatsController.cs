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
    public class ChatsController : Controller
    {
        private readonly MyContext _context;
        private string thisUser;

        public ChatsController(MyContext context)
        {
            _context = context;
            thisUser = new MembresController(_context).thisUser;
        }

        // GET: Chats
        public IActionResult Index()
        {
            ICollection<Membre> amis = new MembresController(_context).GetAmis();

            return View(amis);
        }

        // GET: Chats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // GET: Chats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MailMembre1,MailMembre2,MessageDate,Containt")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chat);
        }

        // GET: Chats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return View(chat);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MailMembre1,MailMembre2,MessageDate,Containt")] Chat chat)
        {
            if (id != chat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatExists(chat.Id))
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
            return View(chat);
        }

        // GET: Chats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Here Emmanuel's Codes

        public async Task<IActionResult> PrivateChat(string mail, string search)
        {
            thisUser = HttpContext.Session.GetString("email");
            if (mail == string.Empty || mail == thisUser)
            {
                return NotFound();
            }

            if (search != null)
            {
                Chat newChat = new(thisUser, mail, search);
                _context.Chats.Add(newChat);
                _context.SaveChanges();
            }

            List<Chat> chats = await _context.Chats
                .Where(m => (m.MailMembre2 == thisUser && m.MailMembre1 == mail) || (m.MailMembre2 == mail && m.MailMembre1 == thisUser))
                .OrderBy(m => m.MessageDate)
                .ToListAsync();

            Membre membre1 = _context.Membres.FirstOrDefault(m => m.Email == thisUser);
            ViewData["Membre1"] = membre1.Nom + " " + membre1.Prenom + " ";
            Membre membre2 = _context.Membres.FirstOrDefault(m => m.Email == mail);
            ViewData["Membre2"] = membre2.Nom + " " + membre2.Prenom + " ";

            ViewData["thisUser"] = thisUser;

            return View(chats);
        }

        private bool ChatExists(int id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}
