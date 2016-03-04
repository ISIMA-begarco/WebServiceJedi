using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCFJedi;
using WebApplicationJedi.Models;

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
			return View();
		}

		// POST: Tournoi/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Tournoi/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Tournoi/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Tournoi/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Tournoi/Delete/5
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
