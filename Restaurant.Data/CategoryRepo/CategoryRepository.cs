using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.CategoryRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _db;

        public CategoryRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDB = _db.Categories.FirstOrDefault(u => u.Id == category.Id);
            objFromDB.Name = category.Name;
            objFromDB.DisplayOrder = category.DisplayOrder;
        }
    }
}
