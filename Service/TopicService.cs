using AutoMapper;
using BusinessObject.Models;
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

        public async Task<(bool Success, string Error)> TopicRegistrationAsync(TopicRegistrationRequest req)
        {
            var topic = await _repo.GetTopicByIdAsync(req.TopicId);
            if (topic == null) return (false, "Không tìm thấy đề tài.");

            // Create group
            var group = new StudentGroup
            {
                Topic_Id = req.TopicId,
                Created_At = DateTime.UtcNow,
                Members = new List<StudentGroupMember>(),
                Created_By = req.StudentId
            };

            // Leader (current user)
            group.Members.Add(new StudentGroupMember
            {
                Student_Id = req.StudentId,
                Is_Leader = true
            });

            if (topic.Is_Group_Required)
            {
                if (req.MemberIds.Count < 3 || req.MemberIds.Count > 4)
                    return (false, "Nhóm phải có tối thiểu 4 thành viên (bao gồm nhóm trưởng).");

                if (req.MemberIds.Count > 4)
                    return (false, "Nhóm tối đa 5 thành viên.");

                foreach (var studentId in req.MemberIds)
                {
                    group.Members.Add(new StudentGroupMember
                    {
                        Student_Id = studentId,
                        Is_Leader = false
                    });
                }

                topic.Status = "PendingAssignedGroup";  
            }
            else
            {
                topic.Status = "PendingAssignedSingle";         
            }

            await _repo.CreateGroupAsync(group);
            await _repo.UpdateAsync(topic);

            return (true, null);
        }

        public Task<bool> StudentHasTopicAsync(Guid studentId)
        {
            return _repo.StudentHasTopicAsync(studentId);
        }

        public async Task<TopicResponseModel?> GetRegisteredTopicForStudentAsync(Guid studentId)
        {
            var groupMember = await _repo.GetStudentGroupMemberAsync(studentId);
            if (groupMember == null) return null;

            var topic = await _repo.GetTopicByIdAsync(groupMember.Group.Topic_Id);
            if (topic == null) return null;

            var topicDto = _mapper.Map<TopicResponseModel>(topic);

            // Map members
            topicDto.Members = groupMember.Group.Members
                .Select(m => new StudentBasicInfo
                {
                    Id = m.Student_Id,
                    Full_Name = m.Student.Full_Name,
                    IsLeader = m.Is_Leader
                }).ToList();

            topicDto.GroupId = groupMember.Group.Id;
            topicDto.SemesterId = topic.Semester_Id;

            return topicDto;
        }
    }
}