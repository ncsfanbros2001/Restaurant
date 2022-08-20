using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using System.Security.Claims;

namespace Restaurant.Pages.Customers.Cart
{
    [Authorize]
    public class ShoppingCartIndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }
        private readonly IUnitOfWork _uow;

        public ShoppingCartIndexModel(IUnitOfWork uow)
        {
            _uow = uow;
            CartTotal = 0;
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _uow.ShoppingCartRepository.GetAll(filter: u => u.UserInfoId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var cartItem in ShoppingCartList)
                {
                    CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
            }
        }
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _uow.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            _uow.ShoppingCartRepository.IncrementCount(cart, 1);
            return RedirectToPage("/Customers/Cart/ShoppingCartIndex");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _uow.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                _uow.ShoppingCartRepository.Remove(cart);
                _uow.Save();
            }
            else
            {
                _uow.ShoppingCartRepository.DecrementCount(cart, 1);
            }
            return RedirectToPage("/Customers/Cart/ShoppingCartIndex");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _uow.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            _uow.ShoppingCartRepository.Remove(cart);
            _uow.Save();
            return RedirectToPage("/Customers/Cart/ShoppingCartIndex");
        }
    }
}
