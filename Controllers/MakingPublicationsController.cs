using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjectFirstSteps.Models;

namespace ProjectFirstSteps.Controllers
{
    public class MakingPublicationsController : Controller
    {
        private readonly MyContext _context;

        public MakingPublicationsController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MakingPublications model)
        {
            //model.ressource.nomRessource = "Combined";
            model.photoVideo.path = "chemin";
                //_context.AddRange(model.message.libelle, model.lien.Url, model.photoVideo.taille);
            _context.Add(model.message);
            _context.Add(model.lien);
            _context.Add(model.photoVideo);
            
            //_context.Add(model.ressource);
            //_context.Messages.Add(model.message);
            //_context.Liens.Add(model.lien);
            //_context.PhotoVideos.Add(model.photoVideo);
            //_context.Publications.Add(model.publication);
            await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            
            //return View(model);
           
        }
    }
}
