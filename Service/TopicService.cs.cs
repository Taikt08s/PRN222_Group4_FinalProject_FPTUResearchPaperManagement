using AutoMapper;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;


namespace Service
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repo;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TopicResponseModel>> GetTopicsForStudentAsync(string major)
        {
            var topics = await _repo.GetTopicsByMajorAsync(major);

            return _mapper.Map<List<TopicResponseModel>>(topics);
        }

        public async Task<TopicResponseModel?> GetTopicByIdAsync(int id)
        {
            var topic = await _repo.GetTopicByIdAsync(id);
            if (topic == null) return null;

            return _mapper.Map<TopicResponseModel>(topic);
        }
    }
}