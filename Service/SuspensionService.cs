using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class SuspensionService : ISuspensionService
    {
        private readonly ISuspensionRepository _suspensionRepo;
        private readonly IStudentGroupRepository _groupRepo;
        private readonly ISemesterRepository _semesterRepo;

        public SuspensionService(
            ISuspensionRepository suspensionRepo,
            IStudentGroupRepository groupRepo,
            ISemesterRepository semesterRepo)
        {
            _suspensionRepo = suspensionRepo;
            _groupRepo = groupRepo;
            _semesterRepo = semesterRepo;
        }

        public async Task SuspendGroupAsync(Submission submission)
        {
            // ✅ Guard condition
            if (submission.Status != SubmissionStatus.Rejected.ToString()
                || submission.Plagiarism_Flag == false)
                return;

            var semester = await _semesterRepo.GetByIdAsync(submission.Semester_Id);
            var studentIds = await _groupRepo.GetStudentIdsByGroupAsync(submission.Group_Id);

            foreach (var studentId in studentIds)
            {
                // Prevent duplicate suspension
                if (await _suspensionRepo.IsStudentSuspendedAsync(studentId))
                    continue;

                var record = new SuspensionRecord
                {
                    Student_Id = studentId,
                    Submission_Id = submission.Id,
                    Detected_By_Ai = true,
                    Approved_By_Gpec = false,
                    Approved_By_Sdc = false,
                    Start_Date = semester.Start_Date,
                    End_Date = semester.End_Date
                };

                await _suspensionRepo.AddSuspensionAsync(record);
            }

            // Optional: mark submission as Suspended
            submission.Status = SubmissionStatus.Suspended.ToString();
        }
    }
}
