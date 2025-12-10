using BusinessObject.Models;

namespace Service.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}