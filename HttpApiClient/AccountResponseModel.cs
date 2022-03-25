using HttpModels;

namespace HttpApiClient;

public class AccountResponseModel
{
    public Account Account { get; set; }
    public string? Token { get; set; }

    public AccountResponseModel(Account account, string? token = null)
    {
        Account = account;
        Token = token;
    }
}