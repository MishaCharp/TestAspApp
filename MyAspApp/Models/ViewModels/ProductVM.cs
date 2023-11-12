using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyAspApp.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
    }
}
