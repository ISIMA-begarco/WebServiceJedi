using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationJedi.Models
{
    public class MatchViewModel {

		public int Id { get; set; }

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
		public ServiceReference.EPhaseTournoiWS Phase { get; set; }

		public MatchViewModel() { }

		/// <summary>
		/// Instanciation du match
		/// </summary>
		/// <param name="match">Un match du web service</param>
		/// <remarks>
		/// Les jedis sont null si c'est une phase non encore jouee
		/// </remarks>
		public MatchViewModel(ServiceReference.MatchWS match) {
			this.Id = Id;
			this.Jedi1 = ((match.Jedi1 != null) ? new JediViewModel(match.Jedi1) : null);
			this.Jedi2 = ((match.Jedi2 != null) ? new JediViewModel(match.Jedi2) : null);
			this.JediVainqueur = ((match.JediVainqueur != null) ? new JediViewModel(match.JediVainqueur) : null);
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
