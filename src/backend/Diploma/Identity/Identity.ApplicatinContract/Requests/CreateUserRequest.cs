using System.ComponentModel.DataAnnotations;

namespace Identity.ApplicatinContract.Requests;

public class CreateUserRequest
{
    
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}