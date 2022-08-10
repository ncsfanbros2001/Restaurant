using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.FoodPages
{
    [BindProperties]
    public class DeleteFoodTypeModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public FoodType FoodType { get; set; }

        public DeleteFoodTypeModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet(int id)
        {
            FoodType = _uow.FoodTypeRepository.GetFirstOrDefault(u => u.Id == id);
        }

        public IActionResult OnPost()
        {
            var trackFromDB = _uow.FoodTypeRepository.GetFirstOrDefault(u => u.Id == FoodType.Id);
            if (trackFromDB != null)
            {
                _uow.FoodTypeRepository.Remove(trackFromDB);
                _uow.Save();
                TempData["success"] = "Food Deleted Successfully";
                return RedirectToPage("FoodTypeIndex");
            }
            else
            {
                return Page();
            }
        }
    }
}
