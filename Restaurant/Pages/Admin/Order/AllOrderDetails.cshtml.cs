using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

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
    }
}
