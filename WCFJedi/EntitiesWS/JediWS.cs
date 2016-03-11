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
		public int Id { get; set; }
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public bool IsSith { get; set; }
        [DataMember]
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public JediWS(Jedi j)
        {
			this.Id = j.Id;
            this.Nom = j.Nom;
            this.IsSith = j.IsSith;
            this.Caracteristiques = new List<CaracteristiqueWS>();
            j.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }

        public JediWS(int pId, string pNom, bool pIsSith, List<CaracteristiqueWS> pCaracs)
        {
            this.Id = pId;
            this.Nom = pNom;
            this.IsSith = pIsSith;
            this.Caracteristiques = pCaracs;
        }
    }
}