using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class StadeWS
    {
        [DataMember]
        public string Planete { get; set; }
        [DataMember]
        public int NbPlaces { get; set; }
        [DataMember]
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public StadeWS(Stade s)
        {
            this.Planete = s.Planete;
            this.NbPlaces = s.NbPlaces;
            s.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }
    }
}