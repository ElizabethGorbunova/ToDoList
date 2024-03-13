using System.Linq.Expressions;
using ToDoWebApp.Entities;
using ToDoWebApp.Enums;


namespace ToDoWebApp
{
    public class TaskSeeder
    {
        public TaskDbContext _dbContext;
        public TaskSeeder(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Tasks.Any())
                {
                    var tasks = GetTasks();
                    _dbContext.Tasks.AddRange(tasks);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

            }
        }

        private List<MyTask> GetTasks()
        {
            var tasks = new List<MyTask>()

            {
                new MyTask()
              {
                Name = "Cleaning",
                Date = new DateTime(2024, 01, 22),
                Status = Status.Planned,
                Category = Category.Home,
                GroupId=4
      
              },
               new MyTask()
              {
                Name = "Studying .NET",
                Date = new DateTime(2024, 01, 21),
                Status = Status.Planned,
                Category = Category.Studying,
                GroupId=4
             },
            };

            return tasks;

        }

        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name="Liza",
                    LastName="Gorbunova",
                    DateOfBirth=new DateTime(1999, 09, 25),
                    Email="egorbunova016@gmail.com",
                    /*MyTasks=GetTasks(),*/
                    /*MyTaskId=1,*/
                },
                new User()
                {
                    Name = "Anton",
                    LastName = "Trafimovich",
                    DateOfBirth = new DateTime(1996, 01, 05),
                    Email = "tonytrof@gmail.com",
                   /* MyTasks=GetTasks(),*/
                    /*MyTaskId=2,*/

                }
            };
            return users;
        }
        private List<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Administrator"
                }
            };
            return roles;
        }
    }
}
