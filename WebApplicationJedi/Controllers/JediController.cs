using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Controllers {
	public class JediController : Controller {

		// GET: Jedi
		public ActionResult Index() {
			List<JediViewModel> list = new List<JediViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				foreach(var jedi in service.getJedis()) {
					list.Add(new JediViewModel(jedi));
				}
			}
			return View(new JediCollection(list));
		}

		// GET: Jedi/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Jedi/Create
		public ActionResult Create() {
			List<CaracteristiqueViewModel> caracList = new List<CaracteristiqueViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceClient()) {
				// On va chercher toutes les caracteristiques
				service.getCaracteristiques().ForEach(x => caracList.Add(new CaracteristiqueViewModel(x)));
			}

			/* Un tuple parce qu'il faut le jedi et les caracteristiques dispo */
			return View(Tuple.Create(new JediViewModel(), new CaracteristiqueCollection(caracList)));
		}

		// POST: Jedi/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				ServiceReference.JediWS jedi = new JediWS();
				List<CaracteristiqueWS> caracList = new List<CaracteristiqueWS>();

				using(ServiceReference.ServiceClient service = new ServiceClient()) {
					service.getCaracteristiques().ForEach(x => caracList.Add(x));

					/* Item1. sur le champs du jedi parce que on a un tuple */
					jedi.Id = 0; // Car creation
					jedi.Nom = Convert.ToString(collection.Get("Item1.Nom"));
					jedi.IsSith = Convert.ToBoolean(collection.Get("Item1.IsSith") != "false"); // Pour que ca marche bien
					jedi.Caracteristiques = new List<CaracteristiqueWS>(); // Pour init

					string[] checkboxes = collection.GetValues("caracteristiques");
					if(checkboxes != null) {
						foreach(string s in checkboxes) {
							//On a que les ids des box selected, on ajoute les caracteristiques
							Int32 caracId = Convert.ToInt32(s);
							jedi.Caracteristiques.Add(caracList.First(x => x.Id == caracId));
						}
					}

					service.addJedi(jedi); // Ajout du jedi
				}

				return RedirectToAction("Index"); // Retour a l'index
			} catch {
				return RedirectToAction("Index");
			}
		}

		// GET: Jedi/Edit/5
		public ActionResult Edit(int id) {
			ServiceReference.JediWS jedi = null;
			List<CaracteristiqueViewModel> caracList = new List<CaracteristiqueViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceClient() ) {
				jedi = service.getJedis().First(x => x.Id == id);

				if(jedi == null) {
					return HttpNotFound();
				}

				/* Selectionne toutes les caracteristiques qui ne sont pas deja dans le jedi */
				caracList = (from carac in service.getCaracteristiques()
								where !(jedi.Caracteristiques.Exists(x => x.Id == carac.Id))
								select new CaracteristiqueViewModel(carac)).ToList();

			}

			/* Tuple de vues parce qu'il faut le jedi et les autres caracteristiques */

			return View(Tuple.Create(new JediViewModel(jedi), new CaracteristiqueCollection(caracList)));
		}

		// POST: Jedi/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				ServiceReference.JediWS jedi = null;
				List<CaracteristiqueWS> caracList = new List<CaracteristiqueWS>();

				using(ServiceReference.ServiceClient service = new ServiceClient()) {
					jedi = service.getJedis().First(x => x.Id == id);
					caracList = service.getCaracteristiques();

					if(jedi == null) {
						return HttpNotFound();
					}

					/* Item1. sur le champs du jedi parce que on a un tuple */
					jedi.Nom = Convert.ToString(collection.Get("Item1.Nom"));
					jedi.IsSith = Convert.ToBoolean(collection.Get("Item1.IsSith") != "false");
					jedi.Caracteristiques = new List<CaracteristiqueWS>(); // Pour RAZ

					string[] checkboxes = collection.GetValues("caracteristiques");
					if(checkboxes != null) {
						foreach(string s in checkboxes) {
							// On a que les ids des box selected, on ajoute les caracteristiques
							Int32 caracId = Convert.ToInt32(s);
							jedi.Caracteristiques.Add(caracList.First(x => x.Id == caracId));
						}
					}

					service.updateJedi(jedi);
				}

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Jedi/Delete/5
		public ActionResult Delete(int id) {
			try {
				WebApplicationJedi.ServiceReference.JediWS jedi = null;

				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					jedi = service.getJedis().Find(x => x.Id == id); // On tente de le recuperer
				}

				if(jedi != null) { // Si on l'a eu, on le fait afficher
					return View(new JediViewModel(jedi));
				} else { // Sinon retour a l'index
					return RedirectToAction("Index");
				}
			} catch { // Les autres erreurs
				return RedirectToAction("Index");
			}
		}

		// POST: Jedi/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
					WebApplicationJedi.ServiceReference.JediWS jedi = null;
					jedi = service.getJedis().Find(x => x.Id == id); // On tente de le recuperer

					if(jedi != null) { // Si on l'a eu, on le supprime
						service.removeJedi(jedi);
					}
				}

				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Index");
			}
		}

	}
}
