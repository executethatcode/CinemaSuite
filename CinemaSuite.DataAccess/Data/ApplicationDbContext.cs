using CinemaSuite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaSuite.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Comedy", DisplayOrder = 4 }
                );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Duration = 148,
                    Director = "Christopher Nolan",
                    DvdPrice = 14.99M,
                    BlurayPrice = 19.99M,
                    CategoryId = 2,
                    ImageUrl =""

                },
                new Movie
                {
                    Id = 2,
                    Title = "The Shawshank Redemption",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    ReleaseDate = new DateTime(1994, 9, 23),
                    Duration = 142,
                    Director = "Frank Darabont",
                    DvdPrice = 12.99M,
                    BlurayPrice = 17.99M,
                    CategoryId = 3,
                    ImageUrl = ""

                },
                new Movie
                {
                    Id = 3,
                    Title = "The Godfather",
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    ReleaseDate = new DateTime(1972, 3, 24),
                    Duration = 175,
                    Director = "Francis Ford Coppola",
                    DvdPrice = 15.99M,
                    BlurayPrice = 21.99M,
                    CategoryId = 2,
                    ImageUrl = ""

                },
                new Movie
                {
                    Id = 4,
                    Title = "The Dark Knight",
                    Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    ReleaseDate = new DateTime(2008, 7, 18),
                    Duration = 152,
                    Director = "Christopher Nolan",
                    DvdPrice = 13.99M,
                    BlurayPrice = 18.99M,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Movie
                {
                    Id = 5,
                    Title = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster, and his wife intertwine in four tales of violence and redemption.",
                    ReleaseDate = new DateTime(1994, 10, 14),
                    Duration = 154,
                    Director = "Quentin Tarantino",
                    DvdPrice = 11.99M,
                    BlurayPrice = 16.99M,
                    CategoryId = 2,
                    ImageUrl = ""
                }
 
                );
        }
    }
}
