using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationJedi.Models;
using WebApplicationJedi.ServiceReference;

namespace WebApplicationJedi.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            List<TournoiViewModel> list = new List<TournoiViewModel>();

            using (ServiceReference.ServiceClient service = new ServiceReference.ServiceClient())
            {
                foreach (var t in service.getTournois())
                {
                    list.Add(new TournoiViewModel(t));
                }
            }

            return View(new TournoiCollection(list));
        }

        // POST: Jedi/Create
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            TournoiWS ts = null;
            string nom = collection.Get(1);
            using (ServiceReference.ServiceClient service = new ServiceReference.ServiceClient())
            {
                TournoiWS tn = service.getTournois().Where(x => x.Nom == nom).First();
                ts = service.playTournoi(tn);
            }
            return View("Details", new TournoiViewModel(ts));
        }

        public ActionResult TournoiSelected(string tournoi)
        {
            JediCollection jedis = null;
            MatchCollection matches = null;
            using (ServiceReference.ServiceClient service = new ServiceReference.ServiceClient())
            {
                List<MatchViewModel> tmpList = new List<MatchViewModel>();
                List<JediViewModel> tmpList2 = new List<JediViewModel>();
                foreach (MatchWS mat in ((TournoiWS)service.getTournois().Select(x => x.Nom == tournoi)).Matches)
                {
                    tmpList.Add(new MatchViewModel(mat));
                    if (mat.Jedi1 != null)
                        tmpList2.Add(new JediViewModel(mat.Jedi1));
                    if (mat.Jedi2 != null)
                        tmpList2.Add(new JediViewModel(mat.Jedi2));
                }
                matches = new MatchCollection(tmpList);
                jedis = new JediCollection(tmpList2);
            }
            return Json(jedis, JsonRequestBehavior.AllowGet);
        }

        // GET: Game/Details/5
        public ActionResult Details(TournoiViewModel tws)
        {
            return View(tws);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
