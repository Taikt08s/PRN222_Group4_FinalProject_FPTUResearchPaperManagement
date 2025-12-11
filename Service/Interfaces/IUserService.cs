using Service.Dtos;

namespace Service.Interfaces;

public interface IUserService
{
    Task<StudentBasicInfo?> FindStudentByEmailAsync(string email);
}