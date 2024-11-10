using HR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthRequest request)
        {
            var user = ValidateUserInformation(request.Username, request.Password);

            if (user == null)
                return Unauthorized();

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentcation:secretkey"]));
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Authentcation:issuer"],
                audience: _configuration["Authentcation:audience"],
                claims: new List<Claim> { },
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: signingCred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(token);
        }

        private User ValidateUserInformation(string username, string password)
        {
            // التحقق من صحة بيانات المستخدم
            var u = new User { FirstName = "", LastName = "", UserId = 0, Username = username };
            return u; 
        }
    }

}
