using System.Collections.Generic;
using EntitiesLayer;

namespace StubDataAccessLayer
{
    public class DalManager
    {
        public List<Jedi> getJedis()
        {
            List<Jedi> jedis =new List<Jedi>(); 

            jedis.Add(new Jedi(1, null,false,"Revan"));
            jedis.Add(new Jedi(2, null,true,"Jacen Solo"));
            jedis.Add(new Jedi(3, null,false,"Cade Skywalker"));
            jedis.Add(new Jedi(4, null, true, "Darth Bane"));

            return jedis;
        }

        public List<Match> getMatches()
        {
            List<Match> matches = new List<Match>();

            matches.Add(new Match(1, new Jedi(1, null, false, "Revan"), new Jedi(2, null, true, "Jacen Solo"),EPhaseTournoi.DemiFinale1, new Stade(2, 120,"Tython",null)));
            matches.Add(new Match(2, new Jedi(3, null, false, "Cade Skywalker"), new Jedi(4, null, true, "Darth Bane"),EPhaseTournoi.DemiFinale2,new Stade(1, 120,"Nar Shaddaa",null)));
            matches.Add(new Match(3, new Jedi(2, null, true, "Jacen Solo"), new Jedi(4, null, true, "Darth Bane"), EPhaseTournoi.Finale, new Stade(3, 250, "Coruscant", null)));

            return matches;
        }

        public List<Stade> getStades()
        {
            List<Stade> stades = new List<Stade>();

            stades.Add(new Stade(1, 120, "Nar Shaddaa",null));
            stades.Add(new Stade(2, 120, "Tython", null));
            stades.Add(new Stade(3, 250, "Coruscant", null));

            return stades;
        }

        public List<Caracteristique> getCaracteristiques()
        {
            List<Caracteristique> caracteristiques = new List<Caracteristique>();

            caracteristiques.Add(new Caracteristique(1, EDefCaracteristique.Perception, "Perception",ETypeCaracteristique.Jedi, 2));
            caracteristiques.Add(new Caracteristique(2, EDefCaracteristique.Dexterity, "Dextérité", ETypeCaracteristique.Jedi, 2));
            caracteristiques.Add(new Caracteristique(3, EDefCaracteristique.Strength, "Force", ETypeCaracteristique.Jedi, 2));

            return caracteristiques;
        }

        public List<Utilisateur> GetUtilisateur()
        {
            List<Utilisateur> users = new List<Utilisateur>();

            users.Add(new Utilisateur(1, "YodaDu69", "YoloForce", "Yoda", "Master"));
            users.Add(new Utilisateur(2, "BestForceEver", "IamGod", "Doe", "John"));
            users.Add(new Utilisateur(3, "Luck", "leia24+", "Skywalker", "Luke"));

            return users;
        }

        public Utilisateur GetUtilisateurByLogin(string login)
        {
            Utilisateur user_found = null;

            foreach (Utilisateur user in GetUtilisateur())
            {
                if (user.Login.Equals(login))
                {
                    user_found = user;
                    break;
                }
            }

            return user_found;
        }
    }
}