﻿using StoreFile.DAL.Entities;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace StoreFile.DAL.Repository.Classes
{
    public class TokenRepository : ITokenRepository
    {
        private readonly StoreFileDbContext _storeFileDbContext;

        public TokenRepository(StoreFileDbContext storeFileDbContext)
        {
            _storeFileDbContext = storeFileDbContext;
        }
        public void AddToken(TokenDTO tokenDTO)
        {
            _storeFileDbContext.AccessTokens.Add(new Token
            {
                AccessToken = tokenDTO.AccessToken,
                ExpirationDate = tokenDTO.ExpirationDate,
                TokenId = tokenDTO.TokenId,
                UserId = tokenDTO.UserId
            });
            _storeFileDbContext.SaveChanges();
        }

        public TokenDTO GetTokenByValue(string tokenValue)
        {
            Token tokenFromDb = _storeFileDbContext.AccessTokens.FirstOrDefault(x => x.AccessToken == tokenValue);
            if (tokenFromDb == null)
            {
                return null;
            }
            return new TokenDTO
            {
                TokenId = tokenFromDb.TokenId,
                AccessToken = tokenFromDb.AccessToken,
                ExpirationDate = tokenFromDb.ExpirationDate,
                UserId = tokenFromDb.UserId
            };
        }

        public void RemoveTokenById(int id)
        {
            Token tokenFromDb = _storeFileDbContext.AccessTokens.FirstOrDefault(x => x.TokenId == id);
            if (tokenFromDb != null)
            {
                _storeFileDbContext.AccessTokens.Remove(tokenFromDb);
                _storeFileDbContext.SaveChanges();
            }
        }
        public void RemoveAllTokensByUserId(int userId)
        {
            List<Token> tokensUser = _storeFileDbContext.AccessTokens.Where(x => x.UserId == userId).ToList();
            foreach (Token tokenUser in tokensUser)
            {
                if (tokenUser != null)
                {
                    RemoveTokenById(tokenUser.TokenId);
                }
            }
        }
    }
}
