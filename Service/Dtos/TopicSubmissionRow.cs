namespace Service.Dtos;

public class TopicSubmissionRow
{
    public int TopicId { get; set; }
    public string TopicTitle { get; set; }
    public string Major { get; set; }
    public string Semester { get; set; }

    public int? GroupId { get; set; }
    public string? LeaderName { get; set; }
    public int MemberCount { get; set; }

    public int? SubmissionId { get; set; }
    public string SubmissionStatus { get; set; } = "Not submitted";
    public bool HasReviewedByMe { get; set; }
}
