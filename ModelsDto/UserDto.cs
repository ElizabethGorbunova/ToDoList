using System.ComponentModel.DataAnnotations;
using ToDoWebApp.Entities;

namespace ToDoWebApp.ModelsDto
{
    public class UserDto
    {
        public int UserId { get; set; }

        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 0;

    }
}
