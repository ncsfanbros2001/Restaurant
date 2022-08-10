using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.FoodPages
{
    [BindProperties]
    public class EditFoodTypeModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public FoodType FoodType { get; set; }

        public EditFoodTypeModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            FoodType = _uow.FoodTypeRepository.GetFirstOrDefault(u => u.Id == id);
        }

        public IActionResult OnPost() {
            if (ModelState.IsValid)
            {
                _uow.FoodTypeRepository.Update(FoodType);
                _uow.Save();
                TempData["success"] = "Food Updated Successfully";
                return RedirectToPage("FoodTypeIndex");
            }
            return Page();
        }
    }
}
