using StoreFile.TL.DTO;

namespace StoreFile.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(StoredFileUserDTO registerDTO);
        StoredFileUserDTO GetUserByEmail(string email);
        StoredFileUserDTO GetUserById(int id);
    }
}
