namespace Service.Dtos;

public class ThesisAiResult
{
    public bool IsApproved { get; set; }
    public float PlagiarismScore { get; set; }
    public bool PlagiarismPass { get; set; }
    public string Reasoning { get; set; } = "";
    public string ViolationsJson { get; set; } = "";
}