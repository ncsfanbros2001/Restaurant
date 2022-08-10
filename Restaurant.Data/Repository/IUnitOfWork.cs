using Restaurant.Data.CategoryRepo;
using Restaurant.Data.FoodTypeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IFoodTypeRepository FoodTypeRepository { get; }
        void Save();
    }
}
