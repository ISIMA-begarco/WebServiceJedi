using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class JediViewModel {

		public int Id { get; set; }

		[Required]
		[Display(Name = "Nom")]
		public string Nom { get; set; }

		[Required]
		[Display(Name = "Sith")]
		public bool IsSith { get; set; }

		[Required]
		[Display(Name = "Caractéristiques")]
		public CaracteristiqueCollection Caracteristiques { get; set; }
		// TODO ajouter les caracteristiques par la suite

		public JediViewModel() { }

		public JediViewModel(ServiceReference.JediWS jedi) {
            if(jedi != null)
            {
			    this.Id = jedi.Id;
			    this.Nom = jedi.Nom;
			    this.IsSith = jedi.IsSith;

			    List<CaracteristiqueViewModel> tmpList = new List<CaracteristiqueViewModel>();
			    foreach(var car in jedi.Caracteristiques) {
				    tmpList.Add(new CaracteristiqueViewModel(car));
			    }
			    this.Caracteristiques = new CaracteristiqueCollection(tmpList);
            }
		}
	}

	/// <summary>
	/// Cette classe permet d'etre utilisable plus facilement dans la vue
	/// </summary>
	public class JediCollection {
		/// <summary>
		/// La liste contenant des JediModels
		/// </summary>
		public List<JediViewModel> list { get; set; }

		/// <summary>
		/// Constucteur permettant de creer facilement la liste
		/// </summary>
		/// <param name="list"></param>
		public JediCollection(List<JediViewModel> list) {
			this.list = list;
		}

		/// <summary>
		/// Ce champ permet de creer les headers dans les tables de la vue
		/// </summary>
		public JediViewModel Default {
			get { return new JediViewModel(); }
		}
	}

}
