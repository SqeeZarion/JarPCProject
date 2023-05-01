using System.ComponentModel.DataAnnotations;

public class Login
{
    [Required] // означає, що поле обов'язково має бути
    [EmailAddress] // диктує значення формату Email
    public string Email { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}