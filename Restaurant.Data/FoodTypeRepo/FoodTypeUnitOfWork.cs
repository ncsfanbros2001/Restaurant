using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.FoodTypeRepo
{
    public class FoodTypeUnitOfWork : IFoodTypeUnitOfWork
    {
        private readonly DatabaseContext _db;

        public FoodTypeUnitOfWork(DatabaseContext db)
        {
            _db = db;
            FoodTypeRepository = new FoodTypeRepository(db);
        }

        public IFoodTypeRepository FoodTypeRepository { get; private set; }

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
