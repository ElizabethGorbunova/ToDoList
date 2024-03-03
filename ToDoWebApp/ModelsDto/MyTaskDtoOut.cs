using ToDoWebApp.Enums;

namespace ToDoWebApp.ModelsDto
{
    public class MyTaskDtoOut
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
        public int? UserId { get; set; }

    }
}
