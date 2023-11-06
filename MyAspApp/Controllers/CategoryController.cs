using Microsoft.AspNetCore.Mvc;
using MyAspApp.Database;
using MyAspApp.Models;

namespace MyAspApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Category.ToList();  
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
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Category.Find(id);
            var category = _db.Category.FirstOrDefault(x => x.Id == id);

            if(category == null) return NotFound();

            return View(category);
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
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
            var category = _db.Category.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _db.Category.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                
                _db.Category.Remove(category);
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
