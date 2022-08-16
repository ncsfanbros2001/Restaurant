using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Pages.Customers.HomePage
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public MenuItem MenuItem { get; set; }
        [Range(1, 100, ErrorMessage = "Please type in between 1 and 100")]
        public int Count { get; set; }

        public void OnGet(int id)
        {
            MenuItem = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == id, includeProperties:"Category,FoodType");
        }
    }
}
