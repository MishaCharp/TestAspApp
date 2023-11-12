using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAspApp.Database;
using MyAspApp.Models;
using MyAspApp.Models.ViewModels;

namespace MyAspApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> products = _db.Product.Include(x => x.Category).ToList();
            return View(products);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            //List<SelectListItem> categoryListDown = _db.Category.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //}).ToList();

            //ViewBag.CategoryDropDown = categoryListDown;

            //var product = new Product();

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            if(id == null)
            {
                return View(productVM);
    }
            else
            {
                productVM.Product = _db.Product.FirstOrDefault(x => x.Id == id);
                if (productVM.Product == null) return NotFound();
                else return View(productVM);
}
        }

        //POST - UPSERT
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Upsert(Product product)
{
    if (ModelState.IsValid)
    {
        _db.Product.Add(product);
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
    var product = _db.Product.FirstOrDefault(x => x.Id == id);

    if (product == null) return NotFound();

    return View(product);
}

//POST - Delete
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult DeletePost(int? id)
{
    var product = _db.Product.FirstOrDefault(x => x.Id == id);
    if (product != null)
    {

        _db.Product.Remove(product);
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
