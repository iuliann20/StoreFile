using StoreFile.Models;
using StoreFile.TL.DTO;

namespace StoreFile.Helpers.Interfaces
{
    public interface IAccountControllerHelper
    {
        StoredFileUserDTO BuildDTO(RegisterViewModel registerViewModel);
    }
}
