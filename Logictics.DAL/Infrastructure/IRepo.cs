using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.DAL.Infrastructure
{
    public interface IRepo<T> where T : class
    {
        int Count { get; }
        bool HasChanges { get; }

        int SaveChanges();

        int Create(T entity, bool persist = true);

        int CreateMulti(IEnumerable<T> entities, bool persist = true);

        T Find(int? id);

        T Get(object id);

        //T GetFirst();
        IEnumerable<T> GetAll();

        IEnumerable<T> GetRange(int skip, int take);

        int Update(T entity, bool persist = true);

        int UpdateMulti(IEnumerable<T> entities, bool persist = true);

        int Delete(T entity, bool persist = true);

        //int Delete(int id, byte[] timeStamp, bool persist = true);

        int DeleteMulti(IEnumerable<T> entities, bool persist = true);
    }
}
