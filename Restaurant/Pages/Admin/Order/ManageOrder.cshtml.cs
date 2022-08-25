using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository;

namespace Restaurant.Pages.Admin.Order
{
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public ManageOrderModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void OnGet()
        {
        }
    }
}
