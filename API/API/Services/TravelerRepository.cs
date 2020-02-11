using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TravelerRepository : ITravelerRepository
    {
        private readonly TravelerPhotoAPIContext _eventContext;
        private readonly ILogger<ITravelerRepository> _logger;

        public TravelerRepository(TravelerPhotoAPIContext eventContext, ILogger<ITravelerRepository> logger)
        {
            _eventContext = eventContext;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _eventContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _eventContext.Remove(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _eventContext.SaveChangesAsync()) >= 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _eventContext.Update(entity);
        }


        #region Trip
        public async Task<Trips[]> GetTrips()
        {
            _logger.LogInformation($"Getting all trips");
            var query = _eventContext.Trips
                        .OrderBy(c => c.CreationDate);

            return await query.ToArrayAsync();
        }

        public async Task<Trips> GetTrip(int tripId)
        {
            _logger.LogInformation($"Getting trip for id {tripId}");

            var query = _eventContext.Trips
                        .Where(c => c.Id == tripId);

            return await query.FirstOrDefaultAsync();
        }
        #endregion





        #region Photos
        public async Task<Photos[]> GetPhotos()
        {
            _logger.LogInformation($"Getting all Photos");
            var query = _eventContext.Photos
                        .OrderBy(c => c.CreationDate);

            return await query.ToArrayAsync();
        }

        public async Task<Photos> GetPhoto(int photoId)
        {
            _logger.LogInformation($"Getting Photos for id {photoId}");

            var query = _eventContext.Photos
                        .Where(c => c.Id == photoId);

            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}
