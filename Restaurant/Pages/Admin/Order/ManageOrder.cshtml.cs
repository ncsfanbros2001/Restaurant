using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
using Restaurant.Utility;

namespace Restaurant.Pages.Admin.Order
{
    [Authorize(Roles = $"{StaticDetail.KitchenRole},{StaticDetail.ManagerRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public List<OrderDetailsVM> OrderDetailsVM { get; set; }

        public ManageOrderModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void OnGet()
        {
            OrderDetailsVM = new();

            List<OrderHeader> orderHeaders = _uow.OrderHeaderRepository
                .GetAll(u => u.Status == StaticDetail.StatusSubmitted ||
                u.Status == StaticDetail.StatusInProcess).ToList();

            foreach (OrderHeader item in orderHeaders)
            {
                OrderDetailsVM individual = new OrderDetailsVM()
                {
                    OrderHeader = item,
                    OrderDetails = _uow.OrderDetailsRepository.GetAll(u => u.OrderId == item.Id).ToList()
                };
                OrderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusInProcess);
            _uow.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusReady);
            _uow.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _uow.OrderHeaderRepository.UpdateStatus(orderId, StaticDetail.StatusCancelled);
            _uow.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
