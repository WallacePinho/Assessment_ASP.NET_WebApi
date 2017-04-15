using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core {
    interface IRepository<T> {
        ICollection<T> GetAll();
        bool Insert(T entity);
        bool Update(T entity);
        T Get(int id);
        bool Delete(int id);
    }
}
