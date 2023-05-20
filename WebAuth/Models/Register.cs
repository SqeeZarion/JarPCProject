using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class Register
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Token { get; set; }
    
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int UserTypeRole { get; set; }
} 