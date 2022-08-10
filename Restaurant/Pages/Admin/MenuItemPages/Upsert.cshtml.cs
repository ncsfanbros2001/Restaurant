using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.MenuItemPages
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public MenuItem MenuItem { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork uow)
        {
            _uow = uow;
            MenuItem = new();
        }

        public void OnGet()
        {
            CategoryList = _uow.CategoryRepository.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            FoodTypeList = _uow.FoodTypeRepository.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public IActionResult OnPost() {
            //if (ModelState.IsValid)
            //{
            //    _uow.FoodTypeRepository.Add(MenuItem);
            //    _uow.Save();
            //    TempData["success"] = "Food Created Successfully";
            //    return RedirectToPage("FoodTypeIndex");
            //}
            return Page();
        }
    }
}
