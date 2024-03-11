using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;
using ToDoWebApp.Services;

namespace ToDoWebApp.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;
        private readonly IGroupService _groupService;

        public GroupController(TaskDbContext dbContext, IGroupService groupService)
        {
            _dbContext = dbContext;
            _groupService = groupService;
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllGroups()
        {
            var groups = _groupService.GetAllGroups();
            if (groups == null)
            {
                return NoContent();
            }

            return Ok(groups);

        }

        [HttpPost("{groupName}")]
        public ActionResult<GroupDtoOut> CreateNewGroup([FromRoute] string groupName)
        {

            var newGroup = _groupService.CreateNewGroup(groupName);
            return Ok(newGroup);
        }

        [HttpPut("{groupId}")]
        public ActionResult<GroupDtoOut> EditGroup([FromBody] GroupDtoIn groupDto, [FromRoute] int groupId)
        {
            var groupExist = _dbContext.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if(groupExist == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var changedGroup = _groupService.EditGroup(groupDto, groupId);
            return Ok(changedGroup);
        }

        [HttpDelete("{groupId}")]
        public ActionResult DeleteGroup([FromRoute] int groupId)
        {
            var group = _dbContext.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if(group==null)
            {
                return BadRequest();
            }

            _groupService.DeleteGroup(groupId);

            return Ok($"Group{groupId} deleted");
        }
            
    }
}
