using System.Net;
using System.Text.Json.Nodes;
using HttpApiClient;
using HttpApiServer_backend.Controllers;
using HttpModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public class RegistrationService : IRegistrationService
{
    private readonly AccountValidator _validator;
    private readonly ILogger<RegistrationService> _logger;
    private readonly IPasswordHasher<Account> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _uow;

    public RegistrationService(
        ILogger<RegistrationService> logger,
        IPasswordHasher<Account> passwordHasher,
        ITokenService tokenService,
        IUnitOfWork uow
        )
    {
        _validator = new AccountValidator();
        _logger = logger;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _uow = uow;

    }
    
    public async Task AddAccount(Account account)
    {
        if (ValidateAccount(account))
        {
            var accByLogin = await GetAccountByEmail(account.Email);
            var accByEmail = await GetAccountByLogin(account.Login);

            if (accByLogin != null || accByEmail != null)
                throw new AccountException();

            var cart = new Cart() { Account = account };
            await _uow.AccountRepository.Add(account);
            await _uow.CartRepository.Add(cart);
            await _uow.SaveChangesAsync();
        }
    }
    
    public Task GetAccountById(int id)
    {
        return _uow.AccountRepository.GetById(id);
    }

    public async Task BanAccount(Account account)
    {
        account.IsBanned = true;
        await _uow.AccountRepository.Update(account);
        await _uow.SaveChangesAsync();
    }
    
    public Task<Account> GetAccountByEmail(string email)
    {
        return _uow.AccountRepository.GetByEmail(email);
    }
    
    public Task<Account> GetAccountByLogin(string login)
    {
        return _uow.AccountRepository.GetByLogin(login);
    }

    public Task<IReadOnlyList<Account>> GetAccounts()
    {
        return _uow.AccountRepository.GetAll();
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