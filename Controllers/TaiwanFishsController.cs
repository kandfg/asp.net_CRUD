using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Fish.Models;

namespace Fish.Controllers
{
    public class TaiwanFishsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: TaiwanFishs
        public ActionResult Index()
        {
            return View(db.TaiwanFish.ToList());
        }

        // GET: TaiwanFishs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiwanFish taiwanFish = db.TaiwanFish.Find(id);
            if (taiwanFish == null)
            {
                return HttpNotFound();
            }
            return View(taiwanFish);
        }

        // GET: TaiwanFishs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaiwanFishs/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Taiwan_Distribution,Habitat,Max_body_length")] TaiwanFish taiwanFish, HttpPostedFileBase Image_Path)
        {
            if (ModelState.IsValid)
            {
                if (Image_Path != null && Image_Path.ContentLength > 0)
                {
                    string filename = Path.GetFileName(Image_Path.FileName);
                    string path = Path.Combine(Server.MapPath("~/Photos"),filename);
                    Image_Path.SaveAs(path);
                    taiwanFish.Image_Path = "/Photos/"+filename;
                }
                db.TaiwanFish.Add(taiwanFish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(taiwanFish);
        }

        // GET: TaiwanFishs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiwanFish taiwanFish = db.TaiwanFish.Find(id);
            if (taiwanFish == null)
            {
                return HttpNotFound();
            }
            return View(taiwanFish);
        }

        // POST: TaiwanFishs/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Taiwan_Distribution,Habitat,Max_body_length")] TaiwanFish taiwanFish,string img_pa, HttpPostedFileBase Image_Path)
        {
            if (ModelState.IsValid)
            {
                if (Image_Path != null && Image_Path.ContentLength > 0)
                {
                    string filename = Path.GetFileName(Image_Path.FileName);
                    string path = Path.Combine(Server.MapPath("~/Photos"), filename);
                    Image_Path.SaveAs(path);
                    taiwanFish.Image_Path = "/Photos/" + filename;
                }
                else {
                    taiwanFish.Image_Path = img_pa;
                }
                db.Entry(taiwanFish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiwanFish);
        }

        // GET: TaiwanFishs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiwanFish taiwanFish = db.TaiwanFish.Find(id);
            if (taiwanFish == null)
            {
                return HttpNotFound();
            }
            return View(taiwanFish);
        }

        // POST: TaiwanFishs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiwanFish taiwanFish = db.TaiwanFish.Find(id);
            db.TaiwanFish.Remove(taiwanFish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
