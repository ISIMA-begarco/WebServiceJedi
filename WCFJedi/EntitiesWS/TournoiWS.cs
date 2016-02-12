using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;

namespace WCFJedi
{
    public class TournoiWS
    {
        public string Nom { get; set; }
        public List<MatchWS> Matches { get; set; }

        public TournoiWS (Tournoi t)
        {
            this.Nom = t.Nom;
            t.Matchs.ForEach(x => this.Matches.Add(new MatchWS(x)));
        }
    }
}