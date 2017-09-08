using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resolver.Core;
using Resolver.Test.Contracts;
using Resolver.Test.Implementations;

namespace Resolver.Test
{
    internal class SearchServiceRegister : Register
    {
        public SearchServiceRegister()
        {
            For<INameRepository>().Use<NameRepository>("name");
            For<INameRepository>().Use<NullRepository>("null");
            For<ISearchService>().Use<SearchService>();
            For<ISearchServiceProvider>().Use<SearchServiceProvider>();
        }
    }
}
