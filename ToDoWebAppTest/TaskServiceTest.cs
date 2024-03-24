using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;
using ToDoWebApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoWebApp.Enums;
using ToDoWebApp;
using Microsoft.Extensions.Configuration;
using ToDoWebApp.Controllers;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace ToDoWebAppTest
{
    
    public class TaskServiceTest
    {
        [Fact]
        public void GetTaskByIdTest()
        {
            //Arrange 
            var taskId = 27;
            var resultTestedObj = new MyTaskDtoOut()
            {
                Category = 0,
                GroupName = "Studying",
                Name = "Travel",
                Status = 0,
                UserId = 12
            };


          //Act

            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(x => x.GetTaskById(taskId)).Returns(resultTestedObj);

            var taskMapperMock = new Mock<IMapper>();

            var taskDbContextMock = new Mock<TaskDbContext>();
            

            var controller = new TaskController(taskDbContextMock.Object, taskServiceMock.Object, taskMapperMock.Object);

            var result = controller.GetTaskById(taskId);


            //Assert 
             Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void GetAllTasksTest()
        {
            var listTaskTest = new List<MyTaskDtoOut>()
            {
                new MyTaskDtoOut()
                {
                    Category = 0,
                    GroupName = "Household chores",
                    Name = "Clean bathroom",
                    Status = 0,
                    UserId = 11
                },
                new MyTaskDtoOut()
                {
                    Category = 0,
                    GroupName = "Household chores",
                    Name = "Water flowers",
                    Status = 0,
                    UserId = 10
                },
            };

            var taskDbContextMock = new Mock<TaskDbContext>();
            var taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(x => x.GetAllTasks()).Returns(listTaskTest);
            var mapperMock = new Mock<IMapper>();



            var taskController = new TaskController(taskDbContextMock.Object, taskServiceMock.Object,mapperMock.Object);
            var result = taskController.GetAllTasks();

            Assert.IsType<OkObjectResult>(result.Result);   //think about how can I compare ActionResult<IEnumerable<MyTaskDtoOut>>

        }
    }
}