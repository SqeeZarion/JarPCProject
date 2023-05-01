using JarPControlProject.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using WebAuthCommon;

namespace WebApplication3.Controllers;

//відповідає за генерацію токенів при успішній аутифікації

[Route("api/[controller]")]
[ApiController]

public class AuthController : Controller
{

    private readonly IOptions<AuthOption> authOptions;
    public AuthController(IOptions<AuthOption> authOptions )
    {
        this.authOptions = authOptions;
    }
    
    [Route("login")] // вказує на шлях URL-адреси, за яким клієнт може зробити HTTP-запит до сервера.
    [HttpPost]
    
    //[FromBody] - це атрибут дотнета, який вказує на те, що дані для
    //параметра методу контролера повинні братися з тіла запиту HTTP
    
    public IActionResult Login([FromBody] Login request)
    {
        var user = UserRepository.AuthenticateUser(request.Email, request.UserName, request.Password);

        if (user != null)
        {
            //Generate token
        }

        return Unauthorized(); //помилка 401
    }
    
    
    //токен для авторизованого користувача

    //private string GenerateJWT(){}

}