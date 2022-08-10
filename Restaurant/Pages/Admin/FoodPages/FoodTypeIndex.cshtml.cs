using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.Repository;
using Restaurant.Models;

namespace Restaurant.Pages.Admin.FoodTypePages
{
    public class FoodTypeIndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IEnumerable<FoodType> FoodType { get; set; }

        public FoodTypeIndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnGet()
        {
            FoodType = _uow.FoodTypeRepository.GetAll();
        }
    }
}
