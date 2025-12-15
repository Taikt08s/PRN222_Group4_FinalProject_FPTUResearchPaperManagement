using AutoMapper;
using BusinessObject.Models;
using Service.Dtos;

namespace DataAccessLayer.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StudentGroupMember, StudentBasicInfo>()
            .ForMember(d => d.IsLeader, o => o.MapFrom(s => s.Is_Leader))
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Student_Id))
            .ForMember(d => d.Full_Name, o => o.MapFrom(s => s.Student.Full_Name))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Student.Email))
            .ForMember(d => d.Major, o => o.MapFrom(s => s.Student.Major));
        CreateMap<Topic, TopicResponseModel>();
        CreateMap<TopicRegistrationRequest, StudentGroup>();
        CreateMap<User, StudentBasicInfo>();
        CreateMap<User, UserAdminDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.Full_Name))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.Created_At))
            .ForMember(d => d.IsActive, o => o.MapFrom(s => s.Is_Active))
            .ForMember(d => d.IsSuspended, o => o.MapFrom(s => s.Is_Suspended))
            .ForMember(d => d.SuspendedUntil, o => o.MapFrom(s => s.Suspended_Until));
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
