using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using StoreFile.BL.Logic.Interfaces;
using StoreFile.Helpers.Interfaces;
using StoreFile.Models;
using StoreFile.TL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StoreFile.Controllers
{
    public class ShareController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileLogic _fileLogic;
        private readonly IShareControllerHelper _shareControllerHelper;
        private readonly string _uploads;
        private readonly IAccountLogic _accountLogic;

        public ShareController(IFileLogic fileLogic, IShareControllerHelper shareControllerHelper, IWebHostEnvironment hostingEnvironment, IAccountLogic accountLogic)
        {
            _fileLogic = fileLogic;
            _shareControllerHelper = shareControllerHelper;
            _hostingEnvironment = hostingEnvironment;
            _uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            _accountLogic = accountLogic;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Files()
        {
            List<SharedFileViewModel> fileViewModels = _shareControllerHelper.BuildViewModel(_fileLogic.GetAllFilesByUserId(_accountLogic.GetCurentUserById()));
            return View(fileViewModels);
        }
        public IActionResult Delete(int id)
        {

            _fileLogic.DeleteFileOnDisk(id, _uploads);
            _fileLogic.RemoveFile(id);
            return RedirectToAction("Files");
        }

        [HttpPost]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            if (!files.Any())
            {
                return RedirectToAction("Files");
            }
            foreach (IFormFile file in files)
            {

                string newFileName = _fileLogic.AddFile(new StoredFileDTO
                {
                    UserId = _accountLogic.GetCurentUserById(),
                    FileName = file.FileName,
                    FileSize = _shareControllerHelper.FormatSize(file.Length),
                    UploadDate = DateTime.Now

                });


                _fileLogic.UploadFileOnDiskAsync(_uploads, file, newFileName);
            }
            return RedirectToAction("Files");
        }
        public IActionResult Download(int id)
        {


            string filePath = _fileLogic.DownloadFileAsync(id, _uploads);
            if (filePath == null) return NotFound();

            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }
        [HttpGet]
        public IActionResult EditFile(int id)
        {
            SharedFileViewModel model = _shareControllerHelper.BuildViewModel(_fileLogic.GetFileById(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult EditFile(int fileId, IFormFile file)
        {
            StoredFileDTO fileById = _fileLogic.GetFileById(fileId);
            bool hasNameChanged = fileById.FileName.Equals(file.FileName);
            string newFileName = _fileLogic.UpdateFile(new StoredFileDTO
            {
                Id = fileId,
                FileName = file.FileName,
                FileSize = _shareControllerHelper.FormatSize(file.Length),
                UploadDate = DateTime.Now,
                UserId= _accountLogic.GetCurentUserById()
            }, !hasNameChanged);

            _fileLogic.ReplaceFileOnDisk(file, _uploads, !hasNameChanged, newFileName);
            return RedirectToAction("Files");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

