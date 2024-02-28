using AutoMapper;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;

namespace ToDoWebApp
{
    public class MyTaskMappingProfile:Profile
    {
        public MyTaskMappingProfile()
        {
            CreateMap<MyTask, MyTaskDtoOut>()
                .ForMember(t => t.UserId, n => n.MapFrom(p => p.UserId));
               

            CreateMap<User, UserDto>();

            CreateMap<MyTaskDtoIn, MyTaskDtoOut>();

            CreateMap<MyTaskDtoIn, MyTask>();
            CreateMap<MyTask, MyTaskDtoOut>();

            /*CreateMap<UserDto, User>();*/

        }
    }
}
