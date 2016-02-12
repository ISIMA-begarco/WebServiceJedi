using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFJedi
{
    public class JediWS
    {
        public string Nom { get; set; }
        public bool IsSith { get; set; }
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public JediWS(Jedi j)
        {
            this.Nom = j.Nom;
            this.IsSith = j.IsSith;
            j.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }
    }
}