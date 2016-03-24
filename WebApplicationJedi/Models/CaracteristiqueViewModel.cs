using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models
{
    public class CaracteristiqueViewModel {

		public int Id { get; set; }

		[Required]
		[Display(Name = "Nom")]
		public string Nom { get; set; }

        [Required]
        [Display(Name = "Definition")]
        public EDefCaracteristiqueWS Definition { get; set; }

        [Required]
        [Display(Name = "Type")]
        public ETypeCaracteristiqueWS Type { get; set; }

        [Required]
		[Display(Name = "Valeur")]
		[Range(0,100)]
		public int Valeur { get; set; }

		public CaracteristiqueViewModel() { }

		public CaracteristiqueViewModel(CaracteristiqueWS caractetistique) {
			this.Id = caractetistique.Id;
			this.Nom = caractetistique.Nom;
            this.Definition = caractetistique.Definition;
            this.Type = caractetistique.Type;
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
