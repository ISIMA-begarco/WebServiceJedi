using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.IO;

namespace DataAccessLayer
{
    public class DalManager
    {
        private static DalManager _instance;
        private static readonly object padlock = new object();
        IBridge bdd;

        public static DalManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DalManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private DalManager()
        {
            //==> Sara  : à mettre  connectionString 
            //string root ="(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\Sara\\Documents\\Dot_NET\\Projet\\Database\\JediTournament.mdf;Integrated Security=True;Connect Timeout=30";
            //string root = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Kami\\Source\\Repos\\webService\\Database\\JediTournament.mdf;Integrated Security=True;Connect Timeout=30";
            //string root = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Kami\\Source\\Repos\\webService\\Database\\JediTournament.mdf; Integrated Security = True; Connect Timeout = 30";
            string root = Environment.CurrentDirectory.Split(new string[] { "JediTournamentConsole", "ApplicationWPF", "DataAccessLayerTest", "BusinessLayerTest" }, StringSplitOptions.None)[0];
            string url = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + root + "Database\\JediTournament.mdf;Integrated Security=True;Connect Timeout=30";
            /// AVANT VISUAL STUDIO 15     string root = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=" + Environment.CurrentDirectory.Split(new string[] { "JediTournamentConsole" }, StringSplitOptions.None)[0] + "Database\\JediTournament.mdf;Integrated Security=True;Connect Timeout=30";//"Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\alnoel4\\Source\\Repos\\webService\\Database\\JediTournament.mdf;Integrated Security=True;Connect Timeout=30";

            bdd = new MSSQLSFile(url);
            //bdd = new StubDatabase();
        }

        public List<Jedi> getJedis()
        {
            return bdd.getJedis();
        }
        public List<Stade> getStades()
        {
            return bdd.getStades();
        }
        public List<Match> getMatches()
        {
            return bdd.getMatches();
        }
        public List<Tournoi> getTournois()
        {
            return bdd.getTournois();
        }
        public List<Caracteristique> getCaracteristiques()
        {
            return bdd.getCaracteristiques();
        }
        public int updateJedis(List<Jedi> l)
        {
            return bdd.updateJedis(l);
        }
        public int updateStades(List<Stade> l)
        {
            return bdd.updateStades(l);
        }
        public int updateMatches(List<Match> l)
        {
            return bdd.updateMatches(l);
        }
        public int updateTournois(List<Tournoi> l)
        {
            return bdd.updateTournois(l);
        }
        public int updateCaracteristiques(List<Caracteristique> l)
        {
            return bdd.updateCaracteristiques(l);
        }
        public Utilisateur GetUtilisateurByLogin(string login)
        {
            return bdd.getUtilisateurByLogin(login);
        }
        public List<Utilisateur> getUsers()
        {
            return bdd.getUsers();
        }
        public bool addUser(Utilisateur u)
        {
            return bdd.addUser(u);
        }
        public bool deleteUserByLogin(string login)
        {
            return bdd.deleteUserByLogin(login);
        }
    }
}
