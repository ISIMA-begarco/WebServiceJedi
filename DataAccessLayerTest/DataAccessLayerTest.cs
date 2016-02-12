using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayerTest
{
    [TestClass]
    public class DataAccessLayerTest
    {
        private static DataAccessLayer.DalManager bdd = DataAccessLayer.DalManager.Instance;

        [TestMethod]
        public void getUpdateJedisTest()
        {
            int taille = 0;
            int debTaille = 0;
            List<Jedi> liste = bdd.getJedis();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if(liste.Count > 0)
            {
                Jedi j = new Jedi(0, null, true, "Testman", "John.png");
                liste.Add(j);
                bdd.updateJedis(liste);
                taille = liste.Count;
                liste = bdd.getJedis();
                Assert.AreEqual<int>(taille, liste.Count);
                Assert.AreNotEqual<int>(0, (liste.Where(x => x.Nom == j.Nom)).ToList().Count);
                liste.Remove(liste.Last());
                bdd.updateJedis(liste);
                Assert.AreEqual(debTaille, bdd.getJedis().Count);
            }
        }

        [TestMethod]
        public void getUpdateStadesTest()
        {
            int taille = 0;
            int debTaille = 0;
            List<Stade> liste = bdd.getStades();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if (liste.Count > 0)
            {
                Stade j = new Stade(0, 999, "Planete Test", new List<Caracteristique>() { bdd.getCaracteristiques().First() });
                liste.Add(j);
                bdd.updateStades(liste);
                taille = liste.Count;
                liste = bdd.getStades();
                Assert.AreEqual<int>(taille, liste.Count);
                Assert.AreNotEqual<int>(0, (liste.Where(x => x.Planete == j.Planete)).ToList().Count);
                liste.Remove(liste.Last());
                bdd.updateStades(liste);
                Assert.AreEqual(debTaille, bdd.getStades().Count);
            }
        }

        [TestMethod]
        public void getUpdateMatchesTest()
        {
            int taille = 0;
            int debTaille = 0;
            List<Match> liste = bdd.getMatches();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if (liste.Count > 0)
            {
                Match j = liste[0];
                Assert.AreEqual<int>(liste.Count, bdd.getMatches().Count);
                /*liste.Remove(liste.First());
                bdd.updateMatches(liste);
                taille = liste.Count;
                liste = bdd.getMatches();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Add(j);
                bdd.updateMatches(liste);
                taille = liste.Count;
                liste = bdd.getMatches();
                Assert.AreEqual<int>(taille, liste.Count);*/
                j = new Match(0, bdd.getJedis().First(), bdd.getJedis().Last(), EPhaseTournoi.Finale, bdd.getStades().First());
                liste.Add(j);
                bdd.updateMatches(liste);
                taille = liste.Count;
                liste = bdd.getMatches();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Remove(liste.Last());
                bdd.updateMatches(liste);
                Assert.AreEqual(debTaille, bdd.getMatches().Count);
            }
        }

        [TestMethod]
        public void getUpdateCaracteristiquesTest()
        {
            int taille = 0;
            int debTaille = 0;
            List<Caracteristique> liste = bdd.getCaracteristiques();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if (liste.Count > 0)
            {
                Caracteristique j = liste[0];
                Assert.AreEqual<int>(liste.Count, bdd.getCaracteristiques().Count);
                liste.Remove(liste.First());
                bdd.updateCaracteristiques(liste);
                taille = liste.Count;
                liste = bdd.getCaracteristiques();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Add(j);
                bdd.updateCaracteristiques(liste);
                taille = liste.Count;
                liste = bdd.getCaracteristiques();
                Assert.AreEqual<int>(taille, liste.Count);
                j = new Caracteristique(0, EDefCaracteristique.Strength, "Carac test", ETypeCaracteristique.Jedi, 999);
                liste.Add(j);
                bdd.updateCaracteristiques(liste);
                taille = liste.Count;
                liste = bdd.getCaracteristiques();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Remove(liste.Last());
                bdd.updateCaracteristiques(liste);
                Assert.AreEqual(debTaille, bdd.getCaracteristiques().Count);
            }
        }

        [TestMethod]
        public void getUpdateUsersTest()
        {
            int debTaille = 0;
            List<Utilisateur> liste = bdd.getUsers();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if (liste.Count > 0)
            {
                Utilisateur j = new Utilisateur(0, "test", "test", "Jean", "Bernard");
                bdd.addUser(j);
                Assert.AreEqual<int>(debTaille+1, bdd.getUsers().Count);
                bdd.deleteUserByLogin("test");
                Assert.AreEqual<int>(debTaille, bdd.getUsers().Count);
            }
        }

        [TestMethod]
        public void getUpdateTournoisTest()
        {
            int taille = 0;
            int debTaille = 0;
            List<Tournoi> liste = bdd.getTournois();
            debTaille = liste.Count;
            Assert.AreNotEqual<int>(liste.Count, 0);
            if (liste.Count > 0)
            {
                Tournoi j = liste[0];
                Assert.AreEqual<int>(liste.Count, bdd.getTournois().Count);
                liste.Remove(liste.First());
                bdd.updateTournois(liste);
                taille = liste.Count;
                liste = bdd.getTournois();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Add(j);
                Assert.AreEqual<int>(15, liste.First().Matchs.Count);
                bdd.updateTournois(liste);
                taille = liste.Count;
                liste = bdd.getTournois();
                Assert.AreEqual<int>(taille, liste.Count);
                j = new Tournoi(0, "Test", bdd.getMatches());
                liste.Add(j);
                bdd.updateTournois(liste);
                taille = liste.Count;
                liste = bdd.getTournois();
                Assert.AreEqual<int>(taille, liste.Count);
                liste.Remove(liste.Last());
                bdd.updateTournois(liste);
                Assert.AreEqual(debTaille, bdd.getTournois().Count);
            }
        }
    }
}
