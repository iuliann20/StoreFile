using Microsoft.AspNetCore.Http;
using StoreFile.BL.Logic.Interfaces;
using StoreFile.DAL.Repository.Interfaces;
using StoreFile.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreFile.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context, ITokenLogic _tokenLogic)
        {
            
            string value = context.Request.Cookies["AuthenticationToken"];
            if (!string.IsNullOrEmpty(value))
            {
                TokenDTO tokenDTO = _tokenLogic.GetTokenByValue(value);
                if (tokenDTO == null)
                {
                    RemoveTokenFromCookie(context, value);
                    await _next(context);
                    return;
                }
                if (DateTime.Compare(tokenDTO.ExpirationDate, DateTime.Now) < 0)
                {
                    RemoveTokenFromCookie(context, value);
                    RemoveTokenFromDb(_tokenLogic, tokenDTO.TokenId);
                    await _next(context);
                    return;
                }
            } 
            // Call the next delegate/middleware in the pipeline
            await _next(context);
            return;
        }

        private void RemoveTokenFromCookie(HttpContext context, string accessToken)
        {
            context.Response.Cookies.Append("AuthenticationToken", accessToken, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
        }
        private void RemoveTokenFromDb(ITokenLogic _tokenLogic, int tokenId)
        {
           _tokenLogic.RemoveTokenById(tokenId);
        }
    }
}
