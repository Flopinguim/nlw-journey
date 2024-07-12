using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id)
        {

            var dbContext = new JourneyDbContext();

            var trip = dbContext
                .Trips
                .Include(i => i.Activities)
                .FirstOrDefault(i => i.Id.Equals(id));

            if (trip is null)
                throw new NotFoundException(ResourceErrorMessage.TripNotFound);

            return new ResponseTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(i => new ResponseActivityJson
                {
                    Id = i.Id,
                    Name = i.Name,
                    Date = i.Date,
                    Status = (Communication.Enums.ActivityStatus)i.Status,
                }).ToList(),
            };
        }
    }
}
