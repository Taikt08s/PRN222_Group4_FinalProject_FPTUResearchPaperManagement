using BusinessObject.Enums;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Service;

public class DashboardService : IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly ISubmissionRepository _submissionRepository;

    public DashboardService(IUserRepository userRepository, ISubmissionRepository submissionRepository)
    {
        _userRepository = userRepository;
        _submissionRepository = submissionRepository;
    }

    public async Task<DashboardStatisticsDto> GetStatisticsAsync()
    {
        var studentRole = AccountRole.Student.ToString();
        var totalStudents = await _userRepository.CountByRoleAsync(studentRole);

        var submittedStatus = SubmissionStatus.Submitted.ToString();
        var approvedStatus = SubmissionStatus.Aprroved.ToString();
        var rejectedStatus = SubmissionStatus.Rejected.ToString();

        var totalSubmitted = await _submissionRepository.CountByStatusAsync(submittedStatus);
        var totalApproved = await _submissionRepository.CountByStatusAsync(approvedStatus);
        var totalRejected = await _submissionRepository.CountByStatusAsync(rejectedStatus);
        var totalPlagiarism = await _submissionRepository.CountPlagiarismFlaggedAsync();

        var allSubmissions = await _submissionRepository.GetAllAsync();

        var semesterStats = allSubmissions
            .Where(s => s.Semester != null)
            .GroupBy(s => new { s.Semester_Id, s.Semester.Term, s.Semester.Year })
            .Select(g => new SemesterStatisticsDto
            {
                SemesterId = g.Key.Semester_Id,
                SemesterName = $"{g.Key.Term} {g.Key.Year}",
                Students = 0, // Would need group member data to calculate
                Submitted = g.Count(s => s.Status == submittedStatus),
                Approved = g.Count(s => s.Status == approvedStatus),
                Plagiarism = g.Count(s => s.Plagiarism_Flag),
                Rejected = g.Count(s => s.Status == rejectedStatus)
            })
            .OrderByDescending(s => s.SemesterId)
            .ToList();

        return new DashboardStatisticsDto
        {
            TotalStudents = totalStudents,
            TotalSubmitted = totalSubmitted,
            TotalApproved = totalApproved,
            TotalPlagiarism = totalPlagiarism,
            TotalRejected = totalRejected,
            SemesterStats = semesterStats
        };
    }
}
