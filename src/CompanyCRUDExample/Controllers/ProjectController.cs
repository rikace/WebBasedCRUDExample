using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using TechStudioTest.Auth;
using TechStudioTest.DataAccess;
using TechStudioTest.Models;

namespace TechStudioTest.Controllers
{
    [CustomAuth]
    public class ProjectController : Controller
    {
        private const string CompanyID = "CompanyID";
        private UnitOfWork uow = new UnitOfWork();

        // GET: Project
        public ActionResult Index()
        {
            var projects = uow.ProjectRepository.dbSet.Include(p => p.Company);
            return View(projects.ToList());
        }

        // GET: Project/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await uow.ProjectRepository.GetByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public async Task<ActionResult> Create()
        {
            var companies = await uow.CompanyRepository.Get();
            ViewBag.CompanyID = new SelectList(companies, CompanyID, "Name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProjectID,Title,Description,CompanyID")] Project project)
        {
            if (ModelState.IsValid)
            {
                uow.ProjectRepository.Insert(project);
                await uow.SaveAsync();
                return RedirectToAction("Index");
            }

            var companies = await uow.CompanyRepository.Get();
            ViewBag.CompanyID = new SelectList(companies, CompanyID, "Name", project.CompanyID);
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await uow.ProjectRepository.GetByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var companies = await uow.CompanyRepository.Get();
            ViewBag.CompanyID = new SelectList(companies, CompanyID, "Name", project.CompanyID);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProjectID,Title,Description,CompanyID")] Project project)
        {
            if (ModelState.IsValid)
            {
                uow.ProjectRepository.Update(project);
                await uow.SaveAsync();
                return RedirectToAction("Index");
            }
            var companies = await uow.CompanyRepository.Get();
            ViewBag.CompanyID = new SelectList(companies, CompanyID, "Name", project.CompanyID);

            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await uow.ProjectRepository.GetByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await uow.ProjectRepository.Delete(id);
            await uow.SaveAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
