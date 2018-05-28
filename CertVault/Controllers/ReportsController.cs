using CertVault.Models;
using CertVault.Models.DBModels;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CertVault.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string userName;
        string password;
        string reportPath;
        string database;
        string server;
        // GET: Reports
        public ActionResult Index()
        {
            GetViewData();
            return View();
        }

        public void GetViewData()
        {
            ViewBag.Client = new SelectList(db.Clients.ToList(), "Name", "Name");
            ViewBag.Status = new SelectList(db.Database.SqlQuery<Status>("usp_getStatus"), "StatusName", "StatusName");
            ViewBag.Vaults = new SelectList(db.Vaults.ToList(), "Name", "Name");
        }

        [HttpPost]
        public ActionResult DownloadJCSDCertificateVaultReport(string Symbol, string ClientName, string Status, string CertificateNumber, string VaultName)
        {

            userName = Properties.Settings.Default.UserName;
             password= Properties.Settings.Default.Password;
            reportPath = Properties.Settings.Default.ReportPath;
            database= Properties.Settings.Default.DatabaseName;
             server= Properties.Settings.Default.Server;

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath(reportPath));
            rd.SetParameterValue("@CertificateNumber", string.IsNullOrEmpty(CertificateNumber) ? DBNull.Value : (object)CertificateNumber);
            rd.SetParameterValue("@SymbolIsin", string.IsNullOrEmpty(Symbol) ? DBNull.Value : (object)Symbol);
            rd.SetParameterValue("@ClientName", string.IsNullOrEmpty(ClientName) ? DBNull.Value : (object)ClientName);
            rd.SetParameterValue("@VaultName", string.IsNullOrEmpty(VaultName) ? DBNull.Value : (object)VaultName);
            rd.SetParameterValue("@Status", string.IsNullOrEmpty(Status) ? DBNull.Value : (object)Status);
            rd.SetDatabaseLogon(userName, password, server,database);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Cert Vault Report - " + ClientName + ".pdf");
        }
    }
}