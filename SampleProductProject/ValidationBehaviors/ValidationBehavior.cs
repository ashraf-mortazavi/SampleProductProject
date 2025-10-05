using FluentValidation;
using MediatR;
using SampleProductProject.ViewModels;

namespace SampleProductProject.ValidationBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
      
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var messages = failures.Select(f => f.ErrorMessage).ToList();

                object response = (object)(
                    new UserProductsResult(false, "ValidationFailed", messages, new List<string>())
                );

                return (TResponse)response;
            }

            return await next();
        }
    }
}
