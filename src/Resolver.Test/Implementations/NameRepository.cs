using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resolver.Test.Contracts;

namespace Resolver.Test.Implementations
{
    public class NameRepository : INameRepository
    {
        private static List<string> _repository = new List<string>();
        public NameRepository()
        {
        }
        public void Add(string entity)
        {
            _repository.Add(entity);
        }
        public string Get(string id)
        {
            return _repository.FirstOrDefault(repo => string.Equals(repo, id));
        }
    }
    public class NullRepository : INameRepository
    {
        public NullRepository()
        {
        }
        public void Add(string entity)
        {
            
        }
        public string Get(string id)
        {
            return string.Empty;
        }
    }
}
