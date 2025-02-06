using System.ComponentModel.DataAnnotations;

namespace BackendDotNet.Data.DTOs;

public class RegisterDto
{
    [Required]
    [MaxLength(50)]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}