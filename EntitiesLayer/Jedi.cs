using System;
using System.Collections.Generic;

namespace EntitiesLayer
{
    public class Jedi: EntityObject
    {
        private List<Caracteristique> caracteristiques;
        private bool isSith;
        private String nom;
        private String image;


        public Jedi(int id, List<Caracteristique> caracteristiques, bool isSith, string nom, string image = "default.png")
        {
            this.caracteristiques = caracteristiques;
            this.isSith = isSith;
            this.nom = nom;
            this.image = image;
            this.Id = id;
        }

        public List<Caracteristique> Caracteristiques
        {
            get { return caracteristiques; }
            set { caracteristiques = value; }
        }

        public Uri IsSithUri
        {
            get {
                if (IsSith)
                {
                    return new Uri("../../Picture/logo_sith.png", UriKind.Relative);
                }
                else {
                    return new Uri("",UriKind.Relative);
                }
            }
        }


        public bool IsSith
        {
            get { return isSith; }
            set { isSith = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public Uri ImageUri
        {
            get { return new Uri("../../Picture/" +  Image, UriKind.Relative); }
        }

        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public double getPerception()
        {
            double retour = 0;
            foreach(Caracteristique c in Caracteristiques)
            {
                retour += (c.Definition == EDefCaracteristique.Perception ? c.Valeur : 0);
            }
            return retour;
        }

        public int getStrength()
        {
            int retour = 0;
            foreach (Caracteristique c in Caracteristiques)
            {
                retour += (c.Definition == EDefCaracteristique.Strength ? c.Valeur : 0);
            }
            return retour;
        }

        public int getDexterity()
        {
            int retour = 0;
            foreach (Caracteristique c in Caracteristiques)
            {
                retour += (c.Definition == EDefCaracteristique.Dexterity ? c.Valeur : 0);
            }
            return retour;
        }
    }
}