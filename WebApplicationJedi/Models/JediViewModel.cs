using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFJedi;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class JediViewModel {

		[Required]
		[Display(Name ="Nom")]
		public string Nom { get; set; }

		[Required]
		[Display(Name ="Est sith ?")]
		public bool IsSith { get; set; }

		[Required]
		[Display(Name ="Caractéristiques")]
		public List<string> Caracteristiques { get; set; }
		// TODO ajouter les caracteristiques par la suite

		public JediViewModel() { }

		public JediViewModel(ServiceReference.JediWS jedi) {
			this.Nom = jedi.Nom;
			this.IsSith = jedi.IsSith;
			// TODO ajouter les caracteristiques
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
