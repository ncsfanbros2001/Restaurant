using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.CategoryPages
{
    public class CategoryIndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IEnumerable<Category> Category { get; set; }

        public CategoryIndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet()
        {
            Category = _uow.CategoryRepository.GetAll();
        }
    }
}
