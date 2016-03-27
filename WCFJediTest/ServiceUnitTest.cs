using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WCFJedi;
using System.Linq;

namespace WCFJediTest
{
    [TestClass]
    public class WebServiceTest
    {
        [TestMethod]
        public void TestServiceCaracteristiques()
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
        public void TestServiceGetJedis()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<JediWS> jedis = client.getJedis();
            Assert.IsNotNull(jedis);
            JediWS yoda = jedis.Find(x => x.Nom.Equals("Yoda"));
            Assert.IsNotNull(yoda);

            client.Close();
        }

        [TestMethod]
        public void TestServiceUpdateJedis()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<JediWS> jedis = client.getJedis();
            Assert.IsNotNull(jedis);
            JediWS yoda = client.getJedis().Find(x => x.Nom.Equals("Yoda"));
            yoda.IsSith = true;
            client.updateJedi(yoda);
            Assert.IsTrue(((JediWS)client.getJedis().Find(x => x.Nom.Equals("Yoda"))).IsSith);
            yoda.IsSith = false;
            client.updateJedi(yoda);
            Assert.IsFalse(((JediWS)client.getJedis().Find(x => x.Nom.Equals("Yoda"))).IsSith);

            client.Close();
        }

        [TestMethod]
        public void TestServiceARJedis()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<JediWS> jedis = client.getJedis();
            int size = jedis.Count;
            Assert.IsNotNull(jedis);
            /* AJOUT */
            JediWS bob = new JediWS(0, "Bob THE TEST", true, new List<CaracteristiqueWS>());
            client.addJedi(bob);
            Assert.AreEqual(size+1, client.getJedis().Count);
            /* SUPPRESSION */
            bob = client.getJedis().Find(x => x.Nom.Equals("Bob THE TEST"));
            client.removeJedi(bob);
            Assert.AreEqual(size, client.getJedis().Count);

            client.Close();
        }

        [TestMethod]
        public void TestServiceGetStades()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<StadeWS> stades = client.getStades();
            Assert.IsNotNull(stades);
            StadeWS kamino = stades.Find(x => x.Planete.Equals("Kamino"));
            Assert.IsNotNull(kamino);
            client.Close();
        }

        [TestMethod]
        public void TestServiceUpdateStades()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<StadeWS> stades = client.getStades();
            Assert.IsNotNull(stades);
            StadeWS kamino = client.getStades().Find(x => x.Planete.Equals("Kamino"));
            kamino.NbPlaces = 10;
            client.updateStade(kamino);
            Assert.AreEqual(10, ((StadeWS)client.getStades().Find(x => x.Planete.Equals("Kamino"))).NbPlaces);
            kamino.NbPlaces = 100000;
            client.updateStade(kamino);
            Assert.AreEqual(100000, ((StadeWS)client.getStades().Find(x => x.Planete.Equals("Kamino"))).NbPlaces);

            client.Close();
        }

        [TestMethod]
        public void TestServiceARStades()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<StadeWS> stades = client.getStades();
            int size = stades.Count;
            Assert.IsNotNull(stades);
            /* AJOUT */
            StadeWS zone = new StadeWS(0, "Zone TEST", 11, new List<CaracteristiqueWS>());
            client.addStade(zone);
            Assert.AreEqual(size + 1, client.getStades().Count);
            /* SUPPRESSION */
            zone = client.getStades().Find(x => x.Planete.Equals("Zone TEST"));
            client.removeStade(zone);
            Assert.AreEqual(size, client.getStades().Count);

            client.Close();
        }

        [TestMethod]
        public void TestServiceGetMatches()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<MatchWS> matches = client.getMatches();
            Assert.IsNotNull(matches);
            MatchWS combat = matches.Find(x => x.Jedi1.Nom.Equals("Obi Wan Kenobi") && x.Jedi2.Nom.Equals("Yoda"));
            Assert.IsNotNull(combat);
            client.Close();
        }

        [TestMethod]
        public void TestServiceUpdateMatches()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<MatchWS> matches = client.getMatches();
            Assert.IsNotNull(matches);
            MatchWS kamino = client.getMatches().Find(x => x.Id.Equals(11));
            kamino.Phase = (EPhaseTournoiWS)5;
            client.updateMatch(kamino);
            Assert.AreEqual((EPhaseTournoiWS)5, ((MatchWS)client.getMatches().Find(x => x.Id.Equals(11))).Phase);
            kamino.Phase = (EPhaseTournoiWS)4;
            client.updateMatch(kamino);
            Assert.AreEqual((EPhaseTournoiWS)4, ((MatchWS)client.getMatches().Find(x => x.Id.Equals(11))).Phase);

            client.Close();
        }

        [TestMethod]
        public void TestServiceARMatches()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<MatchWS> matches = client.getMatches();
            List<JediWS> jedis = client.getJedis();
            List<StadeWS> stades = client.getStades();
            int size = matches.Count;
            Assert.IsNotNull(matches);
            /* AJOUT */
            MatchWS zone = new MatchWS(0, jedis.ElementAt(0), jedis.ElementAt(3), null, stades.ElementAt(0), EntitiesLayer.EPhaseTournoi.HuitiemeFinale1);
            client.addMatch(zone);
            Assert.AreEqual(size + 1, client.getMatches().Count);
            /* SUPPRESSION */
            zone = client.getMatches().Find(x => x.Jedi1 != null && x.Jedi2 != null && x.Jedi1.Id.Equals(jedis.ElementAt(0).Id) && x.Jedi2.Id.Equals(jedis.ElementAt(3).Id) && x.Stade.Id.Equals(stades.ElementAt(0).Id));
            client.removeMatch(zone);
            Assert.AreEqual(size, client.getMatches().Count);

            client.Close();
        }

        [TestMethod]
        public void TestServiceGetTournois()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            List<TournoiWS> tournois = client.getTournois();
            Assert.IsNotNull(tournois);
            TournoiWS combat = tournois.Find(x => x.Nom == "NewTournamentDeOufMalade");
            Assert.IsNotNull(combat);
            client.Close();
        }

        [TestMethod]
        public void TestServiceARTournois()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();

            List<TournoiWS> tournois = client.getTournois();
            int size = tournois.Count;
            Assert.IsNotNull(tournois);
            /* AJOUT */
            TournoiWS bob = new TournoiWS(0, "Test", new List<MatchWS>());
            client.addTournoi(bob);
            Assert.AreEqual(size + 1, client.getTournois().Count);
            /* SUPPRESSION */
            bob = client.getTournois().Find(x => x.Nom.Equals("Test"));
            client.removeTournoi(bob);
            Assert.AreEqual(size, client.getTournois().Count);

            client.Close();
        }
    }
}
