using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.FoodPages
{
    [BindProperties]
    public class CreateFoodTypeModel : PageModel
    {
        private readonly IFoodTypeUnitOfWork _uow;

        public FoodType FoodType { get; set; }

        public CreateFoodTypeModel(IFoodTypeUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost() {
            if (ModelState.IsValid)
            {
                _uow.FoodTypeRepository.Add(FoodType);
                _uow.Save();
                TempData["success"] = "Food Created Successfully";
                return RedirectToPage("FoodTypeIndex");
            }
            return Page();
        }
    }
}
