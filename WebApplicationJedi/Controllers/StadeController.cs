using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;

namespace WebApplicationJedi.Controllers {
	public class StadeController : Controller {
		// GET: Stade
		public ActionResult Index() {
			List<StadeViewModel> list = new List<StadeViewModel>();

			using(ServiceReference.ServiceClient service = new ServiceReference.ServiceClient()) {
				foreach(var stade in service.getStades()) {
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
			return View();
		}

		// POST: Stade/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Stade/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Stade/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Stade/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Stade/Delete/5
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
