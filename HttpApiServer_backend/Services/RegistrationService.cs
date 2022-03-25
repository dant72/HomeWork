using System.Net;
using System.Text.Json.Nodes;
using HttpApiClient;
using HttpModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public class RegistrationService : IRegistrationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountValidator _validator;
    private readonly ILogger<RegistrationService> _logger;
    private readonly IPasswordHasher<Account> _passwordHasher;
    private readonly ITokenService _tokenService;

    public RegistrationService(
        IAccountRepository accountRepository,
        ILogger<RegistrationService> logger,
        IPasswordHasher<Account> passwordHasher,
        ITokenService tokenService
        )
    {
        _accountRepository = accountRepository;
        _validator = new AccountValidator();
        _logger = logger;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;

    }
    
    public Task AddAccount(Account account)
    {
        if (ValidateAccount(account))
        {
            _accountRepository.Add(account);
        }
        
        return Task.CompletedTask;

    }
    
    public Task GetAccountById(int id)
    {
        return _accountRepository.GetById(id);
    }
    
    public Task<Account> GetAccountByEmail(string email)
    {
        return _accountRepository.GetByEmail(email);
    }
    
    public Task<Account> GetAccountByLogin(string login)
    {
        return _accountRepository.GetByLogin(login);
    }

    public async Task<IReadOnlyList<Account>> GetAccounts()
    {
        return await _accountRepository.GetAll();
    }
    
    private bool ValidateAccount(Account account)
    {
        var result = _validator.Validate(account);
        if (result.IsValid)
        {
            return true;
        }
 
        foreach(var error in result.Errors)
        {
            _logger.Log(LogLevel.Information, error.ErrorMessage);
        }
        return false;
    }

    public async Task<ActionResult<AccountResponseModel>> Autorization(AccountRequestModel account)
    {
        var acc = await GetAccountByLogin(account.Login);
        
       var isCorrectPassword = _passwordHasher.VerifyHashedPassword(acc, acc.HashPassword, account.Password) !=
               PasswordVerificationResult.Failed;

        
        if (acc != null && isCorrectPassword)
        {
            var token = _tokenService.GenerateToken(acc);
            return new AccountResponseModel(acc, token);
        }

        return new ContentResult()
        {
            StatusCode = 404
        };
    }

}