using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WCFJediTest.ServiceReference;
using WCFJedi;
using System.Linq;

namespace WCFJediTest
{
    [TestClass]
    public class WebServiceTest
    {
        [TestMethod]
        public void TestServicesCaracteristiques()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<CaracteristiqueWS> caracs = client.getCaracteristiquesOf("Anakin Skywalker");
            Assert.IsNotNull(caracs);
            Assert.AreEqual(4, caracs.Count);
            CaracteristiqueWS chosen = caracs.Find(x => x.Nom.Equals("The chosen one"));
            Assert.IsNotNull(chosen);
            client.Close();
        }

        [TestMethod]
        public void TestServiceJedis()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<JediWS> jedis = client.getJedis();
            Assert.IsNotNull(jedis);
            JediWS yoda = jedis.Find(x => x.Nom.Equals("Yoda"));
            Assert.IsNotNull(yoda);

            /** TEST AJOUT */
            /*
                client.addJedi(yoda);
                Assert.AreEqual(2, client.getJedis().Where(x => x.Nom.Equals("Yoda")).ToList().Count());
            **/

            client.Close();
        }

        [TestMethod]
        public void TestServicesStades()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<StadeWS> stades = client.getStades();
            Assert.IsNotNull(stades);
            StadeWS kamino = stades.Find(x => x.Planete.Equals("Kamino"));
            Assert.IsNotNull(kamino);
            client.Close();
        }

        [TestMethod]
        public void TestServicesMatches()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<MatchWS> matches = client.getMatches();
            Assert.IsNotNull(matches);
            MatchWS combat = matches.Find(x => x.Jedi1.Nom.Equals("Obi Wan Kenobi") && x.Jedi2.Nom.Equals("Yoda"));
            Assert.IsNotNull(combat);
            client.Close();
        }

        [TestMethod]
        public void TestServicesTournois()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<TournoiWS> tournois = client.getTournois();
            Assert.IsNotNull(tournois);
            TournoiWS combat = tournois.Find(x => x.Matches[0].Jedi1.Nom.Equals("Obi Wan Kenobi") && x.Matches[0].Jedi2.Nom.Equals("Yoda"));
            Assert.IsNotNull(combat);
            client.Close();
        }
    }
}
