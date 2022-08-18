using Restaurant.Data.CategoryRepo;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.MenuItemRepo;
using Restaurant.Data.ShoppingCartRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repository
{
    public interface IUnitOfWork
    {
        IMenuItemRepository MenuItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFoodTypeRepository FoodTypeRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        void Save();
    }
}
