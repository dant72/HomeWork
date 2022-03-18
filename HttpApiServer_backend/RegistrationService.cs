using System.Net;
using System.Text.Json.Nodes;
using HttpApiClient;
using HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public class RegistrationService : IRegistrationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountValidator _validator;
    private readonly ILogger<RegistrationService> _logger;

    public RegistrationService(IAccountRepository accountRepository, ILogger<RegistrationService> logger)
    {
        _accountRepository = accountRepository;
        _validator = new AccountValidator();
        _logger = logger;

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

    public async Task<IReadOnlyList<Account>> GetAccounts()
    {
        return await _accountRepository.GetAll();
    }
    
    private bool ValidateAccount(Account customer)
    {
        var result = _validator.Validate(customer);
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

    public async Task<ActionResult<Account>> Autorization(AccountRequestModel account)
    {
        var acc = await GetAccountByEmail(account.Email);
        if (acc == null)
        {
            return new ContentResult()
            {
                StatusCode = 404
            };
        }
        else
        {
            return acc;
        }
    }

}