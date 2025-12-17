using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISubmissionFileRepository
    {
        Task<List<SubmissionFile>> GetBySubmissionIdAsync(int submissionId);
        Task AddAsync(SubmissionFile file);
        Task RemoveAsync(SubmissionFile file);
        Task<SubmissionFile?> GetByIdAsync(int id);
    }
}
