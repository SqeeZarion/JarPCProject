using System.Data.SqlClient;
using JarPControlProject.Database;
using Telegram.Bot.Types;

public class UserRepository
{
    private SqlConnection url;
    private static UserRepository user = new(DBConnection.GetConnection());
    private static List<UserAccount> accounts = user.GetListAccounts();

    public UserRepository(SqlConnection url)
    {
        this.url = url;
    }

    public UserAccount? GetIsAccounts(UserAccount user)
    {
        String select = "SELECT * FROM Users WHERE Email=? " +
                        "and Password=?and UserName=?";
        int count = 0;

        using (SqlConnection conn = DBConnection.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(select, conn))
            {
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("UserName", user.UserName);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string email = reader.GetString(reader.GetOrdinal("Email"));
                        string password = reader.GetString(reader.GetOrdinal("Password"));
                        string userName = reader.GetString(reader.GetOrdinal("UserName"));

                        return new UserAccount(email, password, userName);
                    }
                    else
                        return null;
                }
            }
        }
    }
    public List<UserAccount> GetListAccounts()
    {
        String select = "SELECT * FROM Users";
        List<UserAccount> accounts = new List<UserAccount>();

        using (SqlConnection conn = DBConnection.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(select, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid userId = reader.GetGuid(reader.GetOrdinal("UserId"));
                        string email = reader.GetString(reader.GetOrdinal("Email"));
                        string password = reader.GetString(reader.GetOrdinal("Password"));
                        string userName = reader.GetString(reader.GetOrdinal("UserName"));
                        UserTypeRole userTypeRole = (UserTypeRole)Enum.Parse(typeof(UserTypeRole),
                            reader.GetString(reader.GetOrdinal("UserTypeRole")));

                        UserAccount account = new UserAccount(userId, email, password, userName, new[] { userTypeRole });
                        accounts.Add(account);
                    }
                }
            }
        }

        return accounts;
    }

    public static object AuthenticateUser(string requestEmail, string requestUserName, string requestPassword)
    {
        UserAccount foundUser = accounts.FirstOrDefault(u => u.Email == requestEmail && u.Password == requestPassword && u.UserName == requestUserName)!;
        return foundUser;
    }
}