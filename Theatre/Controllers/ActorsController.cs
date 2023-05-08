using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Theatre.Models.Theatre;

namespace Theatre.Controllers
{
    public class ActorsController : Controller
    {
        TheatreDbContext _context;

        public ActorsController(TheatreDbContext context)
        {
            _context = context;
        }
        // GET: ActorsController
        public ActionResult Index()
        {
            return View(_context.Actors.ToList());
        }

        // GET: ActorsController/Details/5
        public ActionResult Details(int id)
        {
            Actor actor = _context.Actors.Find(id);
            return View(actor);
        }

        // GET: ActorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            try
            {
                _context.Actors.Add(actor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorsController/Edit/5
        public ActionResult Edit(int id)
        {
            Actor actor = _context.Actors.Find(id);
            return View(actor);
        }

        // POST: ActorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,Actor actor)
        {
            try
            {
                _context.Actors.Update(actor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorsController/Delete/5
        public ActionResult Delete(int id)
        {
            Actor actor = _context.Actors.Find(id);
            return View(actor);
        }

        // POST: ActorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection,Actor actor)
        {
            try
            {
                _context.Actors.Remove(actor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ActorsAndTheirSpectacles()
        {
            var spectacles = _context.Spectacles.ToList();
            return View(_context.Actors.ToList());
        }

        public IActionResult MySpectacles(int id)
        {

            var actors = _context.Actors.ToList();
            var spectacles = _context.Spectacles.ToList();
            var res = from s in spectacles where s.ActorId == id select s;
            var res2 = _context.Spectacles.Where(s=> s.ActorId == id);
            return View(res2.ToList());
        }
    }
}
