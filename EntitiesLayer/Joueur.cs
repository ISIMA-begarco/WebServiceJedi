using System;

namespace EntitiesLayer
{
    public class Joueur : EntityObject
    {
        private String nom;
        private int score;

        public Joueur(int id, string nom, int score)
        {
            this.nom = nom;
            this.score = score;
            this.Id = id;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}