using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.CategoryRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.CategoryPages
{
    [BindProperties]
    public class DeleteCategoryModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public Category Category { get; set; }

        public DeleteCategoryModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            Category = _uow.CategoryRepository.GetFirstOrDefault(u => u.Id == id);
        }

        public IActionResult OnPost()
        {
            var trackFromDB = _uow.CategoryRepository.GetFirstOrDefault(u => u.Id == Category.Id);
            if (trackFromDB != null)
            {
                _uow.CategoryRepository.Remove(trackFromDB);
                _uow.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToPage("CategoryIndex");
            }
            else
            {
                return Page();
            }
        }
    }
}
