using Restaurant.Data.Repository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.MenuItemRepo
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly DatabaseContext _db;

        public MenuItemRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDB = _db.MenuItems.FirstOrDefault(u => u.Id == menuItem.Id);
            objFromDB.Name = menuItem.Name;
            objFromDB.Description = menuItem.Description;
            objFromDB.Price = menuItem.Price;
            objFromDB.CategoryID = menuItem.CategoryID;
            objFromDB.FoodTypeID = menuItem.FoodTypeID;
            if (objFromDB.Image != null)
            {
                objFromDB.Image = menuItem.Image;
            }
        }
    }
}
