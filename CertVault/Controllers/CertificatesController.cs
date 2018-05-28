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
using PagedList;
using PagedList.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using Microsoft.AspNet.Identity;

namespace CertVault.Controllers
{
    public class CertificatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void  GetViewData()
        {
            ViewBag.Depopart = new SelectList(db.Database.SqlQuery<MemberId>("usp_GetDepoPartId"),"Depopart_id","Depopart_name");
            ViewBag.Vault = new SelectList(db.Vaults.ToList(),"VaultId","Name");
            ViewBag.CurrentUser = User.Identity.GetUserId();
        }

        // GET: Certificates
        public async Task<ActionResult> Index(int ? page)
        {
            GetViewData();
            var Certs = await db.Certificates.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return View(Certs.ToPagedList(page ?? 1,10));
        }

        [HttpPost]
        public async Task<ActionResult>Index(string CertNumber)
        {
            GetViewData();
            var cert =  from c in  db.Certificates select c;
            if (!String.IsNullOrEmpty(CertNumber))
            {
                cert = cert.Where(c => c.CertificateNumber == (CertNumber)).OrderByDescending(c => c.CreatedAt);
            }
            return View(await cert.OrderByDescending(x => x.CreatedAt).ToListAsync());
        }

        // GET: Certificates/Details/5
        public async Task<ActionResult> Details(string id, string id2, int? id3)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id, id3, id2);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // GET: Certificates/Create
        [Authorize(Roles = "Clerk,Manager,Supervisor,Administrators")]
        public ActionResult Create()
        {
            GetViewData();
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CertificateNumber,MemberID,SymbolIsin,Volume,Status,CreatedAt,CreatedBy,ApprovedAt,ApprovedBy,WithdrawRequestAt,WithdrawRequestBy,WithdrawApprovedAt,WithdrawApprovedBy,UpdatedAt,Client,ClientId,VaultID")] Certificate certificate)
        {
            GetViewData();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Certificates.Add(certificate);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                log.Error(exp);
                return View(certificate);
            }
            return View(certificate);
        }

        // GET: Certificates/Edit/5
        [Authorize(Roles = "Manager,Supervisor,Administrators,Clerk")]
        public async Task<ActionResult> Edit(string id, string id2,int? id3)
        {
            if (id == null && id2 == null && id3 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id,id3,id2);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CertificateNumber,MemberID,SymbolIsin,Volume,Status,CreatedAt,CreatedBy,ApprovedAt,ApprovedBy,WithdrawRequestAt,WithdrawRequestBy,WithdrawApprovedAt,WithdrawApprovedBy,UpdatedAt,Client,ClientId")] Certificate certificate)
        {
            try
            {
               if (ModelState.IsValid)
                {
                    db.Entry(certificate).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception exp)
            {
                log.Error(exp);
                return View(certificate);
            }
            return View(certificate);
        }

        // GET: Certificates/Delete/5
        [Authorize(Roles = "Manager,Supervisor,Administrators,Clerk")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Certificate certificate = await db.Certificates.FindAsync(id);
            db.Certificates.Remove(certificate);
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
