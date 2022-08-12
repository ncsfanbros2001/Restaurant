using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Repository;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MenuItemController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menuItemList = _uow.MenuItemRepository.GetAll();
            return Json(new {data = menuItemList});
        }
    }
}
