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
            Match newMatch = new Match(0, listeJ.Where(x => x.Nom == match.Jedi1.Nom).First(), 
                                            listeJ.Where(x => x.Nom == match.Jedi2.Nom).First(), 
                                            match.Phase, 
                                            listeS.Where(x => x.Planete == match.Stade.Planete).First(),
                                            listeJ.Where(x => x.Nom == match.JediVainqueur.Nom).First());
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

        public bool updateJedi(JediWS jedi)
        {
            throw new NotImplementedException();
        }

        public bool updateMatch(MatchWS match)
        {
            throw new NotImplementedException();
        }

        public bool updateStade(StadeWS stade)
        {
            throw new NotImplementedException();
        }

        public bool updateTournoi(TournoiWS tournoi)
        {
            throw new NotImplementedException();
        }
    }
}
