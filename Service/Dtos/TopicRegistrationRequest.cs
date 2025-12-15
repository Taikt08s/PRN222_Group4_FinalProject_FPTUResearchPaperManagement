namespace Service.Dtos
{
    public class TopicRegistrationRequest
    {
        public int TopicId { get; set; }
        public Guid StudentId { get; set; }
        public List<Guid> MemberIds { get; set; } = new();
        public int? InstructorId { get; set; }
    }
}
