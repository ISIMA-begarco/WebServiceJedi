using System.Collections.Generic;

namespace EntitiesLayer
{
    public class Match : EntityObject
    {
        private Jedi jediVainqueur;
        private Jedi jedi1;
        private Jedi jedi2;
        private EPhaseTournoi phaseTournoi;
        private Stade stade;

        public Match(int id, Jedi jedi1, Jedi jedi2, EPhaseTournoi phaseTournoi, Stade stade, Jedi vainqueur = null)
        {
            this.Id = id;
            this.jedi1 = jedi1;
            this.jedi2 = jedi2;
            this.phaseTournoi = phaseTournoi;
            this.stade = stade;
            this.jediVainqueur = vainqueur;
        }

       
        public Jedi JediVainqueur
        {
            get { return jediVainqueur; }
            set { jediVainqueur = value; }
        }

        public Jedi Jedi1
        {
            get { return jedi1; }
            set { jedi1 = value; }
        }

        public Jedi Jedi2
        {
            get { return jedi2; }
            set { jedi2 = value; }
        }

        public EPhaseTournoi PhaseTournoi
        {
            get { return phaseTournoi; }
            set { phaseTournoi = value; }
        }

        public Stade Stade
        {
            get { return stade; }
            set { stade = value; }
        }

        public override string ToString()
        {
            return Id + "\t\t" + (Jedi1 != null ? Jedi1.Nom : "aucun") + '\t' + (Jedi2 != null ? Jedi2.Nom : "aucun") + '\t' + Stade.Planete + '\t' + PhaseTournoi.ToString() + '\t' + (JediVainqueur != null ? JediVainqueur.Nom : "aucun");
        }
    }
}