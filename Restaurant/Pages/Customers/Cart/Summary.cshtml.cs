using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using Restaurant.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace Restaurant.Pages.Customers.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

        private readonly IUnitOfWork _uow;

        public SummaryModel(IUnitOfWork uow)
        {
            _uow = uow;
            OrderHeader = new OrderHeader();
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
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                UserInfo userInfo = _uow.UserInfoRepository.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = userInfo.FirstName + " " + userInfo.LastName;
                OrderHeader.PhoneNumber =  userInfo.PhoneNumber;
            }
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _uow.ShoppingCartRepository.GetAll(filter: u => u.UserInfoId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                OrderHeader.Status = StaticDetail.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() 
                    + " " + OrderHeader.PickUpTime.ToShortTimeString());
                _uow.OrderHeaderRepository.Add(OrderHeader);
                _uow.Save();

                foreach(var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        MenuItemId = item.MenuItem.Id,
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _uow.OrderDetailsRepository.Add(orderDetails);
                }

                //int quantity = ShoppingCartList.ToList().Count;
                //_uow.ShoppingCartRepository.RemoveRange(ShoppingCartList);
                _uow.Save();

                var domain = "https://localhost:44390/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"/Customers/Cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "Customers/Cart/ShoppingCartIndex",
                };

                // line items
                foreach(var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name
                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _uow.Save();
                return new StatusCodeResult(303);
            }
            return Page();
        }
    }
}
