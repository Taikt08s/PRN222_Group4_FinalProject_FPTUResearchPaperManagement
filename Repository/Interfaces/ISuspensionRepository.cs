using BusinessObject.Models;

namespace Repository.Interfaces
{
    public interface ISuspensionRepository
    {
        Task<bool> IsStudentSuspendedAsync(Guid studentId);

        Task AddSuspensionAsync(SuspensionRecord record);
    }
}
