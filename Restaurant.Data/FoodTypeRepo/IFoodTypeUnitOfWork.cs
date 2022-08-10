using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.FoodTypeRepo
{
    public interface IFoodTypeUnitOfWork
    {
        IFoodTypeRepository FoodTypeRepository { get; }
        void Save();
    }
}
