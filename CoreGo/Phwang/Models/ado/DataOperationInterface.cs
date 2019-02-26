using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.Models
{
    public interface DataOperationInterface<T> where T : class, new()
    {
        IEnumerable<T> Get();
        void Create(T item_val);
        void Update(T item_val);
        void Delete(T item_val);
    }
}
