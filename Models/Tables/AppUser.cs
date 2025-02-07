using System.ComponentModel.DataAnnotations;

namespace BackendDotNet.Models.Tables;

public class AppUser
{
    [Key]
    public int Id { get; set; }

    [MaxLength(32)]
    public required string UserName { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }
}