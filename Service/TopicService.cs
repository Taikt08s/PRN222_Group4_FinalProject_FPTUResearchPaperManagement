using System;
using AutoMapper;
using BusinessObject.Enums;
using BusinessObject.Filters;
using BusinessObject.Models;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;
using Service.Utils;


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

        public async Task<int> CountAsync(TopicFilter? filter)
        {
            return await _repo.CountAsync(filter);
        }

        public async Task<List<TopicResponseModel>> GetPaginationAsync(TopicFilter? filter, int page, int size)
        {
            var list = await _repo.GetPaginationAsync(filter, page, size);
            var listDto = _mapper.Map<List<TopicResponseModel>>(list);
            for (int index = 0; index < list.Count; ++index)
            {
                listDto[index].Members = _mapper.Map<List<StudentBasicInfo>>(list[index].Groups.SelectMany(group => group.Members).ToList());
            }
            return listDto;
        }

        public async Task UpdateTopicStatus(int id, string status)
        {
            Topic? topic = await _repo.GetTopicByIdAsync(id);
            if (topic == null)
            {
                throw new Exception("Cannot find topic with id: " + id);
            }
            EnumValidator.EnsureValidEnum(status, typeof(TopicStatus));
            topic.Status = status;
            await _repo.UpdateAsync(topic);
        }

        public async Task<Topic?> GetTopicByGroupAsync(int groupId)
        {
            return await _repo.GetTopicByGroupAsync(groupId);
        }

        public async Task<StudentGroup?> GetGroupByTopicAsync(int topic)
        {
            return await _repo.GetGroupByTopicAsync(topic);
        }

        public async Task<TopicResponseModel> CreateTopicAsync(CreateTopicRequest req)
        {
            ArgumentNullException.ThrowIfNull(req);

            if (string.IsNullOrWhiteSpace(req.TopicName))
                throw new ArgumentException("Topic name is required.", nameof(req.TopicName));
            if (req.SemesterId <= 0)
                throw new ArgumentException("SemesterId must be provided.", nameof(req.SemesterId));
            if (req.InstructorId == Guid.Empty)
                throw new ArgumentException("InstructorId is required.", nameof(req.InstructorId));
            if (req.DeadlineDate == default)
                throw new ArgumentException("DeadlineDate must be provided.", nameof(req.DeadlineDate));

            var topic = new Topic
            {
                Title = req.TopicName.Trim(),
                Description = req.TopicDescription,
                SubmissionInstruction = req.SubmissionInstruction ?? string.Empty,
                Semester_Id = req.SemesterId,
                Created_By = req.InstructorId,
                Is_Group_Required = req.IsGroupTopic,
                Deadline_Date = req.DeadlineDate,
                Status = req.Status.ToString(),
                Major = GetMajorName(req.Major),
            };

            var createdTopic = await _repo.CreateTopicAsync(topic);
            return _mapper.Map<TopicResponseModel>(createdTopic);
        }
        
        public async Task<TopicResponseModel> UpdateTopicAsync(UpdateTopicRequest req)
        {
            var topic = await _repo.GetTopicByIdAsync(req.TopicId);
            if (topic == null)
                throw new Exception("Topic not found with id: " + req.TopicId);

            ArgumentNullException.ThrowIfNull(req);

            if (string.IsNullOrWhiteSpace(req.TopicName))
                throw new ArgumentException("Topic name is required.", nameof(req.TopicName));
            if (req.SemesterId <= 0)
                throw new ArgumentException("SemesterId must be provided.", nameof(req.SemesterId));
            if (req.InstructorId == Guid.Empty)
                throw new ArgumentException("InstructorId must be provided.", nameof(req.InstructorId));
            if (req.DeadlineDate == default)
                throw new ArgumentException("DeadlineDate must be provided.", nameof(req.DeadlineDate));

            topic.Title = req.TopicName.Trim();
            topic.Description = req.TopicDescription;
            topic.SubmissionInstruction = req.SubmissionInstruction ?? string.Empty;
            topic.Semester_Id = req.SemesterId;
            topic.Created_By = req.InstructorId;
            topic.Is_Group_Required = req.IsGroupTopic;
            topic.Deadline_Date = req.DeadlineDate;
            topic.Status = req.Status.ToString();
            topic.Major = GetMajorName(req.Major);         

            var updatedTopic = await _repo.UpdateTopicAsync(topic);
            return _mapper.Map<TopicResponseModel>(updatedTopic);
        }

        public static string GetMajorName(AccountMajor major)
    {
        return major switch
        {
            AccountMajor.ComputerScience => "Khoa Học Máy Tính",
            AccountMajor.InformationTechnology => "Công Nghệ Thông Tin",
            AccountMajor.SoftwareEngineering => "Kỹ Thuật Phần Mềm",
            AccountMajor.DataScience => "Khoa Học Dữ Liệu",
            AccountMajor.Teacher => "Sư Phạm",
            AccountMajor.Finance => "Tài Chính",
            AccountMajor.BusinessAdministration => "Quản Trị Kinh Doanh",
            AccountMajor.Marketing => "Marketing",
            AccountMajor.Other => "Khác",
            AccountMajor.English => "Ngôn Ngữ Anh",
            _ => "Không xác định"
        };
    }
    }

}
