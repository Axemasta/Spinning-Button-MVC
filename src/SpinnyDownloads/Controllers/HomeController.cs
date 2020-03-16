using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SpinnyDownloads.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DownloadFile()
        {
            await Task.Delay(2000);

            var downloadPath = @"C:\Temp\myfiledownload.zip";

            var fileBytes = System.IO.File.ReadAllBytes(downloadPath);

            //return File(fileBytes, "application/zip", "filedownload.zip");
            var file = File(fileBytes, "application/zip", "filedownload.zip");

            return Json(file, JsonRequestBehavior.AllowGet);
        }
    }
}