using AutoMapper;
using BusinessObject.Dtos;
using BusinessObject.Models;
namespace DataAccessLayer.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Topic, TopicResponseModel>();
    }
}