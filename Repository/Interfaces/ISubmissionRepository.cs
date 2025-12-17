using BusinessObject.Filters;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISubmissionRepository
    {
        Task<Submission?> GetByIdAsync(int id);

        Task<Submission?> GetByTopicGroupSemesterAsync(
            int topicId,
            int groupId,
            int semesterId);

        Task AddAsync(Submission submission);

        Task UpdateAsync(Submission submission);

        Task<int> CountFilteredAsync(SubmissionFilter? filter);
        Task<List<Submission>> GetPaginationAsync(SubmissionFilter? filter, int page, int size);
    }
}
