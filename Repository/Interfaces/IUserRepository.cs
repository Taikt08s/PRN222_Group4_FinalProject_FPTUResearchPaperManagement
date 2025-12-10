using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}