using StoreFile.DAL.Entities;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System.Linq;

namespace StoreFile.DAL.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreFileDbContext _storeFileDbContext;
        public UserRepository(StoreFileDbContext storeFileDbContext)
        {
            _storeFileDbContext = storeFileDbContext;
        }
        public void AddUser(StoredFileUserDTO registerDTO)
        {
            _storeFileDbContext.Users.Add(new StoredFileUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                BirthDay = registerDTO.BirthDay,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                Password = registerDTO.Password
            });
            _storeFileDbContext.SaveChanges();
        }
        public StoredFileUserDTO GetUserByEmail(string email)
        {
            StoredFileUser userFromDb = _storeFileDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (userFromDb == null)
            {
                return null;
            }
            return new StoredFileUserDTO
            {
                UserId = userFromDb.UserId,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                BirthDay = userFromDb.BirthDay,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Password = userFromDb.Password
            };
        }
        public StoredFileUserDTO GetUserById(int id)
        {
            StoredFileUser userFromDb = _storeFileDbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (userFromDb == null)
            {
                return null;
            }
            return new StoredFileUserDTO
            {
                UserId = userFromDb.UserId,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                BirthDay = userFromDb.BirthDay,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Password = userFromDb.Password
            };
        }
    }
}
