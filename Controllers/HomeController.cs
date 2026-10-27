using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_1.Data;
using MVC_1.Models;

namespace MVC_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Movie movie)
        {
            // Серверна валідація: атрибути моделі вже перевірені при зв'язуванні
            if (!ModelState.IsValid)
                return View(movie);

            if (movie.PosterFile != null && movie.PosterFile.Length > 0)
                movie.PosterFileName = await SavePosterAsync(movie.PosterFile);

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = _context.Movies.Find(id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
                return NotFound();

            var existing = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (existing == null)
                return NotFound();

            // Поточний постер зберігається (також потрібен, якщо форму треба показати повторно)
            movie.PosterFileName = existing.PosterFileName;

            if (!ModelState.IsValid)
                return View(movie);

            if (movie.PosterFile != null && movie.PosterFile.Length > 0)
                movie.PosterFileName = await SavePosterAsync(movie.PosterFile);

            try
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(m => m.Id == movie.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SavePosterAsync(IFormFile posterFile)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(posterFile.FileName).ToLowerInvariant();
            var folder = Path.Combine(_env.WebRootPath, "images", "posters");
            Directory.CreateDirectory(folder);

            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            await posterFile.CopyToAsync(stream);

            return fileName;
        }
    }
}
