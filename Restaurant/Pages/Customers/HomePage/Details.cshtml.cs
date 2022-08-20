using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                UserInfoId = claim.Value,
                MenuItem = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == id,
                includeProperties: "Category,FoodType"),
                MenuItemId = id
            };
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {

                _uow.ShoppingCartRepository.Add(ShoppingCart);
                _uow.Save();
                return RedirectToPage("HomeIndex");
            }
            else
            {
                return Page();
            }
        }
    }
}
