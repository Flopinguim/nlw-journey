using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbContext = new JourneyDbContext();

            var trips = dbContext.Trips.ToList();

            return new ResponseTripsJson
            {
                Trips = trips.Select( i => new ResponseShortTripJson
                {
                    Id = i.Id,
                    EndDate = i.EndDate,
                    Name = i.Name,
                    StartDate = i.StartDate
                }).ToList()
            };
        }
    }
}
