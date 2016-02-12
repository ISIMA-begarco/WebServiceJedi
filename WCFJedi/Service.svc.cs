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
    }
}
