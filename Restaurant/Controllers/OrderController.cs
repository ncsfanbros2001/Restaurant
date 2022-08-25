using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Repository;

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
        public IActionResult Get()
        {
            var orderHeaderList = _uow.OrderHeaderRepository.GetAll(includeProperties:"UserInfo");
            return Json(new { data = orderHeaderList });
        }

    }
}
