namespace BusinessObject.Dtos
{
    public class TopicResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Major { get; set; }
        
        public string Description { get; set; }
        
        public string SubmissionInstruction{ get; set; }
        
        public string SemesterTerm { get; set; }
        public DateTime Deadline_Date { get; set; }
        public bool Is_Group_Required { get; set; }
    }
}
