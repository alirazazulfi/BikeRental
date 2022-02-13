using BikeRental.Common;
using BikeRental.Entities.DBEtities;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Repository
{
    public class SeedData
    {
        public static void Seed(ModelBuilder builder)
        { 
            builder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Ali",
                LastName = "Raza",
                UserRole = 1, //--Manager
                Username = "devadmin",
                Email = "alirazazulfi@gmail.com",
                Mobile = "+923438506556",
                Password = EncryptionManager.Encrypt("admin123"),
                CreatedDate = DateTime.UtcNow
            }); 

            builder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "1",
                UserRole = 2, //--User
                Username = "user1",
                Email = "user1@test.com",
                Mobile = "+92",
                Password = EncryptionManager.Encrypt("user123"),
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "2",
                UserRole = 2, //--User
                Username = "user2",
                Email = "user2@test.com",
                Mobile = "+92",
                Password = EncryptionManager.Encrypt("user123"),
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "3",
                UserRole = 2, //--User
                Username = "user3",
                Email = "user3@test.com",
                Mobile = "+92",
                Password = EncryptionManager.Encrypt("user123"),
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 1",
                Model = "2020",
                Color = "Red",
                Location = "Lahore",
                PerDayRate = 20,  
                CreatedDate = DateTime.UtcNow
            });
            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 2",
                Model = "2020",
                Color = "Red",
                Location = "Lahore",
                PerDayRate = 20,  
                CreatedDate = DateTime.UtcNow
            });
            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 3",
                Model = "2021",
                Color = "Black",
                Location = "Lahore",
                PerDayRate = 22,  
                CreatedDate = DateTime.UtcNow
            });
            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 4",
                Model = "2021",
                Color = "Red",
                Location = "Islamabad",
                PerDayRate = 20,  
                CreatedDate = DateTime.UtcNow
            });
            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 5",
                Model = "2022",
                Color = "Blue",
                Location = "Islamabad",
                PerDayRate = 30,  
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 6",
                Model = "2022",
                Color = "Blue",
                Location = "Lahore",
                PerDayRate = 28,  
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<Bike>().HasData(new Bike
            {
                Id = Guid.NewGuid(),
                Name = "Bike 7",
                Model = "2022",
                Color = "Green",
                Location = "Islamabad",
                PerDayRate = 29,  
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}