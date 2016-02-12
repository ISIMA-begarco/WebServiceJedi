using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;

namespace WCFJedi
{
    public class MatchWS
    {
        public JediWS Jedi1 { get; set; }
        public JediWS Jedi2 { get; set; }
        public JediWS JediVainqueur { get; set; }
        public StadeWS Stade { get; set; }
        public EPhaseTournoi Phase { get; set; }

        public MatchWS(Match m)
        {
            this.Jedi1 = new JediWS(m.Jedi1);
            this.Jedi2 = new JediWS(m.Jedi2);
            this.JediVainqueur = new JediWS(m.JediVainqueur);
            this.Stade = new StadeWS(m.Stade);
            this.Phase = m.PhaseTournoi;
        }
    }
}