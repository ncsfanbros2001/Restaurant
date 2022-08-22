using Restaurant.Data.Repository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.UserInfoRepo
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        private readonly DatabaseContext _db;

        public UserInfoRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        
    }
}
