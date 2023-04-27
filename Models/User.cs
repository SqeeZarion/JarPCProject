using Newtonsoft.Json;

public class UserAccount
{
    public Guid UserId { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public UserTypeRole[] UserTypeRole { get; set; }

    public UserAccount(Guid userId, string email, string password, string userName, UserTypeRole[] userTypeRole)
    {
        UserId = userId;
        Email = email;
        Password = password;
        UserName = userName;
        UserTypeRole = userTypeRole;
    }

    public UserAccount(string email, string password, string userName)
    {
        Email = email;
        Password = password;
        UserName = userName;
    }
}
public enum UserTypeRole
{
    User,
    Admin
}