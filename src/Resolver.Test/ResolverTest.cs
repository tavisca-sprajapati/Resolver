using System;
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
        [TestInitialize]
        public void Initialize()
        {
            var register = new SearchServiceRegister();
            var repository = ObjectBuilder.Resolve<INameRepository>();
            if (repository != null)
            {
                repository.Add("test");
                repository.Add("code");
            }
        }
    }
}
