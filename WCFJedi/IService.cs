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
        bool addJedi(JediWS jedi);
    }
}
