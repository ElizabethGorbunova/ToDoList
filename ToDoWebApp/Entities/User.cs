using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApp.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }  
        public virtual List<MyTask>? MyTasks { get; set; }
        
    }

  
}
