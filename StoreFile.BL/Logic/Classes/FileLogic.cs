using Microsoft.AspNetCore.Http;
using StoreFile.BL.Logic.Interfaces;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StoreFile.BL.Logic.Classes
{
    public class FileLogic : IFileLogic
    {
        private readonly IFileRepository _fileRepository;

        public FileLogic(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public string AddFile(StoredFileDTO sharedFileDTO)
        {

            sharedFileDTO.FileName = IncrementFileName(sharedFileDTO.FileName);
            _fileRepository.AddFile(sharedFileDTO);
            return sharedFileDTO.FileName;


        }

        public void RemoveFile(int id)
        {
            if (id <= 0)
            {
                return;
            }

            _fileRepository.RemoveFile(id);
        }

        public string UpdateFile(StoredFileDTO sharedFileDTO, bool hasNameChanged)
        {
            if (sharedFileDTO.Id <= 0)
            {
                return null;
            }
            if (hasNameChanged)
            {
                sharedFileDTO.FileName = IncrementFileName(sharedFileDTO.FileName);
            }
            _fileRepository.UpdateFile(sharedFileDTO);
            return sharedFileDTO.FileName;
        }
        public List<StoredFileDTO> GetAllFiles()
        {
            return _fileRepository.GetAllFiles();
        }
        public StoredFileDTO GetFileById(int id)
        {
            if (id > 0)
            {
                return _fileRepository.GetFileById(id);
            }
            return null;

        }

        public async Task UploadFileOnDiskAsync(string uploads, IFormFile file, string fileName)
        {

            string filePath = Path.Combine(uploads, fileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        public void DeleteFileOnDisk(int id, string uploads)
        {
            StoredFileDTO file = _fileRepository.GetFileById(id);
            string filePath = Path.Combine(uploads, file.FileName);
            File.Delete(filePath);

        }
        public string DownloadFileAsync(int id, string uploads)
        {
            if (id > 0 && !string.IsNullOrEmpty(uploads))
            {
                StoredFileDTO file = _fileRepository.GetFileById(id);
                string filePath = Path.Combine(uploads, file.FileName);
                return filePath;
            }
            return null;

        }
        public void ReplaceFileOnDisk(IFormFile file, string uploads, bool hasNameChanged, string oldFileName)
        {
            string fileName = file.FileName;
            if (hasNameChanged)
            {
                string oldFilePath = Path.Combine(uploads, oldFileName);
                File.Delete(oldFilePath);
                fileName = oldFileName;
            }
            UploadFileOnDiskAsync(uploads, file, fileName);

        }
        public List<StoredFileDTO> GetAllFilesByUserId(int id)
        {
            return _fileRepository.GetAllFilesByUserId(id);
        }


        private string IncrementFileName(string fileName)
        {
            string originalFileName = fileName;
            int count = 1;
            while (_fileRepository.CheckIfFileExistInDbByName(fileName))
            {
                string filextension = Path.GetExtension(originalFileName);
                fileName = originalFileName.Replace(filextension, $" ({count++}){filextension}");

            }
            return fileName;
        }


    }
}
