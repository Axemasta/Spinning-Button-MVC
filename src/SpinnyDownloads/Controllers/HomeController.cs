using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SpinnyDownloads.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _downloadFilePath;
        private readonly string _downloadFileName;
        private readonly string _downloadFileType;

        public HomeController()
        {
            _downloadFilePath = ConfigurationManager.AppSettings["DownloadFilePath"];
            _downloadFileName = ConfigurationManager.AppSettings["DownloadFileName"];
            _downloadFileType = ConfigurationManager.AppSettings["DownloadFileType"];
        }

        private byte[] GetDownloadFile()
        {
            if (!System.IO.File.Exists(_downloadFilePath))
            {
                throw new FileNotFoundException("Please add valid web.config for value: DownloadFilePath");
            }

            return System.IO.File.ReadAllBytes(_downloadFilePath);
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DirectDownload()
        {
            await Task.Delay(2000);

            var fileBytes = GetDownloadFile();

            return File(fileBytes, _downloadFileType, _downloadFileName);
        }

        public async Task<ActionResult> IndirectDownload()
        {
            await Task.Delay(2000);

            var fileBytes = GetDownloadFile();

            var file = File(fileBytes, _downloadFileType, _downloadFileName);

            return Json(file, JsonRequestBehavior.AllowGet);
        }
    }
}