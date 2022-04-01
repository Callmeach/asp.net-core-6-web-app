#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectFirstSteps.Models;

namespace ProjectFirstSteps.Controllers
{
    public class MembresController : Controller
    {
        private readonly MyContext _context;
        public string thisUser = "";
        //public static List<Claim> claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
        //public string thisUser = claims?.FirstOrDefault(x => x.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;

        public MembresController(MyContext context)
        {
            //var thisUser = HttpContext.Session.GetString("email");
            var thisUser = "";
            _context = context;
            
            //Here Emmanuel Codes

            List<MembreMembre> amitie = _context.MembreMembres.Where(m => m.MailMembre1 == thisUser || m.MailMembre2 == thisUser).ToList();
            // return View(amitie);
            List<Membre> amis = new();
            foreach (var item in amitie)
            {
                if (item.MailMembre2 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => m.Email == item.MailMembre1));
                }
                else if (item.MailMembre1 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => m.Email == item.MailMembre2));
                }
            }
        }

        public ICollection<Membre> GetAmis()
        {
            var thisUser = HttpContext.Session.GetString("email");
            List<MembreMembre> amitie = _context.MembreMembres.Where(m => m.MailMembre1 == thisUser || m.MailMembre2 == thisUser).ToList();
            // return View(amitie);
            List<Membre> amis = new();
            foreach (var item in amitie)
            {
                if (item.MailMembre2 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => m.Email == item.MailMembre1));
                }
                else if (item.MailMembre1 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => m.Email == item.MailMembre2));
                }
            }

            return amis;
        }

        public IActionResult Welcome()
        {
            return View();
        }

        // GET: Publications
        public async Task<IActionResult> MesPublications()
        {
            return View("MesPublications",await _context.Personalizeds.ToListAsync());
        }

        //Recuperer les notifications en passant le mail de l'utilisateur en parametre
        public async Task<IActionResult> PublicationsParam(string id)
        {
            var model = await _context.Personalizeds.Where(m => m.UserMail == id).OrderByDescending(i => i.PublicationDate).ToListAsync();
            return View("NewView", model);
        }

        public async Task <IActionResult> Signaler(int id)
        {
            var publication = await _context.Personalizeds.Where(m => m.Id == id).SingleOrDefaultAsync();
            return View(publication);
        }

        [HttpPost, ActionName("Signaler")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignalerConfirmed(int id)
        {
            var notification = new Notifications
            {
                Contenu = "Votre publication a été signalée. Veuillez vous conformer aux conditions d'utilisation de NetAtlas.",
                MembreEmail = await _context.Personalizeds.Where(i => i.Id == id).Select(e => e.UserMail).FirstOrDefaultAsync(),
                PersonalizedClass = await _context.Personalizeds.FindAsync(id),
                IsAdvertisement = true
            };
            _context.Add(notification);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListeNotifications()
        {
            var EmailSession = HttpContext.Session.GetString("email");
            var notifications = await _context.Notifications
                .Where(m => m.MembreEmail == EmailSession)
                .Include(p => p.PersonalizedClass).ToListAsync();
            return View(notifications);
        }

        public IActionResult ValiderInscription(string id)
        {
            var membre = _context.Membres.Find(id);
            membre.IsActivated = true;
            Notifications notifications = new Notifications
            {
                Contenu = "Votre inscription a été validée. Bienvenue sur NetAtlas!",
                Membre = membre,
            };
            _context.Add(notifications);
            _context.SaveChanges();
            return RedirectToAction("Admin","Home");
        }

        //Recuperer la liste des membres signalés 3 fois (au moins)
        public IActionResult ComptesABloquer()
        {
            List<object> liste = new List<object>();
            var listeEmail = _context.Notifications.Select(e => e.MembreEmail).ToList();
            var groupe = listeEmail.GroupBy(i => i);
            foreach(var item in groupe)
            {
                if(item.Count() > 2)
                {
                    liste.Add(item.Key);
                }
            }
            ViewBag.mails = liste;
            return View();
        }

        public IActionResult Reinitialiser(string id)
        {
            return View();
        }

        // GET: Membres
        [Authorize(Roles = "Moderateur,Administrateur")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Membres.Include(r => r.Role).ToListAsync());
        }

        // GET: Membres/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .FirstOrDefaultAsync(m => m.Email == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // GET: Membres/Create
        //[Authorize(Roles = "Administrateur")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Nom,Prenom,Password,Role")] Membre membre)
        {

            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(membre.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "This email already exists, try another one");
                    return View(membre);
                }
                else
                {
                    membre.Password = Encryption.Hash(membre.Password);
                    membre.Role = _context.Roles.Where(a => a.Name == "Utilisateur").FirstOrDefault();
                    _context.Add(membre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Welcome));
                }
            }
            return View(membre);
        }

        // GET: Membres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }
            return View(membre);
        }

        // POST: Membres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Nom,Prenom,Password")] Membre membre)
        {
            if (id != membre.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreExists(membre.Email))
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
            return View(membre);
        }

        // GET: Membres/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .FirstOrDefaultAsync(m => m.Email == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // POST: Membres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var membre = await _context.Membres.FindAsync(id);
            var personalized = await _context.Personalizeds.Where(p => p.UserMail == id).ToListAsync();
            _context.Personalizeds.RemoveRange(personalized);
            _context.Membres.Remove(membre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembreExists(string id)
        {
            return _context.Membres.Any(e => e.Email == id);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Membre membre)
        {

            string message = string.Empty;
            var v = _context.Membres.Where(a => a.Email == membre.Email).FirstOrDefault();
            if (v != null)
            {
                if (v.IsActivated)
                {
                    if (string.Compare(Encryption.Hash(membre.Password), v.Password) == 0)
                    {
                        //Role role = new Role();
                        var userName = v.Nom + " " + v.Prenom;
                        var userRole = _context.Roles.Find(v.RoleId).Name;

                        HttpContext.Session.SetString("email", membre.Email);

                        var userClaims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Email, membre.Email),
                        new Claim(ClaimTypes.Role, userRole),
                        };
                        var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var userPrincipal = new ClaimsPrincipal(new[] { identity });

                        await HttpContext.SignInAsync(userPrincipal);

                        var liste = HttpContext.Session.GetString("email");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        message = "Incorrect Password";
                    }
                }
                else
                {
                    message = "Votre inscription n'a pas encore été validée.";
                }
            }
            else
            {
                message = "Incorrect Email";
            }
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("email");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public bool IsEmailExist(string email)
        {
            var v = _context.Membres.Where(a => a.Email == email).FirstOrDefault();
            return v != null;
        }

        //Here Emmanuel Codes
        

        public ICollection<Membre> amis { get; set; }

        // Add a new invitation
        public async Task<IActionResult> Inviter(string email)
        {
            var thisUser = HttpContext.Session.GetString("email");
            if (email == thisUser)
            {
                return BadRequest();
            }

            var membre = await _context.Membres
                .FirstOrDefaultAsync(m => m.Email == email);
            if (membre == null)
            {
                return NotFound();
            }

            // Here, I have fixed the Id of the inviter. It will be replaced with the connected user's id
            var invitation = new Invitation
            (
                 thisUser,
                email,
                DateTime.Now
            );
            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Suggestion));
        }

        // Shows the invitation list sent to the user
        public async Task<IActionResult> ListInvitations(string search)
        {
            var thisUser = HttpContext.Session.GetString("email");
            List<Invitation> invitations = await _context.Invitations.Where(i => i.InvitedMail == thisUser).ToListAsync();
            List<Membre> membreList = new List<Membre>();
            foreach (var item in invitations)
            {
                membreList.Add(_context.Membres.FirstOrDefault(m => m.Email == item.InviterMail));
            }

            if (search != null)
            {
                List<Membre> result = new List<Membre>();
                foreach (var m in membreList)
                {
                    if (m.Prenom.Contains(search) || m.Nom.Contains(search) ||
                        m.Email.Contains(search))
                    {
                        result.Add(m);
                    }
                }
                return View(result);
            }
            return View(membreList);
        }

        //Shows the invitations list sent by the connected user
        public async Task<IActionResult> InvitationsEnvoyees(string search)
        {
            var thisUser = HttpContext.Session.GetString("email");
            List<Invitation> invitations = await _context.Invitations.Where(i => i.InviterMail == thisUser).ToListAsync();
            List<Membre> membreList = new List<Membre>();
            foreach (var item in invitations)
            {
                membreList.Add(_context.Membres.FirstOrDefault(m => m.Email == item.InvitedMail));
            }
            return View(membreList);
        }

        // Return the friend list of the connected user
        public IActionResult Amis(string search)
        {
            var thisUser = HttpContext.Session.GetString("email");
            List<MembreMembre> amitie = _context.MembreMembres.Where(m => m.MailMembre1 == thisUser || m.MailMembre2 == thisUser).ToList();
            List<Membre> amis = new();
            foreach (var item in amitie)
            {
                if (item.MailMembre2 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => (m.Email == item.MailMembre1)
                    ));
                }
                else if (item.MailMembre1 == thisUser)
                {
                    amis.Add(_context.Membres.FirstOrDefault(m => (m.Email == item.MailMembre2)
                    ));
                }
            }
            amis.Remove(_context.Membres.FirstOrDefault(m => m.Email == thisUser));

            if (search != null)
            {
                List<Membre> result = new List<Membre>();
                foreach (var m in amis)
                {
                    if (m.Prenom.Contains(search) || m.Nom.Contains(search) ||
                        m.Email.Contains(search))
                    {
                        result.Add(m);
                    }
                }
                return View(result);
            }

            return View(amis);
        }

        public IActionResult SearchMember(string search)
        {
            return View(_context.Membres.Where(m => m.Prenom.Contains(search)
                  || m.Nom.Contains(search) || m.Email.Contains(search) || search == null
                ));
        }

        // Returns the list of members that are not friends of the connected user
        public async Task<IActionResult> Inconnus(string search)
        {
            var thisUser = HttpContext.Session.GetString("email");
            string[] invites = await _context.Invitations.
                Where(i => i.InviterMail == thisUser)
                .Select(i => i.InvitedMail).ToArrayAsync();

            string[] idAmis = await _context.MembreMembres.
                Where(m => m.MailMembre2 == thisUser).
                Select(m => m.MailMembre1).ToArrayAsync();

            string[] idAmis2 = await _context.MembreMembres.
                Where(m => m.MailMembre2 == thisUser).
                Select(m => m.MailMembre1).ToArrayAsync();

            List<string> noFriends = new();
            noFriends.AddRange(invites);
            noFriends.AddRange(idAmis);
            noFriends.AddRange(idAmis2);

            List<Membre> inconnus = await _context.Membres
                .Where(m => !noFriends.Contains(m.Email)).ToListAsync();
            // I remove the connected user from the list
            inconnus.Remove(_context.Membres.FirstOrDefault(m => m.Email == thisUser));

            if (search != null)
            {
                List<Membre> result = new List<Membre>();
                foreach (var m in inconnus)
                {
                    if (m.Prenom.Contains(search) || m.Nom.Contains(search) ||
                        m.Email.Contains(search))
                    {
                        result.Add(m);
                    }
                }
                return View(result);
            }

            return View(inconnus);
        }


        // Returns the list of members that are not friends of the connected user
        public async Task<IActionResult> Suggestion(string search)
        {
            var thisUser = HttpContext.Session.GetString("email");

            string[] invites = await _context.Invitations.
                Where(i => i.InviterMail == thisUser)
                .Select(i => i.InvitedMail).ToArrayAsync();
            
            string[] idAmis = await _context.MembreMembres.
                Where(m => m.MailMembre2 == thisUser).
                Select(m => m.MailMembre1).ToArrayAsync();

            string[] idAmis2 = await _context.MembreMembres.
                Where(m => m.MailMembre2 == thisUser).
                Select(m => m.MailMembre1).ToArrayAsync();

            List<string> noFriends = new();
            noFriends.AddRange(invites);
            noFriends.AddRange(idAmis);
            noFriends.AddRange(idAmis2);

            List<Membre> inconnus = await _context.Membres
                .Where(m => (m.RoleId != 1) && (!noFriends.Contains(m.Email)) ).ToListAsync();
            // I remove the connected user from the list
            inconnus.Remove(_context.Membres.FirstOrDefault(m => m.Email == thisUser));

            if (search != null)
            {
                List<Membre> result = new List<Membre>();
                foreach (var m in inconnus)
                {
                    if (m.Prenom.Contains(search) || m.Nom.Contains(search) ||
                        m.Email.Contains(search))
                    {
                        result.Add(m);
                    }
                }
                return View(result);
            }

            return View(inconnus);
        }


        // Accept an invitation
        public IActionResult Accepter(string email)
        {
            var thisUser = HttpContext.Session.GetString("email");
            if (email == string.Empty)
            {
                return NotFound();
            }
            if (email == thisUser)
            {
                return BadRequest();
            }

            MembreMembre ami = new MembreMembre
            {
                MailMembre1 = thisUser,
                MailMembre2 = email
            };
            Invitation inv = _context.Invitations.FirstOrDefault(i => i.InviterMail == email && i.InvitedMail == thisUser);

            if (_context.MembreMembres.Any(m => m.MailMembre1 == thisUser && m.MailMembre2 == email))
            {
                return BadRequest();
            }

            _context.MembreMembres.Add(ami);
            _context.Invitations.Remove(inv);
            _context.SaveChanges();

            return RedirectToAction(nameof(Amis));
        }

        // Remove a friend from the connected user's friend list
        public IActionResult Remove(string email)
        {
            var thisUser = HttpContext.Session.GetString("email");
            var ex_amis = _context.MembreMembres.
                FirstOrDefault(m => (m.MailMembre2 == email && m.MailMembre1 == thisUser)
                || (m.MailMembre2 == thisUser && m.MailMembre1 == email));

            if (ex_amis == null)
            {
                RedirectToAction(nameof(Amis));
            }

            _context.Remove(ex_amis);
            _context.SaveChanges();

            return RedirectToAction(nameof(Amis));
        }

        // Refuse an invitation sent to the connected user
        public IActionResult Refuse(string email)
        {
            var thisUser = HttpContext.Session.GetString("email");
            var inv = _context.Invitations.
                FirstOrDefault(i => (i.InviterMail == email && i.InvitedMail == thisUser));

            if (inv == null)
            {
                RedirectToAction(nameof(ListInvitations));
            }

            _context.Remove(inv);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListInvitations));
        }

        // Cancel an invitation sent by the connected user
        public IActionResult AnnulerInvitation(string email)
        {
            var thisUser = HttpContext.Session.GetString("email");
            var inv = _context.Invitations.
                FirstOrDefault(i => (i.InvitedMail == email && i.InviterMail == thisUser));

            if (inv == null)
            {
                RedirectToAction(nameof(ListInvitations));
            }

            _context.Remove(inv);
            _context.SaveChanges();

            return RedirectToAction(nameof(InvitationsEnvoyees));
        }


    }

}
