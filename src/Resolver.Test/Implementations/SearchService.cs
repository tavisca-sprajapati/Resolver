﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resolver.Core;
using Resolver.Test.Contracts;

namespace Resolver.Test.Implementations
{
    public class SearchService : ISearchService
    {
        INameRepository _nameRepository;

        [DefaultCtor]
        public SearchService()
        {

        }
        public SearchService(string lessUsedParam)
        {

        }
        [CtorSelector("withNameRepository")]
        public SearchService([Dependency("name")]INameRepository nameRepository)
        {
            _nameRepository = nameRepository;
        }
        public string Search(string name)
        {
            if (_nameRepository != null)
            {
                string repoName = _nameRepository.Get(name);
                return name + " " + repoName;
            }
            return "No data";
        }
    }
}
