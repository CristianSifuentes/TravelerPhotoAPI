using API.Context;
using API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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


        //Trips
        public async Task<Trips[]> GetTrips()
        {
            _logger.LogInformation($"Getting all comedians");
            var query = _eventContext.Trips
                        .OrderBy(c => c.CreationDate);

            return await query.ToArrayAsync();
        }

    }
}
