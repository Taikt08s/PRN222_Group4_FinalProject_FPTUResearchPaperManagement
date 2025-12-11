using AutoMapper;
using BusinessObject.Models;
using Service.Dtos;

namespace DataAccessLayer.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Topic, TopicResponseModel>();
        CreateMap<TopicRegistrationRequest, StudentGroup>();
        CreateMap<User, StudentBasicInfo>();

    }
}