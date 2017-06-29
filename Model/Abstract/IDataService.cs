using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IDataService<T> : IDisposable where T : class
    {
        //void Dispose(bool disposing);
        bool Remove(int Id);
        bool Add(T entity);
        bool Update(int oldId, T entity);
        long Count { get; }
        void Dispose();
    }
}
