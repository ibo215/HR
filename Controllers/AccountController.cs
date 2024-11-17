using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountForAdd accountDto)
        {
            var account = await _accountService.RegisterAccountAsync(accountDto);
            return Ok(account);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginReq)
        {
            var account = await _accountService.LoginAsync(loginReq.Username, loginReq.Password);
            return Ok(account);
        }


        [HttpDelete("DeleteMyAccount")]
        public async Task<IActionResult> DeleteMyAccount(LoginRequest loginReq)
        {
            await _accountService.DeleteMyAccountAsync(loginReq.Username, loginReq.Password);
            return Ok("Account deleted successfully.");
        }
    }

}
