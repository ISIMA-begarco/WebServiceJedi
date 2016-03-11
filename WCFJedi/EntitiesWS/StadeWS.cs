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
		public int Id { get; set; }
        [DataMember]
        public string Planete { get; set; }
        [DataMember]
        public int NbPlaces { get; set; }
        [DataMember]
        public List<CaracteristiqueWS> Caracteristiques { get; set; }

        public StadeWS(Stade s)
        {
			this.Id = s.Id;
            this.Planete = s.Planete;
            this.NbPlaces = s.NbPlaces;
			this.Caracteristiques = new List<CaracteristiqueWS>();
            s.Caracteristiques.ForEach(x => this.Caracteristiques.Add(new CaracteristiqueWS(x)));
        }

        public StadeWS(int pId, string pPlanete, int pNbPlaces, List<CaracteristiqueWS> pCaracs)
        {
            this.Id = pId;
            this.Planete = pPlanete;
            this.NbPlaces = pNbPlaces;
            this.Caracteristiques = pCaracs;
        }
    }
}