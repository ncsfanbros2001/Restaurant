using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Repository;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnviroment;

        public MenuItemController(IUnitOfWork uow, IWebHostEnvironment hostEnviroment)
        {
            _uow = uow;
            _hostEnviroment = hostEnviroment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menuItemList = _uow.MenuItemRepository.GetAll(includeProperties: "Category,FoodType");
            return Json(new {data = menuItemList});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDB = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == id);
            var oldImagePath = Path.Combine(_hostEnviroment.WebRootPath, objFromDB.Image.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _uow.MenuItemRepository.Remove(objFromDB);
            _uow.Save();
            return Json(new { success = true, message = "Delete Success!" });
        }
    }
}
