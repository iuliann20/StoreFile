﻿using Microsoft.AspNetCore.Http;
using StoreFile.BL.Logic.Interfaces;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System;

namespace StoreFile.BL.Logic.Classes
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IUserLogic _userLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenRepository _tokenRepository;
        public AccountLogic(IHttpContextAccessor httpContextAccessor, IUserLogic userLogic, ITokenRepository tokenRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userLogic = userLogic;
            _tokenRepository = tokenRepository;
        }

        public bool IsSignedIn()
        {
            string value = _httpContextAccessor.HttpContext.Request.Cookies["AuthenticationToken"];
            if (!string.IsNullOrEmpty(value))
            {
                TokenDTO tokenDTO = _tokenRepository.GetTokenByValue(value);
                if (tokenDTO == null)
                {
                    return false;
                }
                if (DateTime.Compare(tokenDTO.ExpirationDate, DateTime.Now) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public string GetCurentUserFullName()
        {
            string value = _httpContextAccessor.HttpContext.Request.Cookies["AuthenticationToken"];
            if (!string.IsNullOrEmpty(value))
            {
                TokenDTO tokenDTO = _tokenRepository.GetTokenByValue(value);
                if (tokenDTO == null)
                {
                    return null;
                }
                if (DateTime.Compare(tokenDTO.ExpirationDate, DateTime.Now) >= 0)
                {
                    return _userLogic.GetFullName(tokenDTO.UserId);
                }
            }
            return null;
        }

        public int GetCurentUserById()
        {
            string value = _httpContextAccessor.HttpContext.Request.Cookies["AuthenticationToken"];
            if (!string.IsNullOrEmpty(value))
            {
                TokenDTO tokenDTO = _tokenRepository.GetTokenByValue(value);
                if (tokenDTO == null)
                {
                    return 0;
                }
                if (DateTime.Compare(tokenDTO.ExpirationDate, DateTime.Now) >= 0)
                {
                    return tokenDTO.UserId;
                }
            }
            return 0;
        }
        public string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        public void AddToken(TokenDTO tokenDTO)
        {
            _tokenRepository.AddToken(tokenDTO);
        }
        public void RemoveAllTokensByUserId(int userId)
        {
            _tokenRepository.RemoveAllTokensByUserId(userId);
        }

        public void Logout()
        {
            string value = _httpContextAccessor.HttpContext.Request.Cookies["AuthenticationToken"];
            if (!string.IsNullOrEmpty(value))
            {
                TokenDTO tokenDTO = _tokenRepository.GetTokenByValue(value);
                if (tokenDTO == null)
                {
                    return;
                }
                _tokenRepository.RemoveAllTokensByUserId(tokenDTO.UserId);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthenticationToken", tokenDTO.AccessToken, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }
        }

    }
}
