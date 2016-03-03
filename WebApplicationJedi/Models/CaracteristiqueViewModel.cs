using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFJedi;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class CaracteristiqueViewModel {

		[Required]
		[Display(Name = "Nom")]
		public string Nom { get; set; }

		[Required]
		[Display(Name = "Definition")]
		public EDefCaracteristique Definition { get; set; }

		[Required]
		[Display(Name = "Valeur")]
		[Range(0,100)]
		public int Valeur { get; set; }

		public CaracteristiqueViewModel() { }

		public CaracteristiqueViewModel(ServiceReference.CaracteristiqueWS caractetistique) {
			this.Nom = caractetistique.Nom;
			this.Definition = caractetistique.Definition;
            this.Valeur = caractetistique.Valeur;
		}
	}

	public class CaracteristiqueCollection {
		public List<CaracteristiqueViewModel> list { get; set; }

		public CaracteristiqueCollection(List<CaracteristiqueViewModel> list) {
			this.list = list;
		}

		public CaracteristiqueViewModel Default {
			get { return new CaracteristiqueViewModel(); }
		}
	}
}
