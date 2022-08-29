using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;

        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(string? status = null)
        {
            var orderHeaderList = _uow.OrderHeaderRepository.GetAll(includeProperties:"UserInfo");

            if(status == "cancelled")
            {
                orderHeaderList = orderHeaderList.Where(u => u.Status == StaticDetail.StatusCancelled
                || u.Status == StaticDetail.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    orderHeaderList = orderHeaderList.Where(u => u.Status == StaticDetail.StatusCompleted);
                }
                else
                {
                    if (status == "inProcess")
                    {
                        orderHeaderList = orderHeaderList.Where(u => u.Status == StaticDetail.StatusSubmitted
                        || u.Status == StaticDetail.StatusInProcess);
                    }
                    else
                    {
                        orderHeaderList = orderHeaderList.Where(u => u.Status == StaticDetail.StatusReady);
                    }
                }
            }

            return Json(new { data = orderHeaderList });
        }

    }
}
