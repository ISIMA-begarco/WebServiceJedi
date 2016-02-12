using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;

namespace DataAccessLayer
{
    class StubDatabase : IBridge
    {
        private List<Utilisateur> users;
        private List<Caracteristique> caracs;
        private List<Stade> stades;
        private List<Jedi> jedis;
        private List<Match> matches;
        private List<Tournoi> tournois;

        public StubDatabase()
        {
            users = new List<Utilisateur>();
            caracs = new List<Caracteristique>();
            stades = new List<Stade>();
            jedis = new List<Jedi>();
            matches = new List<Match>();
            tournois = new List<Tournoi>();

            #region ajout utilisateurs
            users.Add(new Utilisateur(1, "begarco", "15851918021222115514974461602051215898129243254222", "Garçon", "Benoît"));
            #endregion

            #region ajout des caracteristiques
            caracs.Add(new Caracteristique(1, EDefCaracteristique.Perception, "Le réveil de la force", ETypeCaracteristique.Jedi, 30));
            caracs.Add(new Caracteristique(2, EDefCaracteristique.Perception, "Esprit obscurci", ETypeCaracteristique.Jedi, 40));
            caracs.Add(new Caracteristique(3, EDefCaracteristique.Perception, "The chosen one", ETypeCaracteristique.Jedi, 70));
            caracs.Add(new Caracteristique(4, EDefCaracteristique.Perception, "Je suis un Skywalker", ETypeCaracteristique.Jedi, 30));
            caracs.Add(new Caracteristique(5, EDefCaracteristique.Perception, "Temple Jedi", ETypeCaracteristique.Stade, 20));
            caracs.Add(new Caracteristique(6, EDefCaracteristique.Perception, "Sanctuaire Sith", ETypeCaracteristique.Stade, -20));
            caracs.Add(new Caracteristique(7, EDefCaracteristique.Perception, "Padawan", ETypeCaracteristique.Jedi, 10));
            caracs.Add(new Caracteristique(8, EDefCaracteristique.Perception, "Apprenti Sith", ETypeCaracteristique.Jedi, 15));
            caracs.Add(new Caracteristique(9, EDefCaracteristique.Perception, "Guidé par un esprit", ETypeCaracteristique.Jedi, 20));
            caracs.Add(new Caracteristique(10, EDefCaracteristique.Perception, "Grand Maître Jedi", ETypeCaracteristique.Jedi, 50));
            caracs.Add(new Caracteristique(11, EDefCaracteristique.Perception, "Seigneur Sith", ETypeCaracteristique.Jedi, 50));
            caracs.Add(new Caracteristique(12, EDefCaracteristique.Strength, "Combat au sabre laser", ETypeCaracteristique.Jedi, 30));
            caracs.Add(new Caracteristique(13, EDefCaracteristique.Strength, "Double sabre", ETypeCaracteristique.Jedi, 50));
            caracs.Add(new Caracteristique(14, EDefCaracteristique.Strength, "Sabre double lame", ETypeCaracteristique.Jedi, 40));
            caracs.Add(new Caracteristique(15, EDefCaracteristique.Strength, "Eclairs", ETypeCaracteristique.Jedi, 50));
            caracs.Add(new Caracteristique(16, EDefCaracteristique.Strength, "Etranglement", ETypeCaracteristique.Jedi, 50));
            caracs.Add(new Caracteristique(17, EDefCaracteristique.Strength, "Projection", ETypeCaracteristique.Jedi, 30));
            caracs.Add(new Caracteristique(18, EDefCaracteristique.Strength, "Arène de gladiateur", ETypeCaracteristique.Stade, 20));
            caracs.Add(new Caracteristique(19, EDefCaracteristique.Strength, "Combat dans la nature", ETypeCaracteristique.Stade, 40));
            caracs.Add(new Caracteristique(20, EDefCaracteristique.Strength, "Utilisation du décor", ETypeCaracteristique.Stade, -40));
            caracs.Add(new Caracteristique(21, EDefCaracteristique.Strength, "Public à utiliser", ETypeCaracteristique.Stade, -20));
            caracs.Add(new Caracteristique(22, EDefCaracteristique.Strength, "Soutien Stormtroopers", ETypeCaracteristique.Stade, -30));
            caracs.Add(new Caracteristique(23, EDefCaracteristique.Dexterity, "Parade au sabre laser", ETypeCaracteristique.Jedi, 40));
            caracs.Add(new Caracteristique(24, EDefCaracteristique.Dexterity, "Armure légère", ETypeCaracteristique.Jedi, 20));
            caracs.Add(new Caracteristique(25, EDefCaracteristique.Dexterity, "Garde", ETypeCaracteristique.Jedi, 10));
            caracs.Add(new Caracteristique(26, EDefCaracteristique.Dexterity, "Unité R2", ETypeCaracteristique.Jedi, 5));
            caracs.Add(new Caracteristique(27, EDefCaracteristique.Dexterity, "Je suis un wookie", ETypeCaracteristique.Jedi, 60));
            caracs.Add(new Caracteristique(28, EDefCaracteristique.Dexterity, "Je suis un androïd", ETypeCaracteristique.Jedi, 70));
            caracs.Add(new Caracteristique(29, EDefCaracteristique.Dexterity, "Le dessus sur la lave", ETypeCaracteristique.Stade, 40));
            caracs.Add(new Caracteristique(30, EDefCaracteristique.Dexterity, "Complexe de destruction massif", ETypeCaracteristique.Stade, -40));
            #endregion

            #region ajout des stades
            List<Caracteristique> carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[4]); carAdd.Add(caracs[19]); carAdd.Add(caracs[21]);
            stades.Add(new Stade(1, 1000000, "Coruscant", carAdd, "planet.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[21]); carAdd.Add(caracs[6]);
            stades.Add(new Stade(2, 100000, "Kamino", carAdd, "planet.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[18]); carAdd.Add(caracs[20]);
            stades.Add(new Stade(3, 10000, "Naboo", carAdd, "planet.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[18]); carAdd.Add(caracs[17]);
            stades.Add(new Stade(4, 666, "Tatooine", carAdd, "planet.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[18]); carAdd.Add(caracs[19]);
            stades.Add(new Stade(5, 4, "Dagobah", carAdd, "planet.png"));
            #endregion

            #region ajout des jedis
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[8]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(1, carAdd, false, "Obi Wan Kenobi", "obiwan.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[8]);
            jedis.Add(new Jedi(2, carAdd, false, "Yoda", "yoda.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]);carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(3, carAdd, false, "Mace Windu", "windu.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(4, carAdd, false, "Aayla Secura", "secura.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(5, carAdd, false, "Shaak Ti", "shaakti.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(6, carAdd, false, "Plo Koon", "plokoon.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(7, carAdd, false, "Kit Fist", "kitfist.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]); carAdd.Add(caracs[23]);
            jedis.Add(new Jedi(8, carAdd, false, "Qui Gon Jinn", "quigon.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[9]); carAdd.Add(caracs[3]); carAdd.Add(caracs[22]); carAdd.Add(caracs[8]);
            jedis.Add(new Jedi(9, carAdd, false, "Luke Skywalker", "luke.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[1]); carAdd.Add(caracs[2]); carAdd.Add(caracs[12]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(10, carAdd, true, "Anakin Skywalker", "anakin.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[0]); carAdd.Add(caracs[3]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(11, carAdd, false, "Rey Skywalker", "rey.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[10]); carAdd.Add(caracs[14]); carAdd.Add(caracs[24]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(12, carAdd, true, "Dark Sidious", "sidious.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[10]); carAdd.Add(caracs[2]); carAdd.Add(caracs[15]); carAdd.Add(caracs[27]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(13, carAdd, true, "Dark Vador", "vador.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[10]); carAdd.Add(caracs[11]); carAdd.Add(caracs[27]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(14, carAdd, true, "Grivious", "grivious.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[7]); carAdd.Add(caracs[13]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(15, carAdd, true, "Dark Maul", "darkmaul.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[10]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(16, carAdd, true, "Dooku", "dooku.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[1]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(17, carAdd, true, "Kylo Ren", "kylo.png"));
            carAdd = new List<Caracteristique>();
            carAdd.Add(caracs[7]); carAdd.Add(caracs[11]); carAdd.Add(caracs[22]);
            jedis.Add(new Jedi(18, carAdd, true, "Dark Revan", "revan.png"));
            #endregion

            #region ajout des matches
            matches.Add(new Match(1, jedis[0], jedis[1], EPhaseTournoi.HuitiemeFinale1, stades[0]));
            matches.Add(new Match(2, jedis[2], jedis[3], EPhaseTournoi.HuitiemeFinale2, stades[1]));
            matches.Add(new Match(3, jedis[4], jedis[5], EPhaseTournoi.HuitiemeFinale3, stades[2]));
            matches.Add(new Match(4, jedis[6], jedis[7], EPhaseTournoi.HuitiemeFinale4, stades[3]));
            matches.Add(new Match(5, jedis[8], jedis[9], EPhaseTournoi.HuitiemeFinale5, stades[0]));
            matches.Add(new Match(6, jedis[10], jedis[11], EPhaseTournoi.HuitiemeFinale6, stades[1]));
            matches.Add(new Match(7, jedis[12], jedis[13], EPhaseTournoi.HuitiemeFinale7, stades[2]));
            matches.Add(new Match(8, jedis[14], jedis[15], EPhaseTournoi.HuitiemeFinale8, stades[3]));
            matches.Add(new Match(9, null, null, EPhaseTournoi.QuartFinale1, stades[0]));
            matches.Add(new Match(10, null, null, EPhaseTournoi.QuartFinale2, stades[1]));
            matches.Add(new Match(11, null, null, EPhaseTournoi.QuartFinale3, stades[2]));
            matches.Add(new Match(12, null, null, EPhaseTournoi.QuartFinale4, stades[3]));
            matches.Add(new Match(13, null, null, EPhaseTournoi.DemiFinale1, stades[0]));
            matches.Add(new Match(14, null, null, EPhaseTournoi.DemiFinale2, stades[2]));
            matches.Add(new Match(15, null, null, EPhaseTournoi.Finale, stades[1]));
            #endregion

            #region ajout des tournois
            tournois.Add(new Tournoi(1, "Mos Eisley Tournament", matches));
            #endregion
        }

        public bool addUser(Utilisateur u)
        {
            users.Add(u);
            return true;
        }

        public List<Caracteristique> getCaracteristiques()
        {
            return caracs;
        }

        public List<Jedi> getJedis()
        {
            return jedis;
        }

        public List<Match> getMatches()
        {
            return matches;
        }

        public List<Stade> getStades()
        {
            return stades;
        }

        public List<Tournoi> getTournois()
        {
            return tournois;
        }

        public List<Utilisateur> getUsers()
        {
            return users;
        }

        public Utilisateur getUtilisateurByLogin(string login)
        {
            List<Utilisateur> u = users.Where(x => x.Login == login).ToList();
            Utilisateur retour = null;
            if (u.Count > 0)
                retour = u.First();

            return retour;
        }

        public int updateCaracteristiques(List<Caracteristique> l)
        {
            caracs = l;
            return 0;
        }

        public int updateJedis(List<Jedi> l)
        {
            jedis = l;
            return 0;
        }

        public int updateMatches(List<Match> l)
        {
            matches = l;
            return 0;
        }

        public int updateStades(List<Stade> l)
        {
            stades = l;
            return 0;
        }

        public int updateTournois(List<Tournoi> l)
        {
            tournois = l;
            return 0;
        }

        public bool deleteUserByLogin(string login)
        {
            bool retour = false;

            List<Utilisateur> liste = getUsers().Where(x => x.Login == login).ToList();
            if(liste.Count != 0)
            {
                liste.Remove(liste.First());
                retour = true;
            }

            return retour;
        }
    }
}
