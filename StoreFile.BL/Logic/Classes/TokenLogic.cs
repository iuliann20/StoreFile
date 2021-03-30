using Microsoft.AspNetCore.Http;
using StoreFile.BL.Logic.Interfaces;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFile.BL.Logic.Classes
{
   
    public class TokenLogic : ITokenLogic
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenLogic(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public TokenDTO GetTokenByValue(string tokenValue)
        {
            return _tokenRepository.GetTokenByValue(tokenValue);
        }

        public void RemoveTokenById(int id)
        {
            _tokenRepository.RemoveTokenById(id);
        }
    }
}
