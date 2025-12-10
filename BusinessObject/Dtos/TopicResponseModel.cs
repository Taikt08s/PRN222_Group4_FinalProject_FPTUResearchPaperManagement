namespace BusinessObject.Dtos
{
    public class TopicResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Major { get; set; }
        public DateTime Deadline_Date { get; set; }
        public bool Is_Group_Required { get; set; }
    }
}
