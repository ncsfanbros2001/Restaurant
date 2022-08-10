using Restaurant.Data.CategoryRepo;
using Restaurant.Data.FoodTypeRepo;
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
            FoodTypeRepository = new FoodTypeRepository(db);
            CategoryRepository = new CategoryRepository(db);
        }

        public IFoodTypeRepository FoodTypeRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

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
