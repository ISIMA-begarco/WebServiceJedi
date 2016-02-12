using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EntitiesLayer;
using BusinessLayer;

namespace BusinessLayerTest
{
    [TestClass]
    public class JediTournamentManagerTest
    {
        [TestMethod]
        public void generalGettersTest()
        {
            JediTournamentManager jtm = new JediTournamentManager();

            // Verifie que l'on ne retourne jamais null
            List<Stade> stades = jtm.getStades();
            List<Jedi> jedis = jtm.getJedis();
            List<Match> matchs = jtm.getMatches();

            Assert.IsNotNull(stades);
            Assert.IsNotNull(jedis);
            Assert.IsNotNull(matchs);

            // Verifie que l'on ne peut pas ajouter dans les listes de
            // manière non controlée
            int expectedSize = stades.Count;
            stades.Add(new Stade(0, 125, "Tatouine", null));
            List<Stade> stades2 = jtm.getStades();
            Assert.AreEqual(expectedSize, stades2.Count);

            expectedSize = jedis.Count;
            jedis.Add(new Jedi(0, null, false, "Jar Jar"));
            List<Jedi> jedis2 = jtm.getJedis();
            Assert.AreEqual(expectedSize, jedis2.Count);

            expectedSize = matchs.Count;
            matchs.Add(new Match(0, null, null, EPhaseTournoi.Finale, null));
            List<Match> matchs2 = jtm.getMatches();
            Assert.AreEqual(expectedSize, matchs2.Count);
        }

        [TestMethod]
        public void JedisGettersTest()
        {
            JediTournamentManager jtm = new JediTournamentManager();

            // Verifie que l'on ne retourne bien que des siths
            List<Jedi> siths = jtm.getDarkSideJedis();
            foreach (Jedi j in siths)
                Assert.IsTrue(j.IsSith);

            // Verifie que l'on ne retourne bien que des Jedis
            List<Jedi> jedis = jtm.getWhiteSideJedis();
            foreach (Jedi j in jedis)
                Assert.IsFalse(j.IsSith);

            // Verifie que l'on a bien tout les jedis
            int countExpected = jtm.getJedis().Count;
            Assert.AreEqual(countExpected, siths.Count + jedis.Count);
        }

        [TestMethod]
        public void MatchsGettersTest()
        {
            JediTournamentManager jtm = new JediTournamentManager();


        }

        [TestMethod]
        public void StadeGettersTest()
        {
            JediTournamentManager jtm = new JediTournamentManager();


        }

        [TestMethod]
        public void ShifumiTest()
        {
            JediTournamentManager jtm = new JediTournamentManager();

            // Les trois cas où il y a egalite
            int expectedValue = 0; 
            int computedValue = jtm.playRound(EShifumi.Papier, 
                                              EShifumi.Papier);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Pierre,
                                          EShifumi.Pierre);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Ciseau,
                                          EShifumi.Ciseau);
            Assert.AreEqual(expectedValue, computedValue);

            // Les trois cas où le premier gagne
            expectedValue = -1;
            computedValue = jtm.playRound(EShifumi.Papier,
                                          EShifumi.Pierre);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Pierre,
                                          EShifumi.Ciseau);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Ciseau,
                                          EShifumi.Papier);
            Assert.AreEqual(expectedValue, computedValue);

            // Les trois cas où le deuxième gagne
            expectedValue = 1;
            computedValue = jtm.playRound(EShifumi.Papier,
                                          EShifumi.Ciseau);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Pierre,
                                          EShifumi.Papier);
            Assert.AreEqual(expectedValue, computedValue);

            computedValue = jtm.playRound(EShifumi.Ciseau,
                                          EShifumi.Pierre);
            Assert.AreEqual(expectedValue, computedValue);
        }
    }
}
