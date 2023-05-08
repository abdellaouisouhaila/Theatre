using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Theatre.Models.Theatre;
using Theatre.Models.ViewModels;

namespace Theatre.Controllers
{
    public class SpectaclesController : Controller
    {
        TheatreDbContext _context;

        public SpectaclesController(TheatreDbContext context)
        {
            _context = context;
        }

        // GET: SpectaclesController
        public async Task <ActionResult> Index()
        {
            var theatreDbContext = _context.Spectacles.Include(s => s.Actor);
            return View(await theatreDbContext.ToListAsync());
        }

        // GET: SpectaclesController/Details/5
        public ActionResult Details(int id)
        {
            Spectacle spectacle = _context.Spectacles.Find(id);
            return View(spectacle);
        }

        // GET: SpectaclesController/Create
        public ActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Id");
            return View();
        }

        // POST: SpectaclesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spectacle spectacle)
        {
            try
            {
                _context.Spectacles.Add(spectacle);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpectaclesController/Edit/5
        public ActionResult Edit(int id)
        {
           Spectacle spectacle = _context.Spectacles.Find(id);
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Id");

            return View(spectacle);
        }

        // POST: SpectaclesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,Spectacle spectacle)
        {
            try
            {
                _context.Spectacles.Update(spectacle);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpectaclesController/Delete/5
        public ActionResult Delete(int id)
        {
            Spectacle spectacle = _context.Spectacles.Find(id);
            return View(spectacle);
        }

        // POST: SpectaclesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection,Spectacle spectacle)
        {
            try
            {
                _context.Spectacles.Remove(spectacle);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public async Task<IActionResult> SpectaclesAndTheirActors_UsingModel()
        {
            var actors = _context.Actors.ToList();
            var spectacles = _context.Spectacles.ToList();
            var query_res = from a in actors
                            join s in spectacles
                            on a.Id equals s.Actor.Id
                            select new SpecAct
                            {
                                aName = a.Name,
                                sTitle = s.Title,
                                sGenre = s.Genre,
                                sDate = s.Date,
                                sLieu = s.Lieu
                            };

            return View(query_res);
        }


        public IActionResult SearchBy2()
        {
            var spectacles = _context.Spectacles.ToList();
            ViewBag.Genre = spectacles.Select(item => item.Genre).ToList();

            return View(spectacles);


        }
        [HttpPost]
        public IActionResult SearchBy2(string genre, string title)
        {
            var spectacles = _context.Spectacles.ToList();
            ViewBag.Genre = spectacles.Select(item => item.Genre).ToList();
            ViewBag.Title = "SearchBy2";
            if (!string.IsNullOrEmpty(genre))
            {
                if (genre == "All")
                    spectacles = _context.Spectacles.ToList();
                else
                    spectacles = spectacles.Where(s => s.Genre == genre).ToList();
            }

            if (!string.IsNullOrEmpty(title))
            {
                spectacles = spectacles.Where(m => m.Title.Contains(title)).ToList();
            }
            return View("SearchBy2", spectacles);

        }
    }
}
