using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ITravelerRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> Save();


        Task<Trips[]> GetTrips();
        Task<Trips> GetTrip(int tripId);


        Task<Photos[]> GetPhotos();
        Task<Photos> GetPhoto(int photoId);
    }
}
