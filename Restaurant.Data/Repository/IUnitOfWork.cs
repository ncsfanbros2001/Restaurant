using Restaurant.Data.CategoryRepo;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.MenuItemRepo;
using Restaurant.Data.OrderHeaderRepo;
using Restaurant.Data.OrderDetailsRepo;
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
        ICategoryRepository CategoryRepository { get; }
        IMenuItemRepository MenuItemRepository { get; }
        IFoodTypeRepository FoodTypeRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        void Save();
    }
}
