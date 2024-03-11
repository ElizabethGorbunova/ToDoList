using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;

namespace ToDoWebApp.Services

{
    public interface IGroupService
    {
        public abstract List<string> GetAllGroups();
        public abstract GroupDtoOut CreateNewGroup(string groupName);
        public abstract GroupDtoOut EditGroup(GroupDtoIn groupDto, int groupId);
        public abstract void DeleteGroup(int groupId);
    }
}
