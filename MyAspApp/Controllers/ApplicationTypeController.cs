using Microsoft.AspNetCore.Mvc;
using MyAspApp.Database;
using MyAspApp.Models;

namespace MyAspApp.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ApplicationType> categories = _db.ApplicationType.ToList();
            return View(categories);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType category)
        {
            _db.ApplicationType.Add(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Category.Find(id);
            var application = _db.ApplicationType.FirstOrDefault(x => x.Id == id);

            if (application == null) return NotFound();

            return View(application);
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType application)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Update(application);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        //GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Category.Find(id);
            var application = _db.ApplicationType.FirstOrDefault(x => x.Id == id);

            if (application == null) return NotFound();

            return View(application);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var application = _db.ApplicationType.FirstOrDefault(x => x.Id == id);
            if (application != null)
            {

                _db.ApplicationType.Remove(application);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
