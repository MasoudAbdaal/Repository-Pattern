using Domain.Constants;

namespace Domain.Entities;

internal record User : IEntity
{
    public string? Email { get; set; }

    public string? UserName { get; set; }

    public byte[]? Password { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public byte[]? ResetPasswordToken { get; set; }

    public string UserRole { get; set; } = Roles.ADMIN.ToString();

}