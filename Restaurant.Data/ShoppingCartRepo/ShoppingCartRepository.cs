using Restaurant.Data.Repository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.ShoppingCartRepo
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly DatabaseContext _db;

        public ShoppingCartRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
