using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resolver.Test.Contracts;

namespace Resolver.Test.Implementations
{
    public class SearchServiceProvider : ISearchServiceProvider
    {
        ISearchService _searchService;
        public SearchServiceProvider(ISearchService searchService)
        {
            _searchService = searchService;
        }
        public string Search(string name)
        {
            return _searchService.Search(name);
        }
    }
}
