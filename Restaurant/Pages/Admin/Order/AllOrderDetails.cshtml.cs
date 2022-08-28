using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
using Restaurant.Utility;
using Stripe;

namespace Restaurant.Pages.Admin.Order
{
    public class AllOrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public OrderDetailsVM OrderDeltailsVM { get; set; }

        public AllOrderDetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            OrderDeltailsVM = new()
            {
                OrderHeader = _uow.OrderHeaderRepository
                .GetFirstOrDefault(u => u.Id == id, includeProperties: "UserInfo"),
                OrderDetails = _uow.OrderDetailsRepository.GetAll(u => u.OrderId == id).ToList()
            };
        }
        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusCompleted);
            _uow.Save();
            return RedirectToPage("AllOrderList");
        }

        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _uow.OrderHeaderRepository.GetFirstOrDefault(u => u.Id == orderId);
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            var service = new RefundService();
            Refund refund = service.Create(options);

            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusRefunded);
            _uow.Save();
            return RedirectToPage("AllOrderList");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusCancelled);
            _uow.Save();
            return RedirectToPage("AllOrderList");
        }
    }
}
