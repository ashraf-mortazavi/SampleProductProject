using FluentValidation;

namespace SampleProductProject.Commands
{
    public class ImportUserProductValidator : AbstractValidator<ImportUserProductCommand>
    {
        public ImportUserProductValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(f => f.Length > 0).WithMessage("File cannot be empty.");
        }
    }
}
