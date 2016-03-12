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
			return View(new JediViewModel());
		}

		// POST: Jedi/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			Console.WriteLine("Creation d'un nouveau jedi");
			try {
				// TODO: Add insert logic here

				WebApplicationJedi.ServiceReference.JediWS jedi = new JediWS();
				jedi.Id = 0; // Parce qu'on va le creer
				jedi.Nom = Convert.ToString(collection.Get("Nom"));
				jedi.IsSith = Convert.ToBoolean(collection.Get("IsSith"));

				jedi.Caracteristiques = new List<WebApplicationJedi.ServiceReference.CaracteristiqueWS>();
				// TODO recuperer les caracteristiques et les mettre la

				return View("Edit", new JediViewModel(jedi));//truc de test
															 //return RedirectToAction("Index"); // Le bon truc
		} catch {
				return RedirectToAction("Index");
		}
}

		// GET: Jedi/Edit/5
		public ActionResult Edit(int id) {
			ServiceReference.JediWS jedi = null;
			using(ServiceReference.ServiceClient service = new ServiceClient() ) {
				jedi = service.getJedis().First(x => x.Id == id);
			}

			if(jedi == null) {
				return HttpNotFound();
			}

			return View(new JediViewModel(jedi));
		}

		// POST: Jedi/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

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

		//[HttpPost, ActionName("Delete")]
		//public ActionResult DeleteConfirmed(int id) {
		//	try {
		//		using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
		//			WebApplicationJedi.ServiceReference.JediWS jedi = null;
		//			jedi = service.getJedis().Find(x => x.Id == id); // On tente de le recuperer

		//			if(jedi != null) { // Si on l'a eu, on le supprime
		//				service.removeJedi(jedi);
		//			}
		//		}

		//		return RedirectToAction("Index");
		//	} catch {
		//		return RedirectToAction("Index");
		//	}
		//}
	}
}
