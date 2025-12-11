namespace BusinessObject.Models
{
    public class StudentGroup
    {
        public int Id { get; set; }
        public int Topic_Id { get; set; }
        public Guid Created_By { get; set; }
        public DateTime Created_At { get; set; }

        public Topic Topic { get; set; }
        public User Creator { get; set; }

        public ICollection<StudentGroupMember> Members { get; set; }
    }
}
