using BusinessObject.Models;

namespace Service.Interfaces;

public interface ISubmissionFileService
{
    Task<SubmissionFile?> GetByIdAsync(int id);
}
