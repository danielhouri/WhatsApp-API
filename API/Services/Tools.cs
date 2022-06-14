using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    static class Tools
    {
        public static string getUsername(string authHeader)
        {
            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var token1 = handler.ReadToken(token) as JwtSecurityToken;
            return token1.Claims.First(e => e.Type == "UserId").Value;
        }
    }
}
