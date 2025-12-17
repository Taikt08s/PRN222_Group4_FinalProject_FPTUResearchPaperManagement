using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class SubmissionFileService : ISubmissionFileService
    {
        private readonly ISubmissionFileRepository _repo;

        public SubmissionFileService(ISubmissionFileRepository repo)
        {
            _repo = repo;
        }

        public async Task<SubmissionFile?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

    }
}

