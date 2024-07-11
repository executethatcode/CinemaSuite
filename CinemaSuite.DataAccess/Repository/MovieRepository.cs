using CinemaSuite.DataAccess.Data;
using CinemaSuite.DataAccess.Repository.IRepository;
using CinemaSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSuite.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private ApplicationDbContext _db;
        public MovieRepository(ApplicationDbContext db) : base(db) => _db = db;
        public void Update(Movie obj)
        {
            var objFromDb = _db.Movies.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ReleaseDate = obj.ReleaseDate;
                objFromDb.Duration = obj.Duration;
                objFromDb.Director = obj.Director;
                objFromDb.DvdPrice = obj.DvdPrice;
                objFromDb.BlurayPrice = obj.BlurayPrice;
                objFromDb.Category = obj.Category;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
