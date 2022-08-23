using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using Restaurant.Utility;
using Stripe.Checkout;

namespace Restaurant.Pages.Customers.Cart
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public int OrderId { get; set; }

        public OrderConfirmationModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            OrderHeader orderHeader = _uow.OrderHeaderRepository.GetFirstOrDefault(u => u.Id == id);
            if (orderHeader.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    orderHeader.Status = StaticDetail.StatusSubmitted;
                    _uow.Save();
                }
            }
            List<ShoppingCart> shoppingCarts =
                _uow.ShoppingCartRepository.GetAll(u => u.UserInfoId == orderHeader.UserId).ToList();
            _uow.ShoppingCartRepository.RemoveRange(shoppingCarts);
            _uow.Save();
            OrderId = id;
        }
    }
}
