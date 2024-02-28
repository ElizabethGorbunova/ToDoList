namespace ToDoWebApp
{
    public class EditTaskResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Model { get; set; }
    }
}
