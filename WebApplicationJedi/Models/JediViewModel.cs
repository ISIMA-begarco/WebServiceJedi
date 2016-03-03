using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	}

	/// <summary>
	/// Cette classe permet d'etre utilisable plus facilement dans la vue
	/// </summary>
	public class JediCollection {
		public List<JediViewModel> JediModels { get; set; }

		/// <summary>
		/// Ce champ permet de creer les headers dans les tables de la vue
		/// </summary>
		public JediViewModel Default {
			get { return new JediViewModel(); }
		}
	}

}
