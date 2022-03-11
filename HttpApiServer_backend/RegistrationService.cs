using HttpModels;

namespace HttpApiServer_backend;

public class RegistrationService : IRegistrationService
{
    private readonly IAccountRepository _accountRepository;

    public RegistrationService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public Task AddAccount(Account product)
    {
        _accountRepository.Add(product);
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
}