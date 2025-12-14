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
        CreateMap<SubmissionFile, SubmissionFileDto>();
        CreateMap<Submission, SubmissionDto>()
        .ForMember(d => d.GroupId, o => o.MapFrom(s => s.Group_Id))
        .ForMember(d => d.TopicId, o => o.MapFrom(s => s.Topic_Id))
        .ForMember(d => d.SemesterId, o => o.MapFrom(s => s.Semester_Id))
        .ForMember(d => d.SubmittedAt, o => o.MapFrom(s => s.Submitted_At))
        .ForMember(d => d.ReviewedAt, o => o.MapFrom(s => s.Reviewed_At))
        .ForMember(d => d.PlagiarismScore, o => o.MapFrom(s => s.Plagiarism_Score))
        .ForMember(d => d.PlagiarismFlag, o => o.MapFrom(s => s.Plagiarism_Flag))
        .ForMember(d => d.RejectReason, o => o.MapFrom(s => s.Reject_Reason));
    }
}