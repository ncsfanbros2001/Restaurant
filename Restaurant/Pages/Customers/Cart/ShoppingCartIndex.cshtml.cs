using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using System.Security.Claims;

namespace Restaurant.Pages.Customers.Cart
{
    [Authorize]
    public class ShoppingCartIndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartLists { get; set; }
        private readonly IUnitOfWork _uow;

        public ShoppingCartIndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartLists = _uow.ShoppingCartRepository.GetAll(filter: u => u.UserInfoId == claim.Value);
            }
        }
    }
}
