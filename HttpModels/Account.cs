namespace HttpModels;

public class Account : IEntity
{
    public Account(int id, string login, string email, string password)
    {
        Id = id;
        Login = login;
        Email = email;
        Password = password;
    }

    public Account()
    {
        Id = 0;
        Email = "None";
        Login = "Default";
        Password = "";
    }

    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
}