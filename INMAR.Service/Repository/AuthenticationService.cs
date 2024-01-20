using INMAR.Service.DdContextConfiguration;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace INMAR.Service.Repository
{
    public class AuthenticationService : IAuthenticationService
    {
        public string TokenKey { get; }
        private readonly ApplicationDBContext dbContext;
        public AuthenticationService(string _tokenKey, ApplicationDBContext _dbContex)
        {
            this.dbContext = _dbContex;
            this.TokenKey = _tokenKey;
        }
        public async Task<AuthResponse> Authenticate(string Username, string phone)
        {
            try
            {
                Users user = await dbContext.users.Where(x => x.Email.Trim().ToLower() == Username.Trim().ToLower() && x.Phone == phone).FirstOrDefaultAsync();
                if (user != null)
                {

                    var tokenhandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.ASCII.GetBytes(TokenKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                                new Claim(ClaimTypes.Name, Username)

                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(tokenkey),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenhandler.CreateToken(tokenDescriptor);
                    var writtoken = tokenhandler.WriteToken(token);
                    AuthResponse resp = new AuthResponse { JwtToken = writtoken };
                    resp.ValidUser = true;
                    resp.IsActive = user.IsActive;
                    resp.StatusCode = string.Empty;
                    resp.StatusMessage = string.Empty;
                    return resp;
                }
                else
                {
                    AuthResponse auth = new AuthResponse();
                    auth.ValidUser = false;
                    auth.JwtToken = string.Empty;
                    auth.IsActive = false;
                    auth.StatusCode = string.Empty;
                    auth.StatusMessage = string.Empty;
                    return auth;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
