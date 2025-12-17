using System;
using System.Collections.Generic;
using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetUserByEmailAsync(string email);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task<bool> SoftDeleteAsync(Guid id);

    Task<int> CountByRoleAsync(string role);
}