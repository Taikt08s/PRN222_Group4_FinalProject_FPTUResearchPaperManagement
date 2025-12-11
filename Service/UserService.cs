using AutoMapper;
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
}