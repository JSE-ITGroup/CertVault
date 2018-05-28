using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CertVault.Models;
using CertVault.Models.DBModels;
using Microsoft.AspNet.Identity;
using log4net;

namespace CertVault.Controllers
{
    public class VaultsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void getViewData()
        {
           ViewBag.CurrentUser =  User.Identity.GetUserId();
        }

        // GET: Vaults
        public async Task<ActionResult> Index()
        {
            return View(await db.Vaults.ToListAsync());
        }

        // GET: Vaults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vault vault = await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
            return View(vault);
        }

        // GET: Vaults/Create
        [Authorize(Roles = "Clerk,Manager,Supervisor,Administrators")]
        public ActionResult Create()
        {
            getViewData();
            return View();
        }

        // POST: Vaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VaultId,Name,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Vault vault)
        {
            getViewData();
            if (ModelState.IsValid)
            {
                db.Vaults.Add(vault);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vault);
        }

        // GET: Vaults/Edit/5
        [Authorize(Roles = "Manager,Supervisor,Administrators")]
        public async Task<ActionResult> Edit(int? id)
        {
            getViewData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vault vault = await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
            return View(vault);
        }

        // POST: Vaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VaultId,Name,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Vault vault)
        {
            getViewData();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vault).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception exp)
            {
                log.Error(exp);
                return View(vault);
            }
            return View(vault);
        }

        // GET: Vaults/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vault vault = await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
            return View(vault);
        }

        // POST: Vaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vault vault = await db.Vaults.FindAsync(id);
            db.Vaults.Remove(vault);
            await db.SaveChangesAsync();
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
