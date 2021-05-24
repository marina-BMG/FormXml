using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityServices
{
    public interface IEntityUIService<T>
    {
        List<T> ListAll();

        T Find(int id);

        void Delete(int id);

        void Modify(T entity);

        void Add(T entity);
    }
}
