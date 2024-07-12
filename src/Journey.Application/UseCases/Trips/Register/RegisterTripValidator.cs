using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessage.NameEmpty);
            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessage.StartDateBiggerThenActualDate);
            RuleFor(request => request)
                .Must(request => request.EndDate.Date > request.StartDate.Date)
                .WithMessage(ResourceErrorMessage.StartDateBiggerThenEndDate);
        }
    }
}
