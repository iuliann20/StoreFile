using StoreFile.DAL.Entities;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace StoreFile.DAL.Repository.Classes
{
    public class FileRepository : IFileRepository
    {
        private readonly StoreFileDbContext _storeFileDbContext;
        public FileRepository(StoreFileDbContext storeFileDbContext)
        {
            _storeFileDbContext = storeFileDbContext;
        }


        public void AddFile(StoredFileDTO sharedFileDTO)
        {
            _storeFileDbContext.Files.Add(new StoredFile
            {
                UserId= sharedFileDTO.UserId,
                FileName = sharedFileDTO.FileName,
                FileSize = sharedFileDTO.FileSize,
                UploadDate = sharedFileDTO.UploadDate
            });
            _storeFileDbContext.SaveChanges();
        }

        public void RemoveFile(int id)
        {
            StoredFile fileFromDb = _storeFileDbContext.Files.FirstOrDefault(x => x.Id == id);
            if (fileFromDb != null)
            {
                _storeFileDbContext.Files.Remove(fileFromDb);
                _storeFileDbContext.SaveChanges();
            }
        }

        public void UpdateFile(StoredFileDTO sharedFileDTO)
        {
            StoredFile fileFromDb = _storeFileDbContext.Files.FirstOrDefault(x => x.Id == sharedFileDTO.Id);
            if (fileFromDb != null)
            {
                fileFromDb.FileName = !string.IsNullOrEmpty(sharedFileDTO.FileName) ? sharedFileDTO.FileName : fileFromDb.FileName;
                fileFromDb.FileSize = !string.IsNullOrEmpty(sharedFileDTO.FileSize) ? sharedFileDTO.FileSize : fileFromDb.FileSize;
                fileFromDb.UploadDate = sharedFileDTO.UploadDate != null ? sharedFileDTO.UploadDate : fileFromDb.UploadDate;
                _storeFileDbContext.SaveChanges();
            }
        }
        public List<StoredFileDTO> GetAllFiles()
        {
            return _storeFileDbContext.Files
              .Select(file => new StoredFileDTO
              {
                  Id = file.Id,
                  FileName = file.FileName,
                  FileSize = file.FileSize,
                  UploadDate = file.UploadDate
              }).ToList();

        }

        public List<StoredFileDTO> GetAllFilesByUserId(int id)
        {
            return _storeFileDbContext.Files.Where(x => x.UserId == id)
              .Select(file => new StoredFileDTO
              {
                  Id = file.Id,
                  FileName = file.FileName,
                  FileSize = file.FileSize,
                  UploadDate = file.UploadDate
              }).ToList();
        }

        public StoredFileDTO GetFileById(int id)
        {
            StoredFile fileFromDb = _storeFileDbContext.Files.FirstOrDefault(x => x.Id == id);
            if (fileFromDb != null)
            {
                return new StoredFileDTO
                {
                    Id = fileFromDb.Id,
                    FileName = fileFromDb.FileName,
                    FileSize = fileFromDb.FileSize,
                    UploadDate = fileFromDb.UploadDate
                };
            }
            return null;
        }
        public bool CheckIfFileExistInDbByName(string name)
        {
            StoredFile fileFromDb = _storeFileDbContext.Files.FirstOrDefault(x => x.FileName == name);
            return fileFromDb != null;
        }



    }
}
