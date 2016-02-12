using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class JediWS
    {
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public bool IsSith { get; set; }
        [DataMember]
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public JediWS(Jedi j)
        {
            this.Nom = j.Nom;
            this.IsSith = j.IsSith;
            j.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }
    }
}