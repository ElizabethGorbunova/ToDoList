using FluentValidation;
using ToDoWebApp.Entities;
using ToDoWebApp.Enums;

namespace ToDoWebApp.ModelsDto.Validators
{
    public class MyTaskDtoInValidator:AbstractValidator<MyTaskDtoIn>
    {
        private readonly TaskDbContext _dbContext;
        public MyTaskDtoInValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(t => t.Category).Custom((value, context) =>
             {
                 var allCategories = Enum.GetNames(typeof(Category)).ToArray();  

                 if (!allCategories.Contains(value.ToString()))
                 {
                     context.AddFailure($"There isn't such Category on a list. Available Categories: {String.Join(",", allCategories)}");
                 }
             });

            RuleFor(t => t.Status).Custom((value, context) =>
             {
                 var allStatus = Enum.GetNames(typeof(Status)).ToArray();

                 if (!allStatus.Contains(value.ToString()))
                 {
                     context.AddFailure($"There isn't such Status on a list. Available Status: {String.Join(",", allStatus)}");
                 }
             });

            RuleFor(t => t.GroupName).Custom((value, context) =>
            {
                var groupExistInDB = _dbContext.Groups.FirstOrDefault(g => g.GroupName == value);
                if (groupExistInDB is null)
                {
                    context.AddFailure($"There is no such group as {value}.");
                }
            });

        }
    }
}
