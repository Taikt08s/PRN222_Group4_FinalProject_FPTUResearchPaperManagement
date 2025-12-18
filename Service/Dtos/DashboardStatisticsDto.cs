namespace Service.Dtos;

public class DashboardStatisticsDto
{
    public int TotalStudents { get; set; }
    public int TotalSubmitted { get; set; }
    public int TotalApproved { get; set; }
    public int TotalPlagiarism { get; set; }
    public int TotalRejected { get; set; }
    public List<SemesterStatisticsDto> SemesterStats { get; set; } = new();
}

public class SemesterStatisticsDto
{
    public int SemesterId { get; set; }
    public string SemesterName { get; set; } = string.Empty;
    public int Students { get; set; }
    public int Submitted { get; set; }
    public int Approved { get; set; }
    public int Plagiarism { get; set; }
    public int Rejected { get; set; }
}
