using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class CaracteristiqueWS
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public EDefCaracteristiqueWS Definition { get; set; }
        [DataMember]
        public ETypeCaracteristiqueWS Type { get; set; }
        [DataMember]
        public int Valeur { get; set; }
        
        public CaracteristiqueWS(Caracteristique c)
        {
            this.Id = c.Id;
            this.Nom = c.Nom;
            this.Type = (ETypeCaracteristiqueWS)c.Type;
            this.Definition = (EDefCaracteristiqueWS) c.Definition;
            this.Valeur = c.Valeur;
        }
    }
}