using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Repository;
using Restaurant.Utility;
using System.Security.Claims;

namespace Restaurant.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public ShoppingCartViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(StaticDetail.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(StaticDetail.SessionCart));
                }
                else
                {
                    count = _uow.ShoppingCartRepository.GetAll(u => u.UserInfoId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
