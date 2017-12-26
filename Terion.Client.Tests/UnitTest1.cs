using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tierion.Client;

namespace Terion.Client.Tests
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var authClient = new HashClient("micah@blockarray.com", "asdf1234!");
            var result = authClient.Auth().Result;
            Console.WriteLine(result.access_token);
        }
        [TestMethod]
        public void DataClient()
        {
            var client = new TierionDataClient("micah@blockarray.com", "wJduCOz6V3Z6vBj+brsTw21g2InmNfjfOcqxogAvy78=");
            var datastores = client.GetAllDatastores().Result;
            foreach (var datastore in datastores)
            {
                Console.WriteLine(datastore);
            }
        }
    }
}
