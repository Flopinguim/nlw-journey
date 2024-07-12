using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id)
        {

            var dbContext = new JourneyDbContext();

            var trip = dbContext
                .Trips
                .Include(i => i.Activities)
                .FirstOrDefault(i => i.Id.Equals(id));

            if (trip is null)
                throw new NotFoundException(ResourceErrorMessage.TripNotFound);

            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();
        }
    }
}
