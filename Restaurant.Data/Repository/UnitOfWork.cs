using Restaurant.Data.CategoryRepo;
using Restaurant.Data.FoodTypeRepo;
using Restaurant.Data.MenuItemRepo;
using Restaurant.Data.OrderDetailsRepo;
using Restaurant.Data.OrderHeaderRepo;
using Restaurant.Data.ShoppingCartRepo;
using Restaurant.Data.UserInfoRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _db;

        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
            MenuItemRepository = new MenuItemRepository(_db);
            FoodTypeRepository = new FoodTypeRepository(_db);
            CategoryRepository = new CategoryRepository(_db);
            ShoppingCartRepository = new ShoppingCartRepository(_db);
            OrderHeaderRepository = new OrderHeaderRepository(_db);
            OrderDetailsRepository = new OrderDetailsRepository(_db);
            UserInfoRepository = new UserInfoRepository(_db);
        }

        public IMenuItemRepository MenuItemRepository { get; private set; }

        public IFoodTypeRepository FoodTypeRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }

        public IOrderDetailsRepository OrderDetailsRepository { get; private set; }

        public IUserInfoRepository UserInfoRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
