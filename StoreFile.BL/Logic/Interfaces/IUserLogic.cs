using StoreFile.TL.DTO;
using StoreFile.TL.Helpers;

namespace StoreFile.BL.Logic.Interfaces
{
    public interface IUserLogic
    {
        Response AddUser(StoredFileUserDTO registerDTO, string rePassword);
        StoredFileUserDTO GetUserByEmail(string email);
        string GetFullName(int id);
    }
}
