using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.MenuItemPages
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnviroment;

        public MenuItem MenuItem { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork uow, IWebHostEnvironment hostEnviroment)
        {
            _uow = uow;
            MenuItem = new();
            _hostEnviroment = hostEnviroment;
        }

        public void OnGet(int? id)
        {
            if(id != null) // Edit request
            {
                MenuItem = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == id);
            }
            CategoryList = _uow.CategoryRepository.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            FoodTypeList = _uow.FoodTypeRepository.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public IActionResult OnPost() {
            string webRootPath = _hostEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0) // Create
            {
                string newFileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuItem");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, newFileName + extension),
                    FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = @"images\menuItem\" + newFileName + extension;
                _uow.MenuItemRepository.Add(MenuItem);
                _uow.Save();
            }
            else // Update
            {
                var objFromDB = _uow.MenuItemRepository.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                if (files.Count > 0)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuItem");
                    var extension = Path.GetExtension(files[0].FileName);

                    var oldImagePath = Path.Combine(webRootPath, objFromDB.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, newFileName + extension),
                        FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItem.Image = @"images\menuItem\" + newFileName + extension;
                }
                else
                {
                    MenuItem.Image = objFromDB.Image;
                }
                _uow.MenuItemRepository.Update(MenuItem);
                _uow.Save();
            }
            return RedirectToPage("MenuItemIndex");
        }
    }
}
