using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using DataAccessLayer;
using System.Security.Cryptography;
using System.Xml;

namespace BusinessLayer
{
    public class JediTournamentManager
    {
        private static DataAccessLayer.DalManager bdd = DataAccessLayer.DalManager.Instance;

        #region Stades management
        public List<Stade> getStades()
        {
            return bdd.getStades();
        }
        public int updateStades(List<Stade> l)
        {
            return bdd.updateStades(l);
        }

        public List<Stade> getStadesByNbPlace(int nbPlace)
        {
            return (from x in bdd.getStades() where x.NbPlaces >= nbPlace select x).ToList();
        }

        public List<Stade> getStadeByCarac(Caracteristique carac)
        {
            return (from x in bdd.getStades() where x.Caracteristiques.Contains(carac) select x).ToList();
        }

        public List<Stade> getStadeByPlanet(String planet)
        {
            return (from x in bdd.getStades() where x.Planete == planet select x).ToList();
        }
        #endregion

        #region Jedis management
        public List<Jedi> getJedis()
        {
            return bdd.getJedis();
        }
        public int updateJedis(List<Jedi> l)
        {
            return bdd.updateJedis(l);
        }

        public List<Jedi> getWhiteSideJedis()
        {
            return (from x in bdd.getJedis() where x.IsSith == false select x).ToList();
        }

        public List<Jedi> getDarkSideJedis()
        {
            return (from x in bdd.getJedis() where x.IsSith == true select x).ToList();
        }

        public List<Jedi> getJedisByName(string name)
        {
            return (from x in bdd.getJedis() where x.Nom == name select x).ToList();
        }

        public IEnumerable<String> getDarkSideJedisNames()
        {
            IEnumerable<String> coteObscur = from x in bdd.getJedis() where x.IsSith == true select x.Nom;

            return coteObscur;
        }
        #endregion

        #region Caracteristiques
        public List<Caracteristique> getCaracteristiques()
        {
            return bdd.getCaracteristiques();
        }
        public int updateCaracteristiques(List<Caracteristique> l)
        {
            return bdd.updateCaracteristiques(l);
        }
        #endregion

        #region Match management
        public List<Match> getMatches()
        {
            return bdd.getMatches();
        }
        public int updateMatches(List<Match> l)
        {
            return bdd.updateMatches(l);
        }

        public List<Match> getMatchsByJedisName(string j1, string j2)
        {
            return (from x in bdd.getMatches()
                    where (x.Jedi1.Nom == j1 && x.Jedi2.Nom == j2) ||
                          (x.Jedi1.Nom == j2 && x.Jedi2.Nom == j1)
                    select x).ToList();
        }

        public List<Match> getMatchsByWinner(string winner)
        {
            return (from x in bdd.getMatches() 
                    where (from y in bdd.getJedis() 
                           where y.Nom == winner 
                           select y.Id).Any()
                    select x).ToList();
        }

        public List<Match> getMatchsEmpty()
        {

            return (from x in bdd.getMatches()
                    where x.Jedi1 == null && x.Jedi2 == null
                    select x).ToList();
        }

        public List<Match> getMatchsNonEmpty()
        {

            return (from x in bdd.getMatches()
                    where x.Jedi1 != null && x.Jedi2 != null
                    select x).ToList();
        }

        public IEnumerable<Match> getMatches200Sith()
        {
            IEnumerable<Match> matches = from x in bdd.getMatches()
                                         where x.Stade.NbPlaces >= 200 && x.Jedi2 != null && x.Jedi1.IsSith == true
                                               && x.Jedi2 != null && x.Jedi2.IsSith == true
                                         select x;
            return matches;
        }
        #endregion

        #region Tournoi management
        public List<Tournoi> getTournois()
        {
            return bdd.getTournois();
        }
        public int updateTournois(List<Tournoi> l)
        {
            return bdd.updateTournois(l);
        }
        #endregion

        #region User management
        public static bool CheckConnexionUser(string login, string mdp)
        {
            bool isOk = false;
            string password = HashSHA1(mdp + login);
            Utilisateur user = bdd.GetUtilisateurByLogin(login);

            if (user != null)
            {
                if (user.Password.Equals(password))
                {
                    isOk = true;
                }
            }

            return isOk;
        }
        public bool AddUser(string login, string mdp, string nom, string prenom)
        {
            bool isOk = false;
            string password = HashSHA1(mdp + login);
            Utilisateur user = new Utilisateur(0, login, password, nom, prenom, 1000);

            isOk = bdd.addUser(user);

            return isOk;
        }

        public List<Utilisateur> getUsers()
        {
            return bdd.getUsers();
        }
        public int updateUser(Utilisateur u)
        {
            return bdd.updateUser(u);
        }

        public static string HashSHA1(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            for(int i = 0 ; i < hashData.Length ; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            return returnValue.ToString();
        }
        #endregion


        

        #region Game Simulation       

        public int playRound(EShifumi choiceA, EShifumi choiceB)
        {
            return (choiceA == choiceB ? 0 :            // si egalite ZERO
                    (choiceA == choiceB+1 ? -1 :        // si A gagne -1
                    (choiceA == EShifumi.Pierre && choiceB == EShifumi.Ciseau ? -1 : 1)));   // si B gagne 1
        }

        public Jedi simulateMatch(Match m)
        {
            Jedi winner = m.Jedi1;
            Random rd = new Random();
            double balance = .5;
            
            double gain1, gain2, gain3, gain4;
            double [] stade = new double[3];
            stade[0] = 0;
            stade[1] = 0;
            stade[2] = 0;
            gain1 = rd.NextDouble();
            gain1 = gain1 <= m.Jedi1.getPerception() / 100 ? 1 + gain1 : 1;
            gain2 = rd.NextDouble();
            gain2 = gain2 <= m.Jedi2.getPerception() / 100 ? 1 + gain2 : 1;
            gain3 = rd.NextDouble();
            gain3 = gain3 <= m.Jedi1.getPerception() / 100 ? 1 + gain3 : 1;
            gain4 = rd.NextDouble();
            gain4 = gain4 <= m.Jedi2.getPerception() / 100 ? 1 + gain4 : 1;

            foreach(Caracteristique c in m.Stade.Caracteristiques)
            {
                stade[(int)c.Definition] += c.Valeur;
            }

            if(m.Jedi1.IsSith != m.Jedi2.IsSith)
            {
                balance += ((stade[(int)EDefCaracteristique.Strength] + stade[(int)EDefCaracteristique.Dexterity]) / 100);
                balance = balance * (1 + stade[(int)EDefCaracteristique.Perception] / 100);
            }

            balance += ((m.Jedi1.getStrength()/100*gain1-m.Jedi2.getDexterity()/100*gain2) + (m.Jedi2.getStrength() / 100 * gain4 - m.Jedi1.getDexterity() / 100 * gain3));
            
            if (rd.NextDouble() > balance)
                winner = m.Jedi2;

            m.JediVainqueur = winner;

            return winner;
        }

        public Jedi playTournament(Tournoi t)
        {
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale1).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale1));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale1).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale2));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale2).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale3));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale2).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale4));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale3).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale5));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale3).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale6));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale4).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale7));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale4).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.HuitiemeFinale8));

            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale1).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale1));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale1).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale2));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale2).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale3));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale2).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.QuartFinale4));

            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.Finale).Jedi1 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale1));
            t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.Finale).Jedi2 = simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.DemiFinale2));

            return simulateMatch(t.Matchs.Find(x => x.PhaseTournoi == EPhaseTournoi.Finale));
        }

        public class MatchOrderComparer : IComparer<Match>
        {
            public int Compare(Match x, Match y)
            {
                if(x.PhaseTournoi <= y.PhaseTournoi)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void simulateTournament(Tournoi championship)
        {
            Queue<Jedi> winners = new Queue<Jedi>();
            Dictionary<int, List<Match>> championshipScheme = new Dictionary<int, List<Match>>();
            MatchOrderComparer comp = new MatchOrderComparer();
            List<Match> tournoi = championship.Matchs;
            tournoi.Sort(comp);
            List<Match> tmp = new List<Match>();
            tmp.AddRange(tournoi.Where(x => x.PhaseTournoi < EPhaseTournoi.QuartFinale1));
            championshipScheme.Add(0, tmp);
            tmp = new List<Match>();
            tmp.AddRange(tournoi.Where(x => x.PhaseTournoi < EPhaseTournoi.DemiFinale1));
            championshipScheme.Add(1, tmp);
            tmp = new List<Match>();
            tmp.AddRange(tournoi.Where(x => x.PhaseTournoi < EPhaseTournoi.Finale));
            championshipScheme.Add(2, tmp);
            tmp = new List<Match>();
            tmp.AddRange(tournoi.Where(x => x.PhaseTournoi == EPhaseTournoi.Finale));
            championshipScheme.Add(3, tmp);

            foreach (KeyValuePair<int, List<Match>> phase in championshipScheme)
            {
                if(winners.Count != 0)
                {
                    foreach (Match m in phase.Value)
                    {
                        m.Jedi1 = winners.Dequeue();
                        m.Jedi2 = winners.Dequeue();
                    }
                }
                Console.Out.WriteLine(phase.Key);
                foreach (Match m in phase.Value)
                {
                    winners.Enqueue(simulateMatch(m));
                }
            }
        }
        #endregion

        #region XML
        public void exportJedis(String filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(filename, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Jedis");

            foreach (Jedi jedi in bdd.getJedis())
            {
                writer.WriteStartElement("Jedi");

                writer.WriteElementString("ID", jedi.Id.ToString());
                writer.WriteElementString("Nom", jedi.Nom.ToString());
                writer.WriteElementString("IsSith", jedi.IsSith.ToString());
                writer.WriteStartElement("Caracteristiques");
                foreach (Caracteristique carac in jedi.Caracteristiques)
                {
                    writer.WriteStartElement("Caracteristique");
                    writer.WriteElementString("ID", carac.Id.ToString());
                    writer.WriteElementString("Nom", carac.Nom.ToString());
                    writer.WriteElementString("Type", carac.Type.ToString());
                    writer.WriteElementString("Valeur", carac.Valeur.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        #endregion
    }
}
