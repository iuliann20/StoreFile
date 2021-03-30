using StoreFile.BL.Logic.Interfaces;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using StoreFile.TL.Helpers;

namespace StoreFile.BL.Logic.Classes
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Response AddUser(StoredFileUserDTO registerDTO, string rePassword)
        {
            if (_userRepository.GetUserByEmail(registerDTO.Email) != null)
            {
                return new Response
                {
                    IsCompletedSuccesfuly = false,
                    ResponseMessage = "A user with the same email already exists!"
                };
            }
            if (!registerDTO.Password.Equals(rePassword))
            {
                return new Response
                {
                    IsCompletedSuccesfuly = false,
                    ResponseMessage = "Passwords doesn't match!"
                };
            }
            _userRepository.AddUser(registerDTO);
            return new Response
            {
                IsCompletedSuccesfuly = true,
                ResponseMessage = "User added succesfuly!"
            };
        }
        public StoredFileUserDTO GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
        public string GetFullName(int id)
        {
            StoredFileUserDTO userDTO = _userRepository.GetUserById(id);
            return $"{userDTO.FirstName} {userDTO.LastName}";
        }

    }
}
