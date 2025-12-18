using System;
using BusinessObject.Enums;

namespace Service.Dtos;

public class UpdateTopicRequest
{
    public int TopicId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string TopicDescription { get; set; } = string.Empty;
    public string SubmissionInstruction { get; set; } = string.Empty;
    public int SemesterId { get; set; }
    public Guid InstructorId { get; set; }
    public Guid CreatedBy { get; set; }
    public bool IsGroupTopic { get; set; }
    public DateTime DeadlineDate { get; set; }
    public TopicStatus Status { get; set; } = TopicStatus.Created;
    public AccountMajor Major { get; set; } = AccountMajor.SoftwareEngineering;
}