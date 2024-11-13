﻿using HR.Services;
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
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = await _accountService.LoginAsync(username, password);
            return Ok(account);
        }


        [HttpDelete("DeleteMyAccount")]
        public async Task<IActionResult> DeleteMyAccount(string username, string password)
        {
            await _accountService.DeleteMyAccountAsync(username, password);
            return Ok("Account deleted successfully.");
        }
    }

}
