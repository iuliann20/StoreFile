using Microsoft.AspNetCore.Http;
using StoreFile.TL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreFile.BL.Logic.Interfaces
{
    public interface IFileLogic
    {
        string AddFile(StoredFileDTO sharedFileDTO);
        void RemoveFile(int id);
        string UpdateFile(StoredFileDTO sharedFileDTO, bool hasNameChanged);
        List<StoredFileDTO> GetAllFiles();
        StoredFileDTO GetFileById(int id);
        Task UploadFileOnDiskAsync(string uploads, IFormFile file, string fileName);
        void DeleteFileOnDisk(int id, string uploads);
        string DownloadFileAsync(int id, string uploads);
        void ReplaceFileOnDisk(IFormFile file, string uploads, bool hasNameChanged, string oldFileName);
        List<StoredFileDTO> GetAllFilesByUserId(int id);

    }
}
