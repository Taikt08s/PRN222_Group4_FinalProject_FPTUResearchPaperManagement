using System;
using System.Collections.Generic;
using Service.Dtos;

namespace Service.Interfaces;

public interface IUserService
{
    Task<StudentBasicInfo?> FindStudentByEmailAsync(string email);

    Task<List<UserAdminDto>> GetAllAsync();

    Task<UserAdminDto?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error)> CreateAsync(CreateUserRequest request);

    Task<(bool Success, string? Error)> UpdateAsync(UpdateUserRequest request);

    Task<(bool Success, string? Error)> DeactivateAsync(Guid id);
}