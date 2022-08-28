using Restaurant.Data.Repository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.OrderHeaderRepo
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly DatabaseContext _db;

        public OrderHeaderRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeader.Update(orderHeader);
        }

        public void UpdateStatus(int id, string status)
        {
            var orderFromDB = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if(orderFromDB != null)
            {
                orderFromDB.Status = status;
            }
        }
    }
}
