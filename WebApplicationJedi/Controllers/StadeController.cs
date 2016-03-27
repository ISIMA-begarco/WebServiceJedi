using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Controllers {
	public class StadeController : Controller {
		// GET: Stade
		public ActionResult Index() {
			List<StadeViewModel> list = new List<StadeViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
                foreach (var stade in service.getStades())
                {
                    list.Add(new StadeViewModel(stade));
                }
            }

			return View(new StadeCollection(list));
		}

		// GET: Stade/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Stade/Create
		public ActionResult Create() {
			List<CaracteristiqueViewModel> caracList = new List<CaracteristiqueViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceClient()) {
				service.getCaracteristiques().ForEach(x => {
					if(x.Type == ServiceReference.ETypeCaracteristiqueWS.Stade) {
						caracList.Add(new CaracteristiqueViewModel(x));
					}
				});
			}

			return View(Tuple.Create(new StadeViewModel(), new CaracteristiqueCollection(caracList)));
		}

		// POST: Stade/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				ServiceReference.StadeWS stade = new StadeWS();
				List<CaracteristiqueWS> caracList = new List<CaracteristiqueWS>();

				using(ServiceReference.ServiceClient service = new ServiceClient()) {
					service.getCaracteristiques().ForEach(x => {
						if(x.Type == ServiceReference.ETypeCaracteristiqueWS.Stade) {
							caracList.Add(x);
						}
					});

					/* Item1 parce qu'on a un Tuple */
					stade.Id = 0;
					stade.Planete = Convert.ToString(collection.Get("Item1.Planete"));
					stade.NbPlaces = Convert.ToInt32(collection.Get("Item1.NbPlaces"));
					stade.Caracteristiques = new List<CaracteristiqueWS>();

					string[] checkboxes = collection.GetValues("caracteristiques");
					if(checkboxes != null) {
						foreach(string s in checkboxes) {
							Int32 caracId = Convert.ToInt32(s);
							stade.Caracteristiques.Add(caracList.First(x => x.Id == caracId));
						}
					}

					service.addStade(stade);
				}
				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Index");
			}
		}

		// GET: Stade/Edit/5
		public ActionResult Edit(int id) {
			ServiceReference.StadeWS stade = null;
			List<CaracteristiqueViewModel> caracList = new List<CaracteristiqueViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceClient()) {
				stade = service.getStades().First(x => x.Id == id);

				if(stade == null) {
					return HttpNotFound();
				}

				caracList = (from carac in service.getCaracteristiques()
							 where !(stade.Caracteristiques.Exists(x => x.Id == carac.Id))
								 && carac.Type == ServiceReference.ETypeCaracteristiqueWS.Stade
							 select new CaracteristiqueViewModel(carac)).ToList();
			}

			return View(Tuple.Create(new StadeViewModel(stade), new CaracteristiqueCollection(caracList)));
		}

		// POST: Stade/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				ServiceReference.StadeWS stade = null;
				List<CaracteristiqueWS> caracList = new List<CaracteristiqueWS>();

				using(ServiceReference.ServiceClient service = new ServiceClient()) {
					stade = service.getStades().First(x => x.Id == id);
					service.getCaracteristiques().ForEach(x => {
						if(x.Type == ServiceReference.ETypeCaracteristiqueWS.Stade) {
							caracList.Add(x);
						}
					});

					if(stade == null) {
						return HttpNotFound();
					}

					stade.Planete = Convert.ToString(collection.Get("Item1.Planete"));
					stade.NbPlaces = Convert.ToInt32(collection.Get("Item1.NbPlaces"));
					stade.Caracteristiques = new List<CaracteristiqueWS>();

					string[] checkboxes = collection.GetValues("caracteristiques");
					if(checkboxes != null) {
						foreach(string s in checkboxes) {
							Int32 caracId = Convert.ToInt32(s);
							stade.Caracteristiques.Add(caracList.First(x => x.Id == caracId));
						}
					}

					service.updateStade(stade);
				}

				//return View("Edit", Tuple.Create(new StadeViewModel(stade), new CaracteristiqueCollection(new List<CaracteristiqueViewModel>())));
				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Stade/Delete/5
		public ActionResult Delete(int id) {
			try {
				WebApplicationJedi.ServiceReference.StadeWS stade = null;

				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					stade = service.getStades().First(x => x.Id == id);
				}

				if(stade == null) {
					return RedirectToAction("Index");
				}

				return View(new StadeViewModel(stade));

			} catch {
				return RedirectToAction("Index");
			}
		}

		// POST: Stade/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				WebApplicationJedi.ServiceReference.StadeWS stade = null;
				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					stade = service.getStades().First(x => x.Id == id);

					if(stade != null) {
						service.removeStade(stade);
					}
				}

				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Index");
			}
		}
	}
}
