using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JarPControlProject.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RollbarDotNet.Payloads;
using WebApplication3.Models;
using WebAuthCommon;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebApplication3.Controllers;

//відповідає за генерацію токенів при успішній аутифікації

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IOptions<AuthOption> authOptions;
    private readonly DBConnection connection;

    public AuthController(IOptions<AuthOption> authOptions, DBConnection connection)
    {
        this.authOptions = authOptions;
        this.connection = connection;
    }


    [Route("login")] // вказує на шлях URL-адреси, за яким клієнт може зробити HTTP-запит до сервера.
    [HttpPost]
    //[FromBody] - це атрибут дотнета, який вказує на те, що дані для
    //параметра методу контролера повинні братися з тіла запиту HTTP
    public async Task<IActionResult> Login([FromBody] Login? request)
    {
        // UserAccount user = UserRepository.AuthenticateUser(request.UserName, request.Password);

        if (request != null)
        {
            var user = await connection.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName
                                                                 && u.Password == request.Password);
            if (user == null)
                return NotFound(new { Message = "User not Found" });
            //Generate token
            var token = GenerateJWT(user);

            return Ok(new
            {
                acess_token = token
            });
        }

        return Unauthorized(); //помилка 401
    }


    //токен для авторизованого користувача

    private string GenerateJWT(UserAccount user)
    {
        var authParams = authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();

        //облікові дані
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        //список об'єктів для генерації токена

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        };

        foreach (var role in user.UserTypeRole)
            claims.Add(new Claim("role", role.ToString()));

        //используется для передачи информации между двумя сторонами в виде JSON-объекта.
        //JWT - это безопасный стандартный формат, который используется для передачи утверждений между сторонами,
        //такими как серверы и приложения, и может быть использован для аутентификации и авторизации.

        //Конструктор JwtSecurityToken принимает несколько параметров,
        //включая Issuer (выдавший токен), Audience (адресат токена), claims (утверждения токена),
        //expires (время истечения срока действия токена) и signingCredentials
        //(информация о подписи, используемая для подписи токена)
        var token = new JwtSecurityToken(authParams.Issuer, authParams.Audience,
            claims, expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Route("register")] // вказує на шлях URL-адреси, за яким клієнт може зробити HTTP-запит до сервера.
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserAccount user)
    {
        if (user != null)
        {
            //Generate token
            // UserRepository.RegisterUser(user);
            await connection.Users.AddAsync(user);
            await connection.SaveChangesAsync();
            return Ok(new
            {
                Message = "Registration sucessful"
            });
        }

        return BadRequest(); //помилка 401
    }
}