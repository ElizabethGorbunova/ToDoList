using ToDoWebApp.Enums;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;
using System.Security.Claims;

namespace ToDoWebApp.Services
{
    public interface ITaskService
    {
        public abstract IEnumerable<MyTaskDtoOut> GetAllTasks();
        public abstract MyTaskDtoOut GetTaskById(int id);
        public abstract MyTaskDtoOut AddNewTask(MyTaskDtoIn task, int userId);
        public abstract EditTaskResult<MyTaskDtoOut> EditTask(MyTaskDtoIn task, int id);
        void DeleteTask(int id);
        
        
        
    }
}