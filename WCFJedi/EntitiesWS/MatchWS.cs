using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class MatchWS
    {
        [DataMember]
		public int Id { get; set; }
        [DataMember]
        public JediWS Jedi1 { get; set; }
        [DataMember]
        public JediWS Jedi2 { get; set; }
        [DataMember]
        public JediWS JediVainqueur { get; set; }
        [DataMember]
        public StadeWS Stade { get; set; }
        [DataMember]
        public EPhaseTournoiWS Phase { get; set; }

        public MatchWS(Match m)
        {
			this.Id = m.Id;
            this.Jedi1 = m.Jedi1 != null ? new JediWS(m.Jedi1) : null;
            this.Jedi2 = m.Jedi2 != null ? new JediWS(m.Jedi2) : null;
            this.JediVainqueur = m.JediVainqueur != null ? new JediWS(m.JediVainqueur) : null;
            this.Stade = new StadeWS(m.Stade);
            this.Phase = (EPhaseTournoiWS)m.PhaseTournoi;
        }

        public MatchWS(int pId, JediWS pJedi1, JediWS pJedi2, JediWS pJediV, StadeWS pStade, EPhaseTournoi pPhase)
        {
            this.Id = pId;
            this.Jedi1 = pJedi1;
            this.Jedi2 = pJedi2;
            this.JediVainqueur = pJediV;
            this.Stade = pStade;
            this.Phase = (EPhaseTournoiWS)pPhase;
        }
    }
}