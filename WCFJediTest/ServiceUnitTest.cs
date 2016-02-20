using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WCFJediTest.ServiceReference;
using WCFJedi;

namespace WCFJediTest
{
    [TestClass]
    public class WebServiceTest
    {
        public WebServiceTest()
        {

        }

        [TestMethod]
        public void TestJedis()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<JediWS> jedis = client.getJedis();
            Assert.IsNotNull(jedis);
            JediWS yoda = jedis.Find(x => x.Nom.Equals("Yoda"));
            Assert.IsNotNull(yoda);
            client.Close();
        }
    }
}
