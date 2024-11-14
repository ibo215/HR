using AutoMapper;
using HR.Models;
using HR.Repositoreies;
using HR.ViewModels.DTOs.AccountDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(
            IAccountRepository accountRepository,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> RegisterAccountAsync(AccountForAdd accountDto)
        {
            var existingAccount = await _accountRepository.GetByUserNameAsync(accountDto.UserName);
            if (existingAccount != null)
            {
                throw new ArgumentException("Username already exists.");
            }

            if (string.IsNullOrWhiteSpace(accountDto.Password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            var account = _mapper.Map<Account>(accountDto);
            account.PasswordHash = HashPassword(accountDto.Password);

            await _accountRepository.AddAccountAsync(account);

            var token = GenerateJwtToken(account);
            return token;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var account = await _accountRepository.GetByUserNameAsync(username);
            if (account == null)
            {
                throw new UnauthorizedAccessException("Username does not exist.");
            }

            if (!VerifyPassword(password, account.PasswordHash))
            {
                throw new UnauthorizedAccessException("Incorrect password.");
            }


            var token = GenerateJwtToken(account);
            return token;
        }

        public async Task DeleteMyAccountAsync(string username, string password)
        {
            var account = await _accountRepository.GetByUserNameAsync(username);
            if (account == null || !VerifyPassword(password, account.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            await _accountRepository.DeleteAccountAsync(account.Id);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string GenerateJwtToken(Account account)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:secretkey"]));
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
            };

            if (account.UserName.ToLower() == _configuration["Admin:UserName"].ToLower())
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:issuer"],
                audience: _configuration["Authentication:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: signingCred
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
