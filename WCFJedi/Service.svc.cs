using BusinessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFJedi
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service.svc ou Service.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        JediTournamentManager businessManager;

        public Service()
        {
            businessManager = new JediTournamentManager();
        }

        public bool addJedi(JediWS jedi)
        {
            List<Jedi> listeJ = businessManager.getJedis();
            List<Caracteristique> listeC = businessManager.getCaracteristiques();
            List<Caracteristique> caracs = new List<Caracteristique>();
            jedi.Caracteristiques.ForEach(x => caracs.Add(listeC.Find(c => c.Nom.Equals(x.Nom))));
            Jedi newJedi = new Jedi(0, caracs, jedi.IsSith, jedi.Nom);
            listeJ.Add(newJedi);
            return 0==businessManager.updateJedis(listeJ);
        }

        public bool addMatch(MatchWS match)
        {
            List<Match> listeM = businessManager.getMatches();
            List<Jedi> listeJ = businessManager.getJedis();
            List<Stade> listeS = businessManager.getStades();
            Match newMatch = new Match(0,   (match.Jedi1 != null ? listeJ.Where(x => x.Nom == match.Jedi1.Nom).First() : null),
                                            (match.Jedi2 != null ? listeJ.Where(x => x.Nom == match.Jedi2.Nom).First() : null), 
                                            (EPhaseTournoi) match.Phase, 
                                            listeS.Where(x => x.Planete == match.Stade.Planete).First(),
                                            (match.JediVainqueur != null ? listeJ.Where(x => x.Nom == match.JediVainqueur.Nom).First() : null));
            listeM.Add(newMatch);
            return 0==businessManager.updateMatches(listeM);
        }

        public bool addStade(StadeWS stade)
        {
            List<Stade> listeS = businessManager.getStades();
            List<Caracteristique> listeC = businessManager.getCaracteristiques();
            List<Caracteristique> caracs = new List<Caracteristique>();
            stade.Caracteristiques.ForEach(x => caracs.Add(listeC.Find(c => c.Nom.Equals(x.Nom))));
            Stade newStade = new Stade(0, stade.NbPlaces, stade.Planete, caracs);
            listeS.Add(newStade);
            return 0 == businessManager.updateStades(listeS);
        }

        public bool addTournoi(TournoiWS tournoi)
        {
            List<Tournoi> listeT = businessManager.getTournois();
            List<Match> listeM = businessManager.getMatches();
            List<Match> matches = new List<Match>();
            tournoi.Matches.ForEach(x => matches.Add(listeM.Find(c => c.Id.Equals(x.Id))));
            Tournoi newTournoi = new Tournoi(0, tournoi.Nom, matches);
            listeT.Add(newTournoi);
            return 0 == businessManager.updateTournois(listeT);
        }

        public List<CaracteristiqueWS> getCaracteristiquesOf(string jediName)
        {
            List<Jedi> liste = businessManager.getJedisByName(jediName);
            List<CaracteristiqueWS> listeC = null;
            JediWS jedi = null;
            if (liste.Count()!=0)
            {
                jedi = new JediWS(liste.First());
                listeC = jedi.Caracteristiques;
            }
            return listeC;
        }

        public List<JediWS> getJedis()
        {
            List<JediWS> liste = new List<JediWS>();
            businessManager.getJedis().ForEach(x => liste.Add(new JediWS(x)));
            return liste;
        }

        public List<MatchWS> getMatches()
        {
            List<MatchWS> liste = new List<MatchWS>();
            businessManager.getMatches().ForEach(x => liste.Add(new MatchWS(x)));
            return liste;
        }

        public List<StadeWS> getStades()
        {
            List<StadeWS> liste = new List<StadeWS>();
            businessManager.getStades().ForEach(x => liste.Add(new StadeWS(x)));
            return liste;
        }

        public List<TournoiWS> getTournois()
        {
            List<TournoiWS> liste = new List<TournoiWS>();
            businessManager.getTournois().ForEach(x => liste.Add(new TournoiWS(x)));
            return liste;
        }

        public List<CaracteristiqueWS> getCaracteristiques()
        {
            List<CaracteristiqueWS> liste = new List<CaracteristiqueWS>();
            businessManager.getCaracteristiques().ForEach(x => liste.Add(new CaracteristiqueWS(x)));
            return liste;
        }

        public bool removeJedi(JediWS jedi)
        {
            List<Jedi> listeJ = businessManager.getJedis();
            Jedi paria = listeJ.First(x => x.Id == jedi.Id);
            listeJ.Remove(paria);
            return 0 == businessManager.updateJedis(listeJ);
        }

        public bool removeMatch(MatchWS match)
        {
            List<Match> liste = businessManager.getMatches();
            Match paria = liste.First(x => x.Id == match.Id);
            liste.Remove(paria);
            return 0 == businessManager.updateMatches(liste);
        }

        public bool removeStade(StadeWS stade)
        {
            List<Stade> liste = businessManager.getStades();
            Stade paria = liste.First(x => x.Id == stade.Id);
            liste.Remove(paria);
            return 0 == businessManager.updateStades(liste);
        }

        public bool removeTournoi(TournoiWS tournoi)
        {
            List<Tournoi> liste = businessManager.getTournois();
            Tournoi paria = liste.First(x => x.Id == tournoi.Id);
            liste.Remove(paria);
            return 0 == businessManager.updateTournois(liste);
        }

        public bool updateJedi(JediWS jedi)
        {
            List<Jedi> listeJ = businessManager.getJedis();
            List<Caracteristique> listeC = businessManager.getCaracteristiques();
            List<Caracteristique> caracs = new List<Caracteristique>();
            jedi.Caracteristiques.ForEach(x => caracs.Add(listeC.Find(c => c.Nom.Equals(x.Nom))));
            Jedi newJedi = listeJ.First(x => x.Id == jedi.Id);
            newJedi.Nom = jedi.Nom;
            newJedi.IsSith = jedi.IsSith;
            newJedi.Caracteristiques = caracs;
            return 0 == businessManager.updateJedis(listeJ);
        }

        private Match toMatch(MatchWS ws)
        {
            List<Stade> stades = businessManager.getStades();
            List<Jedi> jedis = businessManager.getJedis();
            Match m = new Match(ws.Id, 
                                (ws.Jedi1 != null ? jedis.First(x => x.Id == ws.Jedi1.Id) : null),
                                (ws.Jedi2 != null ? jedis.First(x => x.Id == ws.Jedi2.Id) : null),
                                (EPhaseTournoi)ws.Phase, 
                                stades.First(x => x.Id == ws.Stade.Id), 
                                (ws.JediVainqueur != null ? jedis.First(x => x.Id == ws.JediVainqueur.Id) : null));
            return m;
        }

        public bool updateMatch(MatchWS match)
        {
            List<Match> listeM = businessManager.getMatches();
            Match newMatch = listeM.First(x => x.Id == match.Id);
            Match newData = toMatch(match);
            newMatch.Jedi1 = newData.Jedi1;
            newMatch.Jedi2 = newData.Jedi2;
            newMatch.JediVainqueur = newData.JediVainqueur;
            newMatch.Stade = newData.Stade;
            newMatch.PhaseTournoi = newData.PhaseTournoi;

            return 0 == businessManager.updateMatches(listeM);
        }

        public bool updateStade(StadeWS stade)
        {
            List<Stade> listeS = businessManager.getStades();
            List<Caracteristique> listeC = businessManager.getCaracteristiques();
            List<Caracteristique> caracs = new List<Caracteristique>();
            stade.Caracteristiques.ForEach(x => caracs.Add(listeC.Find(c => c.Nom.Equals(x.Nom))));
            Stade newStade = listeS.First(x => x.Id == stade.Id);
            newStade.Planete = stade.Planete;
            newStade.NbPlaces = stade.NbPlaces;
            newStade.Caracteristiques = caracs;
            return 0 == businessManager.updateStades(listeS);
        }

        public bool updateTournoi(TournoiWS tournoi)
        {
            List<Tournoi> listeT = businessManager.getTournois();
            List<Match> listeM = businessManager.getMatches();
            List<Match> matches = new List<Match>();
            tournoi.Matches.ForEach(x => matches.Add(listeM.Find(c => c.Id.Equals(x.Id))));
            Tournoi newTournoi = listeT.First(x => x.Id == tournoi.Id);
            newTournoi.Nom = tournoi.Nom;
            newTournoi.Matchs = matches;
            return 0 == businessManager.updateTournois(listeT);
        }

        public TournoiWS playTournoi(TournoiWS tournoi)
        {
            Tournoi t = businessManager.getTournois().Find(x => x.Id == tournoi.Id);
            businessManager.playTournament(t);
            return new TournoiWS(t);
        }

        public int getPoints(string player)
        {
            return businessManager.getUsers().Find(x => x.Login == player).Points;
        }

        public bool setPoints(string player, int value)
        {
            List<Utilisateur> l = businessManager.getUsers();
            l.Find(x => x.Login == player).Points = value;
            return 0 == businessManager.updateUser(l.Find(x => x.Login == player));
        }

        public bool inscription(string username, string password, string nom, string prenom)
        {
            bool exist = businessManager.getUsers().Where(x => x.Login == username).Count() != 0;
            if(!exist)
            {
                businessManager.AddUser(username, password, nom, prenom);
            }
            return !exist;
        }

        public UserWS connexion(string username, string password)
        {
            bool correct = JediTournamentManager.CheckConnexionUser(username, password);
            return (correct ? new UserWS(businessManager.getUsers().Find(x => x.Login == username)) : null);
        }
    }
}
