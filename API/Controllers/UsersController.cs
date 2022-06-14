#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API;
using API.Data;
using API.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly IUserService _service;
        public static readonly Dictionary<string, string> _firebase = new Dictionary<string, string>();

        public UsersController(IConfiguration config, IUserService service)
        {
            _configuration = config;
            _service = service;
        }

        [HttpPost("signin")]
        public IActionResult Login([FromBody][Bind("username,password")] User user)
        {
            var res = _service.SignIn(user.Username, user.Password);
            if (res != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.Username)
            };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                            _configuration["JWTParams:Issuer"],
                            _configuration["JWTParams:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: mac);

                String tokenRet = new JwtSecurityTokenHandler().WriteToken(token);
                var data = new
                {
                    token = tokenRet,
                    data = res
                };

                var json = JsonConvert.SerializeObject(data);


                return Ok(json.ToString());
            }
            return Unauthorized();
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody][Bind("username,name,password,image")] User user)
        {
            if (_service.SignUp(user.Username, user.Name, user.Password, user.Image))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.Username)
            };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                            _configuration["JWTParams:Issuer"],
                            _configuration["JWTParams:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: mac);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Unauthorized();
        }

        [HttpPost("firebase")]
        public IActionResult FirebaseRegister([FromBody][Bind("username,name")] User user)
        {
            if(_firebase.ContainsKey(user.Username))
            {
                _firebase[user.Username] = user.Name;
            }
            else
            {
                _firebase.Add(user.Username, user.Name);
            }
            return Ok();
        }
    }
}
