using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITopicService
    {
        Task<List<TopicResponseModel>> GetTopicsForStudentAsync(string major);
    }
}
