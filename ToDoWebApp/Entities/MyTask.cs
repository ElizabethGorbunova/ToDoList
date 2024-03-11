using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ToDoWebApp.Enums;
using ToDoWebApp.Entities;


namespace ToDoWebApp.Entities
{
    public class MyTask
    {
        public int MyTaskId { get; set; }

       
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Category? Category { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public virtual Group? Group { get; set; }
    }
}
