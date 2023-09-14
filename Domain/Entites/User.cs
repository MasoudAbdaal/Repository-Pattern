using Domain.Constants;

namespace Domain.Entities;

public record User : IEntity
{
    public required string Email { get; set; }

    public required string? UserName { get; set; }

    public required byte[]? Password { get; set; }

    public required byte[]? PasswordSalt { get; set; }

    public  byte[]? ResetPasswordToken { get; set; }

    public required string UserRole { get; set; } = Roles.ADMIN.ToString();

}