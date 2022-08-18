using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Pages.Customers.HomePage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }

        public void OnGet(int id)
        {
            ShoppingCart = new()
            {
                MenuItem = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType")
            };
        }
    }
}
