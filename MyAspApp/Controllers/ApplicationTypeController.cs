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
    }
}
