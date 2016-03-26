using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Controllers {
	public class TournoiController : Controller {
		// GET: Tournoi
		public ActionResult Index() {
			List<TournoiViewModel> list = new List<TournoiViewModel>();
			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				foreach(var tournoi in service.getTournois()) {
					list.Add(new TournoiViewModel(tournoi));
				}
			}

			return View(new TournoiCollection(list));
		}

		// GET: Tournoi/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Tournoi/Create
		public ActionResult Create() {
			List<JediViewModel> jediList = new List<JediViewModel>();
			List<StadeViewModel> stadeListe = new List<StadeViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				service.getJedis().ForEach(x => jediList.Add(new JediViewModel(x)));
				service.getStades().ForEach(x => stadeListe.Add(new StadeViewModel(x)));
			}

			return View(Tuple.Create(new TournoiViewModel(), new JediCollection(jediList), new StadeCollection(stadeListe)));
		}

		// POST: Tournoi/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			//try {
				ServiceReference.TournoiWS tournoi = new ServiceReference.TournoiWS();
				List<JediWS> jediList = new List<JediWS>();
				List<StadeWS> stadeList = new List<StadeWS>();

				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					service.getJedis().ForEach(x => jediList.Add(x));
					service.getStades().ForEach(x => stadeList.Add(x));

					tournoi.Id = 0;
					tournoi.Nom = Convert.ToString(collection.Get("Item1.Nom"));
					tournoi.Matches = new List<MatchWS>();

					// Va chercher jedi1, jedi2 et stade pour les huitieme
					for(int i = (int)WebApplicationJedi.ServiceReference.EPhaseTournoi.HuitiemeFinale1; i >= (int)WebApplicationJedi.ServiceReference.EPhaseTournoi.HuitiemeFinale8; i--) {
						MatchWS m = new MatchWS();
						m.Id = 0;
						m.JediVainqueur = null;
						m.Phase = ((WebApplicationJedi.ServiceReference.EPhaseTournoi)i);
						m.Stade = stadeList.First(x => x.Id == Convert.ToInt32(collection.Get("stadefor" + i)));
						m.Jedi1 = jediList.First(x => x.Id == Convert.ToInt32(collection.Get("jedi1for" + i)));
						m.Jedi2 = jediList.First(x => x.Id == Convert.ToInt32(collection.Get("jedi2for" + i)));
						tournoi.Matches.Add(m);
						// TODO : ai-je besoin des participants ?
					}

					// Va chercher stade pour les autres phases
					for(int i = (int)WebApplicationJedi.ServiceReference.EPhaseTournoi.QuartFinale1; i >= (int)WebApplicationJedi.ServiceReference.EPhaseTournoi.Finale; i--) {
						MatchWS m = new MatchWS();
						m.Id = 0;
						m.JediVainqueur = null;
						m.Jedi1 = null;
						m.Jedi2 = null;
						m.Phase = ((WebApplicationJedi.ServiceReference.EPhaseTournoi)i);
						m.Stade = stadeList.First(x => x.Id == Convert.ToInt32(collection.Get("stadefor" + i)));

						tournoi.Matches.Add(m);
					}

					foreach(var m in tournoi.Matches) {
						service.addMatch(m);
					}
					service.addTournoi(tournoi);
				}

				return RedirectToAction("Index");
			//} catch {
			//	return View("Error");
			//}
		}

		// GET: Tournoi/Edit/5
		public ActionResult Edit(int id) {
			ServiceReference.TournoiWS tournoi = null;
			List<JediViewModel> jediList = new List<JediViewModel>();
			List<StadeViewModel> stadeList = new List<StadeViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				tournoi = service.getTournois().First(x => x.Id == id);

				if(tournoi == null) {
					return HttpNotFound();
				}

				service.getJedis().ForEach(x => jediList.Add(new JediViewModel(x)));
				service.getStades().ForEach(x => stadeList.Add(new StadeViewModel(x)));
			}

			return View(Tuple.Create(new TournoiViewModel(tournoi), new JediCollection(jediList), new StadeCollection(stadeList)));
		}

		// POST: Tournoi/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				ServiceReference.TournoiWS tournoi = null;
				List<JediWS> jediList = new List<JediWS>();
				List<StadeWS> stadeList = new List<StadeWS>();

				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					tournoi = service.getTournois().First(x => x.Id == id);

					if(tournoi == null) {
						return HttpNotFound();
					}

					service.getJedis().ForEach(x => jediList.Add(x));
					service.getStades().ForEach(x => stadeList.Add(x));

					tournoi.Nom = Convert.ToString(collection.Get("Item1.Nom"));

					// Mise a jour des matchs
					foreach(var m in tournoi.Matches) {
						int i = ((int)m.Phase);
						if(m.Phase >= EPhaseTournoi.HuitiemeFinale8 && m.Phase <= EPhaseTournoi.HuitiemeFinale1) { // Mise a jour jedis
							m.Jedi1 = jediList.First(x => x.Id == Convert.ToInt32(collection.Get("jedi1for" + i)));
							m.Jedi2 = jediList.First(x => x.Id == Convert.ToInt32(collection.Get("jedi2for" + i)));
						} else {
							m.Jedi1 = null;
							m.Jedi2 = null;
						}
						m.JediVainqueur = null;
						m.Stade = stadeList.First(x => x.Id == Convert.ToInt32(collection.Get("stadefor" + i)));
					}

					foreach(var m in tournoi.Matches) {
						service.updateMatch(m);
					}
					service.updateTournoi(tournoi);
				}

				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Error");
			}
		}

		// GET: Tournoi/Delete/5
		public ActionResult Delete(int id) {
			WebApplicationJedi.ServiceReference.TournoiWS tournoi = null;

			using(ServiceReference.ServiceClient service = new ServiceClient()) {
				tournoi = service.getTournois().First(x => x.Id == id);
			}

			if(tournoi != null) {
				return View(new TournoiViewModel(tournoi));
			} else {
				return RedirectToAction("Index");
			}
		}

		// POST: Tournoi/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				WebApplicationJedi.ServiceReference.TournoiWS tournoi = null;
				using(ServiceReference.ServiceClient service = new ServiceClient()) {
					tournoi = service.getTournois().First(x => x.Id == id);

					if(tournoi != null) {
						foreach(var m in tournoi.Matches) {
							service.removeMatch(m);
						}
						service.removeTournoi(tournoi);
					}
				}

				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Index");
			}
		}
	}
}
