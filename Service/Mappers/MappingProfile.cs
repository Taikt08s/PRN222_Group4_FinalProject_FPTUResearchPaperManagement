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
        CreateMap<ThesisModeration, ThesisAiResult>()
            .ForMember(d => d.IsApproved, o => o.MapFrom(s => s.Is_Approved))
            .ForMember(d => d.PlagiarismPass, o => o.MapFrom(s => s.Plagiarism_Pass))
            .ForMember(d => d.PlagiarismScore, o => o.MapFrom(s => s.Plagiarism_Score))
            .ForMember(d => d.OriginalityPass, o => o.MapFrom(s => s.Originality_Pass))
            .ForMember(d => d.CitationQualityPass, o => o.MapFrom(s => s.Citation_Quality_Pass))
            .ForMember(d => d.AcademicRigorPass, o => o.MapFrom(s => s.Academic_Rigor_Pass))
            .ForMember(d => d.ResearchRelevancePass, o => o.MapFrom(s => s.Research_Relevance_Pass))
            .ForMember(d => d.WritingQualityPass, o => o.MapFrom(s => s.Writing_Quality_Pass))
            .ForMember(d => d.EthicalCompliancePass, o => o.MapFrom(s => s.Ethical_Compliance_Pass))
            .ForMember(d => d.CompletenessPass, o => o.MapFrom(s => s.Completeness_Pass))
            .ForMember(d => d.Reasoning, o => o.MapFrom(s => s.Reasoning))
            .ForMember(d => d.ViolationsJson, o => o.MapFrom(s => s.Violations_Json));
    }
}
