using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFJedi
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        List<JediWS> getJedis();

        [OperationContract]
        List<StadeWS> getStades();

        [OperationContract]
        List<MatchWS> getMatches();

        [OperationContract]
        List<TournoiWS> getTournois();

        [OperationContract]
        List<CaracteristiqueWS> getCaracteristiquesOf(string jediName);

        [OperationContract]
        List<CaracteristiqueWS> getCaracteristiques();

        [OperationContract]
        bool addJedi(JediWS jedi);

        [OperationContract]
        bool addStade(StadeWS stade);

        [OperationContract]
        bool addMatch(MatchWS match);

        [OperationContract]
        bool addTournoi(TournoiWS tournoi);

        [OperationContract]
        bool updateJedi(JediWS jedi);

        [OperationContract]
        bool updateStade(StadeWS stade);

        [OperationContract]
        bool updateMatch(MatchWS match);

        [OperationContract]
        bool updateTournoi(TournoiWS tournoi);

        [OperationContract]
        bool removeJedi(JediWS jedi);

        [OperationContract]
        bool removeStade(StadeWS stade);

        [OperationContract]
        bool removeMatch(MatchWS match);

        [OperationContract]
        bool removeTournoi(TournoiWS tournoi);

        [OperationContract]
        TournoiWS playTournoi(TournoiWS tournoi);

        [OperationContract]
        int getPoints(string player);

        [OperationContract]
        bool setPoints(string player, int value);

        [OperationContract]
        bool inscription(string username, string password, string nom, string prenom);

        [OperationContract]
        UserWS connexion(string username, string password);
    }
}
