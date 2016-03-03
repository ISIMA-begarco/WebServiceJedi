using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFJedi;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class MatchViewModel {
		[Required]
		[Display(Name = "Jedi 1")]
		public JediViewModel Jedi1 { get; set; }

		[Required]
		[Display(Name = "Jedi 2")]
		public JediViewModel Jedi2 { get; set; }

		[Required]
		[Display(Name = "Jedi vainqueur")]
		public JediViewModel JediVainqueur { get; set; }

		[Required]
		[Display(Name = "Stade")]
		public StadeViewModel Stade { get; set; }

		[Required]
		[Display(Name = "Phase du tournoi")]
		public EPhaseTournoi Phase { get; set; }

		public MatchViewModel() { }

		public MatchViewModel(ServiceReference.MatchWS match) {
			this.Jedi1 = new JediViewModel(match.Jedi1);
			this.Jedi2 = new JediViewModel(match.Jedi2);
			this.JediVainqueur = new JediViewModel(match.JediVainqueur);
			this.Stade = new StadeViewModel(match.Stade);
			this.Phase = match.Phase;
		}
	}

	public class MatchCollection {
		public List<MatchViewModel> list { get; set; }

		public MatchCollection(List<MatchViewModel> list) {
			this.list = list;
		}

		public MatchViewModel Default {
			get { return new MatchViewModel(); }
		}
	}

}
