using Microsoft.AspNetCore.Mvc;
using ProjectFirstSteps.Models;

namespace ProjectFirstSteps.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly MyContext _context;

        public InvitationsController(MyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string mail)
        {
            var invitation = new Invitation("ok@gmail.com",mail,DateTime.Now);
            _context.Add(invitation);
            await _context.SaveChangesAsync();
            return View(invitation);
        }
    }
}
