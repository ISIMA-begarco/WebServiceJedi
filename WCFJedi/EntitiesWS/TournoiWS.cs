using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class TournoiWS
    {
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public List<MatchWS> Matches { get; set; }

        public TournoiWS (Tournoi t)
        {
            this.Nom = t.Nom;
            this.Matches = new List<MatchWS>();
            t.Matchs.ForEach(x => this.Matches.Add(new MatchWS(x)));
        }
    }
}