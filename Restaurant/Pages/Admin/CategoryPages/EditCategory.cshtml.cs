using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.CategoryRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.CategoryPages
{
    [BindProperties]
    public class EditCategoryModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public Category Category { get; set; }

        public EditCategoryModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            Category = _uow.CategoryRepository.GetFirstOrDefault(u => u.Id == id);
        }

        public IActionResult OnPost() {
            if (ModelState.IsValid)
            {
                _uow.CategoryRepository.Update(Category);
                _uow.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToPage("CategoryIndex");
            }
            return Page();
        }
    }
}
