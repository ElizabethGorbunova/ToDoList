using AutoMapper;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;

namespace ToDoWebApp.Services
{
    public class GroupService : IGroupService
    {
        private readonly TaskDbContext _dbContext;
        private readonly IMapper _mapper;
        public GroupService(TaskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<string> GetAllGroups()
        {
            var groups = new List<string>();
            var allGroupsFromDB = _dbContext.Groups.ToList();
            foreach(var group in allGroupsFromDB)
            {
                groups.Add(group.GroupName);
            }

            return groups;
        }

        public GroupDtoOut CreateNewGroup(string groupName)
        {
            var newGroup = new Group();
            newGroup.GroupName = groupName;
            _dbContext.Groups.Add(newGroup);
            _dbContext.SaveChanges();

            var newGroupMapped = _mapper.Map<GroupDtoOut>(newGroup);
            return newGroupMapped;
        }

        public GroupDtoOut EditGroup(GroupDtoIn groupDto, int groupId)
        {
            var groupToChange = _dbContext.Groups.FirstOrDefault(g => g.GroupId == groupId);
            groupToChange.GroupId = groupDto.GroupId;
            _dbContext.SaveChanges();

            var groupToChangeMapped = _mapper.Map<GroupDtoOut>(groupToChange);
            return groupToChangeMapped;
            
        }

        public void DeleteGroup(int groupId)
        {
            var tasksFromGroupToDelete = _dbContext.Tasks.Where(t=>t.GroupId==groupId).ToList();
            foreach(var task in tasksFromGroupToDelete)
            {
                _dbContext.Tasks.Remove(task);
            }

            var group = _dbContext.Groups.FirstOrDefault(g => g.GroupId == groupId);
            _dbContext.Groups.Remove(group);
            _dbContext.SaveChanges();

        }
    }
}
