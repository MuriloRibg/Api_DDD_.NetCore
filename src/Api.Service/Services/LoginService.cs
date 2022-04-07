using System.Runtime.InteropServices;
using System;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.DataTransfer;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signing;
        private TokenConfigurations _token;
        private IConfiguration _congiguration { get; }

        public LoginService
            (
                IUserRepository repository,
                SigningConfigurations signing,
                TokenConfigurations token,
                IConfiguration congiguration
            )
        {
            _repository = repository;
            _signing = signing;
            _token = token;
            _congiguration = congiguration;
        }
        public async Task<object> FindByLogin(LoginRequests user)
        {
            var baseUser = new UserEntity();
            if (user is not null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);
                if (baseUser is null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }

                ClaimsIdentity identity = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        new Claim("Nome: ", baseUser.Name),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //jti = id token
                    }
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_token.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                return SuccessObject(createDate, expirationDate, token, user);
            }
            return null;
        }

        private string CreateToken
            (
                ClaimsIdentity identity,
                DateTime createDate,
                DateTime expirationDate,
                JwtSecurityTokenHandler handler
            )
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                SigningCredentials = _signing.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(
            DateTime createDate,
            DateTime expirationDate,
            string token,
            LoginRequests user
            )
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
