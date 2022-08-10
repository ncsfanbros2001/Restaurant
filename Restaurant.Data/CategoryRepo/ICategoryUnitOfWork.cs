using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.CategoryRepo
{
    public interface ICategoryUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        void Save();
    }
}
