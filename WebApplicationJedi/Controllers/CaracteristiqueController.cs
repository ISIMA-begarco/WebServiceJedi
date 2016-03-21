using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Controllers {
	public class CaracteristiqueController : Controller {

		// GET: Caracteristique
		public ActionResult Index() {
			List<CaracteristiqueViewModel> list = new List<CaracteristiqueViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				foreach(var carac in service.getCaracteristiques()) {
					list.Add(new CaracteristiqueViewModel(carac));
				}
			}

			return View(new CaracteristiqueCollection(list));
		}

		// GET: Caracteristique/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Caracteristique/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Caracteristique/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Caracteristique/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Caracteristique/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Caracteristique/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Caracteristique/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}
	}
}
