using System;
using BusinessObject.Enums;

namespace Service.Dtos
{
    public class CreateTopicRequest
    {
        public string TopicName { get; set; } = string.Empty;
        public string TopicDescription { get; set; } = string.Empty;
        public string SubmissionInstruction { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public AccountMajor Major { get; set; } = AccountMajor.SoftwareEngineering;
        public int SemesterId { get; set; }
        public bool IsGroupTopic { get; set; }
        public DateTime DeadlineDate { get; set; }
        public TopicStatus Status { get; set; } = TopicStatus.Created;
        public int? GraduationGroupId { get; set; }
    }
}