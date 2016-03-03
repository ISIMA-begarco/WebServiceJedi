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
			return View();
		}

		// POST: Jedi/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			} catch {
				return View();
			}
		}

		// GET: Jedi/Edit/5
		public ActionResult Edit(int id) {
			return View();
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
			return View();
		}

		// POST: Jedi/Delete/5
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
