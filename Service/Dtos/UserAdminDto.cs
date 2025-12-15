using System;

namespace Service.Dtos;

public class UserAdminDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsSuspended { get; set; }
    public DateTime? SuspendedUntil { get; set; }
    public DateTime CreatedAt { get; set; }
}
