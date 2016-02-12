using System;
using System.Collections.Generic;

namespace EntitiesLayer
{
    public class Tournoi
    {
        private List<Match> matchs;
        private String nom;
        private int id;

        public Tournoi(int id, string nom, List<Match> matches)
        {
            this.Id = id;
            this.nom = nom;
            this.matchs = matches;
        }

        public List<Match> Matchs
        {
            get { return matchs; }
            set { matchs = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}