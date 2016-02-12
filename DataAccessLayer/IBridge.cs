using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IBridge
    {
        List<Jedi> getJedis();
        List<Stade> getStades();
        List<Match> getMatches();
        List<Caracteristique> getCaracteristiques();
        List<Tournoi> getTournois();
        List<Utilisateur> getUsers();
        int updateJedis(List<Jedi> l);
        int updateStades(List<Stade> l);
        int updateMatches(List<Match> l);
        int updateCaracteristiques(List<Caracteristique> l);
        int updateTournois(List<Tournoi> l);
        bool addUser(Utilisateur u);
        Utilisateur getUtilisateurByLogin(string login);
        bool deleteUserByLogin(string login);
    }
}
