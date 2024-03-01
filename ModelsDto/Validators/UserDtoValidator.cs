using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApp.Entities;

namespace ToDoWebApp.ModelsDto.Validators
{
    public class UserDtoValidator:AbstractValidator<UserDto>
    {
        private readonly TaskDbContext dbContext;
        public UserDtoValidator(TaskDbContext _dbContext)
        {
            dbContext = _dbContext;

            RuleFor(u => u.Name)
                .MaximumLength(30)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .MaximumLength(30)
                .NotEmpty();

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password);

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var checkEmailResult = dbContext.Users.Any(u => u.Email == value);
                    if (checkEmailResult)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                    
                });

            RuleFor(u => u.RoleId).Custom((value, context) =>
            {
                var roles = new List<int>();
                var rolesDb = _dbContext.Roles;
                foreach(var role in rolesDb)
                {
                    roles.Add(role.RoleId);
                }
                var rolesName = new List<string>();
                foreach (var role in rolesDb)
                {
                    rolesName.Add(role.Name);
                }
                var rolesNameArr = rolesName.ToArray();

                if (!roles.Contains(value))
                {
                    context.AddFailure($"There isn't such Role. Availables Roles are: {String.Join(",", rolesNameArr)}");
                }
            });
        }
    }
}
