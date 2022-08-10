using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.CategoryRepo
{
    public class CategoryUnitOfWork : ICategoryUnitOfWork
    {
        private readonly DatabaseContext _db;

        public CategoryUnitOfWork(DatabaseContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
        }
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
