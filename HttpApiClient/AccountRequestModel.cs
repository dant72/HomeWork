namespace HttpApiClient;

public class AccountRequestModel
{
    public string Email { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }

    public AccountRequestModel(string login, string password, string email)
    {
        Login = login;
        Password = password;
        Email = email;
    }
}