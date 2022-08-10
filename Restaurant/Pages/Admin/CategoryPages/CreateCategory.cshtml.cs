using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.CategoryRepo;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.CategoryPages
{
    [BindProperties]
    public class CreateCategoryModel : PageModel
    {
        private readonly ICategoryUnitOfWork _uow;

        public Category Category { get; set; }

        public CreateCategoryModel(ICategoryUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost() {
            if (ModelState.IsValid)
            {
                _uow.CategoryRepository.Add(Category);
                _uow.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToPage("CategoryIndex");
            }
            return Page();
        }
    }
}
