using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;

namespace Service.Dtos;

public class CreateUserRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public AccountRole Role { get; set; } = AccountRole.Student;

    [Required]
    public AccountMajor Major { get; set; } = AccountMajor.SoftwareEngineering;
}
