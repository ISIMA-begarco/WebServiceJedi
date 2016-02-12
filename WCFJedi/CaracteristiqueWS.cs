using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;


namespace WCFJedi
{
    public class CaracteristiqueWS
    {
        public string Nom { get; set; }
        public EDefCaracteristique Definition { get; set; }
        public int Valeur { get; set; }
        public CaracteristiqueWS(Caracteristique c)
        {
            this.Nom = c.Nom;
            this.Definition = c.Definition;
            this.Valeur = c.Valeur;
        }
    }
}