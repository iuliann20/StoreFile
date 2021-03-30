using StoreFile.TL.DTO;

namespace StoreFile.DAL.Repository.Interfaces
{
    public interface ITokenRepository
    {
        TokenDTO GetTokenByValue(string tokenValue);
        void RemoveTokenById(int id);
        void AddToken(TokenDTO tokenDTO);
        void RemoveAllTokensByUserId(int userId);
    }
}
