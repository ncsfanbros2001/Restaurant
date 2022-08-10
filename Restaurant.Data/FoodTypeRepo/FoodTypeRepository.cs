using Restaurant.Data.Repository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.FoodTypeRepo
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly DatabaseContext _db;

        public FoodTypeRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FoodType foodType)
        {
            var objFromDB = _db.FoodTypes.FirstOrDefault(u => u.Id == foodType.Id);
            objFromDB.Name = foodType.Name;
        }
    }
}
