using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using TechStudioTest.Auth;
using TechStudioTest.DataAccess;
using TechStudioTest.Models;
using TechStudioTest.Utilities;
using TechStudioTest.ViewModels;

namespace TechStudioTest.Controllers
{
    [CustomAuth]
    public class ManagerController : Controller
    {
        private CompanyContext db = new CompanyContext();

        // GET: Manager
        public ActionResult Index(int? id, int? projectID)
        {
            var viewModel = new ManagerIndexData();

            // Eager Loading
            viewModel.Managers = db.Managers
                .IncludeMulti(i => i.OfficeAssignment,
                              // Eagerly loading multiple levels
                              i => i.Projects.Select(p => p.EmployeeRoles)) // << This is not necessary, just for demo porpuse
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                // Lazy Loading
                ViewBag.ManagerID = id.Value;
                viewModel.Projects = viewModel.Managers
                    .Where(i => i.ManagerID == id.Value).Single().Projects;
            }

            if (projectID != null)
            {
                ViewBag.ProjectID = projectID.Value;
                // Explicit Loading
                var selectedProject = viewModel.Projects.Where(x => x.ProjectID == projectID).Single();
                db.Entry(selectedProject).Collection(x => x.EmployeeRoles).Load();

                foreach (EmployeeRole role in selectedProject.EmployeeRoles)
                {
                    db.Entry(role).Reference(x => x.Employee).Load();
                }

                viewModel.EmployeeRoles = selectedProject.EmployeeRoles;
            }
            return View(viewModel);
        }

        // GET: Manager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            ViewBag.ManagerID = new SelectList(db.OfficeAssignments, "ManagerID", "Location");
            return View();
        }

        // POST: Manager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerID,LastName,FirstName")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerID = new SelectList(db.OfficeAssignments, "ManagerID", "Location", manager.ManagerID);
            return View(manager);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(db.OfficeAssignments, "ManagerID", "Location", manager.ManagerID);
            return View(manager);
        }

        // POST: Manager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerID,LastName,FirstName")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.OfficeAssignments, "ManagerID", "Location", manager.ManagerID);
            return View(manager);
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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
