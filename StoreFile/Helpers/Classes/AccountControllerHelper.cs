using StoreFile.Helpers.Interfaces;
using StoreFile.Models;
using StoreFile.TL.DTO;

namespace StoreFile.Helpers.Classes
{
    public class AccountControllerHelper : IAccountControllerHelper
    {
        public StoredFileUserDTO BuildDTO(RegisterViewModel registerViewModel)
        {
            return new StoredFileUserDTO
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                BirthDay = registerViewModel.BirthDay,
                PhoneNumber = registerViewModel.PhoneNumber,
                UserId = registerViewModel.UserId,
                Password = registerViewModel.Password,
                Email = registerViewModel.Email

            };
        }

    }
}
