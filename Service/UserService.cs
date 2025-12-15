using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessObject.Models;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<StudentBasicInfo?> FindStudentByEmailAsync(string email)
    {
        var user = await _repo.GetUserByEmailAsync(email);
        return _mapper.Map<StudentBasicInfo?>(user);
    }

    public async Task<List<UserAdminDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return _mapper.Map<List<UserAdminDto>>(users);
    }

    public async Task<UserAdminDto?> GetByIdAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserAdminDto>(user);
    }

    public async Task<(bool Success, string? Error)> CreateAsync(CreateUserRequest request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedName = request.FullName.Trim();

        var existing = await _repo.GetByEmailAsync(normalizedEmail);
        if (existing != null)
        {
            return (false, "Email đã tồn tại.");
        }

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Full_Name = normalizedName,
            Email = normalizedEmail,
            Password_Hash = request.Password,
            Role = request.Role.ToString(),
            Major = request.Major.ToString(),
            Is_Active = true,
            Is_Suspended = false,
            Created_At = DateTime.UtcNow
        };

        await _repo.CreateAsync(newUser);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(UpdateUserRequest request)
    {
        var user = await _repo.GetByIdAsync(request.Id);
        if (user == null)
        {
            return (false, "Không tìm thấy người dùng.");
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedName = request.FullName.Trim();

        var duplicatedEmail = await _repo.GetByEmailAsync(normalizedEmail);
        if (duplicatedEmail != null && duplicatedEmail.Id != request.Id)
        {
            return (false, "Email đã được sử dụng.");
        }

        user.Full_Name = normalizedName;
        user.Email = normalizedEmail;
        user.Role = request.Role.ToString();
        user.Major = request.Major.ToString();
        user.Is_Suspended = request.IsSuspended;
        user.Suspended_Until = request.IsSuspended ? request.SuspendedUntil : null;

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.Password_Hash = request.Password;
        }

        await _repo.UpdateAsync(user);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeactivateAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null)
        {
            return (false, "Không tìm thấy người dùng.");
        }

        if (!user.Is_Active)
        {
            return (false, "Người dùng đã bị vô hiệu hoá trước đó.");
        }

        var success = await _repo.SoftDeleteAsync(id);
        return success
            ? (true, null)
            : (false, "Không thể cập nhật trạng thái người dùng.");
    }
}