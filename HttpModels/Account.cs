namespace HttpModels;

public class Account : IEntity
{
    public Account(int id, string login, string email, string hashPassword)
    {
        Id = id;
        Login = login;
        Email = email;
        HashPassword = hashPassword;
    }

    public Account()
    {
        Id = 0;
        Email = "None";
        Login = "Default";
        HashPassword = "";
    }

    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Login { get; set; }
    
    public string HashPassword { get; set; }
    
}