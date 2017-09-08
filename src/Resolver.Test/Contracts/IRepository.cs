using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Test.Contracts
{
    public interface IRepository<TEntity>
    {
        TEntity Get(TEntity id);
        void Add(TEntity entity);
         
    }
}
