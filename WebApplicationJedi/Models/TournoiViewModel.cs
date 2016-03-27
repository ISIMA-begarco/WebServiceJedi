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

		public int Id { get; set; }


        public int Mise { get; set; }
        public int Gain { get; set; }
        public int Total { get; set; }
        public string Jedi { get; set; }

        [Required]
		[Display(Name = "Nom du tournoi")]
		public string Nom { get; set; }

        [Required]
        [Display(Name = "Liste des matchs")]
        public MatchCollection Matches { get; set; }

        [Required]
        [Display(Name = "Liste des participants")]
        public JediCollection Jedis { get; set; }

        public TournoiViewModel() { }

		public TournoiViewModel(ServiceReference.TournoiWS tournoi) {
			this.Id = tournoi.Id;
			this.Nom = tournoi.Nom;

            List<MatchViewModel> tmpList = new List<MatchViewModel>();
            List<JediViewModel> tmpList2 = new List<JediViewModel>();
            foreach (var mat in tournoi.Matches)
            {
                tmpList.Add(new MatchViewModel(mat));
                if(mat.Jedi1!=null)
                    tmpList2.Add(new JediViewModel(mat.Jedi1));
                if (mat.Jedi2 != null)
                    tmpList2.Add(new JediViewModel(mat.Jedi2));
            }
			this.Matches = new MatchCollection(tmpList);
            this.Jedis = new JediCollection(tmpList2);
		}
	}

	public class TournoiCollection
    {
        public List<TournoiViewModel> list { get; set; }
        public List<JediViewModel> participants { get; set; }

        public TournoiCollection(List<TournoiViewModel> list)
        {
            this.list = list;
        }

        public TournoiCollection(List<TournoiViewModel> list, List<JediViewModel> list2)
        {
            this.list = list;
            this.participants = list2;
        }

        public TournoiViewModel Default {
			get { return new TournoiViewModel(); }
		}
	}
}
