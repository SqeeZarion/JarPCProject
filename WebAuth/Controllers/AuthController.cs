using JarPControlProject.Database;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;

//відповідає за генерацію токенів при успішній аутифікації

[Route("api/[controller]")]
[ApiController]

public class AuthController : Controller
{

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
    
    
}