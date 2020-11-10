﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCFirstProject.DAL;
using MVCFirstProject.Models;

namespace MVCFirstProject.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index(string sortOrder)
        {
            var nameDesc = "name_desc";
            var dateDesc = "date_desc";
            var date = "Date";
            ViewBag.NameSortOrder = string.IsNullOrEmpty(sortOrder) ? nameDesc : "";
            ViewBag.DateSortOrder = sortOrder == date ? dateDesc : date;

            var students = from s in db.Students select s;
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }


            
            return View(students.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
               ModelState.AddModelError("",
                    "Unable to save changes. " +
                    "Try again, and if the problem persists see your system administrator.");
            }

            return View(student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.Students.Find(id);
            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
                new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var studentToDelete = new Student() { ID = id };
                db.Entry(studentToDelete).State = EntityState.Deleted;
            }
            catch (DataException /*dex*/)
            {
                RedirectToAction("Delete", new {id = id, saveChangesError = true});
            }

            return RedirectToAction("Index");
        }
    }
}