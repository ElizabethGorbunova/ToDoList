using ToDoWebApp.ModelsDto;

namespace ToDoWebApp.Services
{
    public interface IAccountService
    {
        public abstract void RegisterUser(UserDto user);
        public abstract string GenerateJwt(LoginDto loginDto);
    }
}