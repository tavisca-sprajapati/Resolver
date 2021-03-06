﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolver.Test.Contracts;
using Resolver.Core;

namespace Resolver.Test
{
    [TestClass]
    public class ResolverTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISearchServiceProvider searchProvider = ObjectBuilder.Resolve<ISearchServiceProvider>();

            string result = searchProvider.Search("test");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCtorSelector()
        {
            ISearchService searchService = ObjectBuilder.Resolve<ISearchService>();

            Assert.IsNotNull(searchService);

            var result = searchService.Search("test");

            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public void Initialize()
        {
            var register = new SearchServiceRegister();
            PrepareData();
            
        }
        private void PrepareData()
        {
            var repository = ObjectBuilder.Resolve<INameRepository>("name");
            if (repository != null)
            {
                repository.Add("test");
                repository.Add("code");
            }
        }
    }
}
