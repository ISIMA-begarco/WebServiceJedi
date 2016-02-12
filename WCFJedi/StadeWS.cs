using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;

namespace WCFJedi
{
    public class StadeWS
    {
        public string Planete { get; set; }
        public int NbPlaces { get; set; }
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public StadeWS(Stade s)
        {
            this.Planete = s.Planete;
            this.NbPlaces = s.NbPlaces;
            s.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }
    }
}