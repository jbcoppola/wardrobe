﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wardrobe.Models;

namespace Wardrobe.Controllers
{
    public class ArticlesController : Controller
    {
        private WardrobeEntities db = new WardrobeEntities();

        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Material1).Include(a => a.ArticleOccasions);
			return View(articles.ToList());
        }

		// GET: Articles (Tops)
		public ActionResult Tops()
		{
			var articles = db.Articles.Include(a => a.Material1);
			var query = from a in db.Articles
						where a.Type.Contains("Top")
						select a;
			return View(query);
		}

		// GET: Articles (Bottoms)
		public ActionResult Bottoms()
		{
			var articles = db.Articles.Include(a => a.Material1);
			var query = from a in db.Articles
						where a.Type.Contains("Bottom")
						select a;
			return View(query);
		}

		// GET: Articles (Shoes)
		public ActionResult Shoes()
		{
			var articles = db.Articles.Include(a => a.Material1);
			var query = from a in db.Articles
						where a.Type.Contains("Shoes")
						select a;
			return View(query);
		}

		// GET: Articles (Accessories)
		public ActionResult Accessories()
		{
			var articles = db.Articles.Include(a => a.Material1);
			var query = from a in db.Articles
						where a.Type.Contains("Accessory")
						select a;
			return View(query);
		}

		// GET: Articles/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
			ViewBag.Photo = article.Photo;
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.Material = new SelectList(db.Materials, "MaterialID", "Name");
			ViewBag.Occasion = new SelectList(db.Occasions, "OccasionID", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Name,Photo,Type,Color,Season,Occasion,Material")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Material = new SelectList(db.Materials, "MaterialID", "Name", article.Material);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Material = new SelectList(db.Materials, "MaterialID", "Name", article.Material);
			ViewBag.Occasion = new SelectList(db.Occasions, "OccasionID", "Name", article.Occasion);
			return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Name,Photo,Type,Color,Season,Occasion,Material")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Material = new SelectList(db.Materials, "MaterialID", "Name", article.Material);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
