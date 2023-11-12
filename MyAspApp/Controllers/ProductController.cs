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
        private readonly IWebHostEnvironment _environment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
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

            if (id == null)
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
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _environment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    //creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    _db.Product.Add(productVM.Product);
                    _db.SaveChanges();

                }
                else
                {
                    var product = _db.Product.AsNoTracking().FirstOrDefault(x=>x.Id == productVM.Product.Id);

                    if(files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, product.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;

                    }
                    else
                    {
                        productVM.Product.Image = product.Image;
                    }

                    _db.Product.Update(productVM.Product);

                }

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
