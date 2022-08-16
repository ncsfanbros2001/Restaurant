using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Customers.HomePage
{
    public class HomeIndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public HomeIndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            MenuItemList = _uow.MenuItemRepository.GetAll(includeProperties: "Category,FoodType");
            CategoryList = _uow.CategoryRepository.GetAll(orderBy: u => u.OrderBy(c => c.DisplayOrder));
        }
    }
}
