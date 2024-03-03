using FluentValidation;
using ToDoWebApp.Enums;

namespace ToDoWebApp.ModelsDto.Validators
{
    public class MyTaskDtoInValidator:AbstractValidator<MyTaskDtoIn>
    {
        public MyTaskDtoInValidator()
        {
            RuleFor(t => t.Category).Custom((value, context) =>
             {
                 var allCategories = Enum.GetNames(typeof(Category)).ToArray();  //ToLower?????????????????????????????????????????????? +Role Validation

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

        }
    }
}
