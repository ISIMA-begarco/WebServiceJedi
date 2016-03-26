using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFJedi;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class StadeViewModel {

		public int Id { get; set; }

		[Required]
		[Display(Name = "Planète")]
		public string Planete { get; set; }

		[Required]
		[Display(Name = "Nombre de places")]
		public int NbPlaces { get; set; }

		[Required]
		[Display(Name = "Caractéristiques")]
		public CaracteristiqueCollection Caracteristiques { get; set; }

		public StadeViewModel() { }

		public StadeViewModel(ServiceReference.StadeWS stade) {
			this.Id = stade.Id;
			this.Planete = stade.Planete;
			this.NbPlaces = stade.NbPlaces;

			List<CaracteristiqueViewModel> tmpList = new List<CaracteristiqueViewModel>();
			foreach(var car in stade.Caracteristiques) {
				tmpList.Add(new CaracteristiqueViewModel(car));
			}
			this.Caracteristiques = new CaracteristiqueCollection(tmpList);
		}
	}

	public class StadeCollection {
		public List<StadeViewModel> list { get; set; }

		public StadeCollection(List<StadeViewModel> list) {
			this.list = list;
		}

		public StadeViewModel Default {
			get { return new StadeViewModel(); }
		}
	}
}
