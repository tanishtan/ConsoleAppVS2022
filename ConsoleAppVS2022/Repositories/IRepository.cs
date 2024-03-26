using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.Repositories
{
    public interface IRepository<TEntity,TIdentity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByCriterial(string filterCriteria);
        TEntity FindById(TIdentity id);
        void Upsert(TEntity entity);
        void RemoveById(TIdentity id);

    }
}
