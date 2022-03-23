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
    public class PhotoVideosController : Controller
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoVideosController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PhotoVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoVideos.ToListAsync());
        }

        // GET: PhotoVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoVideo = await _context.PhotoVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photoVideo == null)
            {
                return NotFound();
            }

            return View(photoVideo);
        }

        // GET: PhotoVideos/Create
        public IActionResult Create()
        {
            PhotoVideo photoVideo = new PhotoVideo();
            return PartialView("_PicMovies", photoVideo);
        }

        // POST: PhotoVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhotoVideo photoVideo)
        {
            if (ModelState.IsValid)
            {
                string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".mp4", ".avi" };
                var ext = Path.GetExtension(photoVideo.formFile.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    // The extension is invalid ... discontinue processing the file
                }
                else
                {
                    var size = photoVideo.formFile.Length;
                    if (size > 0)
                    {
                        string folder = "Medias/Publications/";
                        string filePath = folder + photoVideo.formFile.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                        await photoVideo.formFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        photoVideo.taille = size;
                        photoVideo.path = "/" + filePath;
                        _context.Add(photoVideo);

                        var EmailSession = HttpContext.Session.GetString("email");
                        Publication publication = new Publication
                        {
                            dateDePublication = DateTime.Now,
                            Ressource = photoVideo,
                            Membre = _context.Membres.Find(EmailSession)
                        };
                        _context.Add(publication);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                
            }
            
            return View(photoVideo);
        }

        // GET: PhotoVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoVideo = await _context.PhotoVideos.FindAsync(id);
            if (photoVideo == null)
            {
                return NotFound();
            }
            return View(photoVideo);
        }

        // POST: PhotoVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("legende,taille,path,Id,nomRessource")] PhotoVideo photoVideo)
        {
            if (id != photoVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoVideoExists(photoVideo.Id))
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
            return View(photoVideo);
        }

        // GET: PhotoVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoVideo = await _context.PhotoVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photoVideo == null)
            {
                return NotFound();
            }

            return View(photoVideo);
        }

        // POST: PhotoVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoVideo = await _context.PhotoVideos.FindAsync(id);
            _context.PhotoVideos.Remove(photoVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoVideoExists(int id)
        {
            return _context.PhotoVideos.Any(e => e.Id == id);
        }
    }
}
