using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;
using ToDoWebApp.Services;

namespace ToDoWebApp.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TaskController: ControllerBase
    {
        public readonly TaskDbContext _dbContext;
        public readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        public TaskController(TaskDbContext dbContext, ITaskService taskService, IMapper mapper)
        {
            _dbContext = dbContext;
            _taskService = taskService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<MyTaskDtoOut>> GetAllTasks()
        {
            var allTasks = _taskService.GetAllTasks();
            if(allTasks == null)
            {
                return NotFound();
            }
            return Ok(allTasks);
        }

        [HttpGet("{id}")]
        public ActionResult<MyTaskDtoOut> GetTaskById([FromRoute] int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles="Admin,Manager,User")]
        [Authorize(Policy ="HasDateOfBirth")]
        public ActionResult<MyTaskDtoOut> AddNewTask([FromBody] MyTaskDtoIn task)
        {
            var userId = int.Parse(this.User.Claims.First(c=>c.Type==ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTask = _taskService.AddNewTask(task, userId);
            return Ok(newTask);
        }

        [HttpPut("{id}")]
        public ActionResult EditTask([FromBody] MyTaskDtoIn task, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _taskService.EditTask(task, id);
            if (result.IsSuccess==true)
            {
                return Ok(result.Model);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            _taskService.DeleteTask(id);
            return Ok();
        }

    }
}
