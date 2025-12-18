namespace Service.Dtos;

public class ThesisAiResult
{
    public bool IsApproved { get; set; }

    // Plagiarism
    public bool PlagiarismPass { get; set; }
    public float PlagiarismScore { get; set; }

    // Academic evaluation criteria
    public bool OriginalityPass { get; set; }
    public bool CitationQualityPass { get; set; }
    public bool AcademicRigorPass { get; set; }
    public bool ResearchRelevancePass { get; set; }
    public bool WritingQualityPass { get; set; }
    public bool EthicalCompliancePass { get; set; }
    public bool CompletenessPass { get; set; }

    // Explanation & details
    public string Reasoning { get; set; } = string.Empty;
    public string ViolationsJson { get; set; } = string.Empty;
}