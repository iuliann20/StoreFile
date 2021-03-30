using StoreFile.TL.DTO;
using System.Collections.Generic;

namespace StoreFile.DAL.Repository.Interfaces
{
    public interface IFileRepository
    {
        void AddFile(StoredFileDTO sharedFileDTO);
        void RemoveFile(int id);
        void UpdateFile(StoredFileDTO sharedFileDTO);
        List<StoredFileDTO> GetAllFiles();
        StoredFileDTO GetFileById(int id);
        bool CheckIfFileExistInDbByName(string name);
        List<StoredFileDTO> GetAllFilesByUserId(int id);

    }
}
