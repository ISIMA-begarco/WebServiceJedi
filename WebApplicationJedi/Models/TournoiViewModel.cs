using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFJedi;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Models {
	public class TournoiViewModel {
		[Required]
		[Display(Name = "Nom du tournoi")]
		public string Nom { get; set; }

		[Required]
		[Display(Name = "Liste des matchs")]
		public MatchCollection Matches { get; set; }

		public TournoiViewModel() { }

		public TournoiViewModel(ServiceReference.TournoiWS tournoi) {
			this.Nom = tournoi.Nom;

			List<MatchViewModel> tmpList = new List<MatchViewModel>();
			foreach(var mat in tournoi.Matches) {
				tmpList.Add(new MatchViewModel(mat));
			}
			this.Matches = new MatchCollection(tmpList);
		}
	}

	public class TournoiCollection {
		public List<TournoiViewModel> list { get; set; }

		public TournoiCollection(List<TournoiViewModel> list) {
			this.list = list;
		}

		public TournoiViewModel Default {
			get { return new TournoiViewModel(); }
		}
	}
}
