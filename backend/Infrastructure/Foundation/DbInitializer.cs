using Domain.Entities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Foundation
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            Role roleUser = new Role()
            {
                Id = 1,
                Name = "User",
            };

            Role roleAdmin = new Role()
            {
                Id = 2,
                Name = "Admin",
            };

            Role roleSuperUser = new Role()
            {
                Id = 3,
                Name = "Superuser",
            };

            User john = new User()
            {
                Id = 1,
                Avatar = "user-default.png",
                Biography = "Biography1..",
                Email = "one@mail.com",
                Password = new PasswordHasher().Hash("123123123"),
                Username = "john1"
            };

            modelBuilder.Entity<Role>().HasData(
                roleAdmin,
                roleSuperUser,
                roleUser
            );

            User paul = new User()
            {
                Id = 2,
                Avatar = "user-default.png",
                Biography = "Biography2..",
                Email = "two@mail.com",
                Password = new PasswordHasher().Hash("123123123"),
                Username = "paul1"
            };

            User bob = new User()
            {
                Id = 3,
                Avatar = "user-default.png",
                Biography = "Biography3..",
                Email = "three@mail.com",
                Password = new PasswordHasher().Hash("123123123"),
                Username = "bob1"
            };

            modelBuilder.Entity<User>().HasData(
                john,
                paul,
                bob
            );

            modelBuilder.Entity("RoleUser")
                .HasData(new[]
                    {
                        new { UsersId = john.Id, RolesId = roleSuperUser.Id },
                        new { UsersId = paul.Id, RolesId = roleAdmin.Id },
                        new { UsersId = bob.Id , RolesId = roleUser.Id }
                    }
                );

            Category availability = new Category()
            {
                Id = 1,
                Name = "Availability",
                Description = "Availability description"
            };

            Category interests = new Category()
            {
                Id = 2,
                Name = "Interests",
                Description = "Interests description"
            };

            Category road = new Category()
            {
                Id = 3,
                Name = "Road",
                Description = "Road description"
            };

            modelBuilder.Entity<Category>().HasData(
                availability,
                interests,
                road
            );

            Tag tourist = new Tag
            {
                Id = 1,
                Name = "Tourist",
                CategoryId = availability.Id
            };

            Tag badRoad = new Tag
            {
                Id = 2,
                Name = "Bad road",
                CategoryId = road.Id
            };

            Tag diving = new Tag
            {
                Id = 3,
                Name = "Diving",
                CategoryId = interests.Id
            };

            modelBuilder.Entity<Tag>().HasData(
                tourist,
                badRoad,
                diving
            );

            Route moscowPetersburg = new Route
            {
                Id = 1,
                Name = "Moscow - Saint Petersburg",
                Description = "Route description 1",
                Duration = 1,
                Status = 1,
                UserId = john.Id
            };

            Route mountainTour = new Route
            {
                Id = 2,
                Name = "Mountain tour",
                Description = "Route description 2",
                Duration = 10,
                Status = 1,
                UserId = paul.Id
            };

            Route europeanTravel = new Route
            {
                Id = 3,
                Name = "European travel",
                Description = "Route description 3",
                Duration = 20,
                Status = 0,
                UserId = bob.Id
            };

            modelBuilder.Entity<Route>().HasData(
                moscowPetersburg,
                mountainTour,
                europeanTravel
            );

            modelBuilder.Entity("RouteTag")
                .HasData(new[]
                    {
                        new { RoutesId = moscowPetersburg.Id, TagsId = badRoad.Id },
                        new { RoutesId = mountainTour.Id, TagsId = tourist.Id },
                        new { RoutesId = europeanTravel.Id, TagsId = diving.Id }
                    }
                );

            Location moscow = new Location
            {
                Id = 1,
                Order = 1,
                Description = "Location description 1",
                Coordinates = new NetTopologySuite.Geometries.Point(55.877806802128674, 37.560213608843085),
                Name = "Moscow",
                RouteId = moscowPetersburg.Id
            };

            Location altai = new Location
            {
                Id = 2,
                Order = 1,
                Description = "Location description 2",
                Coordinates = new NetTopologySuite.Geometries.Point(50.62478102741532, 86.32222662409758),
                Name = "Altai",
                RouteId = mountainTour.Id
            };

            Location paris = new Location
            {
                Id = 3,
                Order = 1,
                Description = "Location description 2",
                Coordinates = new NetTopologySuite.Geometries.Point(48.8615359552553, 2.358705469822234),
                Name = "Paris",
                RouteId = europeanTravel.Id
            };

            modelBuilder.Entity<Location>().HasData(
                moscow,
                altai,
                paris
            );

            ImageLocation moscowImg = new ImageLocation
            {
                Id = 1,
                Image = "image-location.jpg",
                LocationId = moscow.Id
            };

            ImageLocation altaiImg = new ImageLocation
            {
                Id = 2,
                Image = "image-location.jpg",
                LocationId = altai.Id
            };

            ImageLocation parisImg = new ImageLocation
            {
                Id = 3,
                Image = "image-location.jpg",
                LocationId = paris.Id
            };

            modelBuilder.Entity<ImageLocation>().HasData(
                moscowImg,
                altaiImg,
                parisImg
            );

            Note note1 = new Note
            {
                Id = 1,
                Text = "Text 1",
                CreatedAt = DateTime.UtcNow,
                RouteId = moscowPetersburg.Id
            };

            Note note2 = new Note
            {
                Id = 2,
                Text = "Text 2",
                CreatedAt = DateTime.UtcNow,
                RouteId = mountainTour.Id
            };

            Note note3 = new Note
            {
                Id = 3,
                Text = "Text 3",
                CreatedAt = DateTime.UtcNow,
                RouteId = europeanTravel.Id
            };

            modelBuilder.Entity<Note>().HasData(
                note1,
                note2,
                note3
            );

            Review review1 = new Review
            {
                Id = 1,
                Rate = 5,
                Text = "Text 1",
                CreatedAt = DateTime.UtcNow,
                UserId = john.Id,
                RouteId = europeanTravel.Id
            };

            Review review2 = new Review
            {
                Id = 2,
                Rate = 4,
                Text = "Text 2",
                CreatedAt = DateTime.UtcNow,
                UserId = paul.Id,
                RouteId = moscowPetersburg.Id
            };

            Review review3 = new Review
            {
                Id = 3,
                Rate = 3,
                Text = "Text 3",
                CreatedAt = DateTime.UtcNow,
                UserId = bob.Id,
                RouteId = mountainTour.Id
            };

            modelBuilder.Entity<Review>().HasData(
                review1,
                review2,
                review3
            );

            Moment moment1 = new Moment
            {
                Id = 1,
                Coordinates = new NetTopologySuite.Geometries.Point(48.858779767208894, 2.294590215790281),
                Description = "Moment description 1",
                CreatedAt = DateTime.UtcNow,
                Status = 4,
                UserId = john.Id
            };

            Moment moment2 = new Moment
            {
                Id = 2,
                Coordinates = new NetTopologySuite.Geometries.Point(55.751165864532894, 37.61726058361952),
                Description = "Moment description 2",
                CreatedAt = DateTime.UtcNow,
                Status = 5,
                UserId = paul.Id
            };

            Moment moment3 = new Moment
            {
                Id = 3,
                Coordinates = new NetTopologySuite.Geometries.Point(50.04278538093594, 87.40137428089643),
                Description = "Moment description 3",
                CreatedAt = DateTime.UtcNow,
                Status = 3,
                UserId = bob.Id
            };

            modelBuilder.Entity<Moment>().HasData(
                moment1,
                moment2,
                moment3
            );

            ImageMoment momentImg1 = new ImageMoment
            {
                Id = 1,
                Image = "Image1.jpg",
                MomentId = moment1.Id
            };

            ImageMoment momentImg2 = new ImageMoment
            {
                Id = 2,
                Image = "Image2.jpg",
                MomentId = moment2.Id
            };

            ImageMoment momentImg3 = new ImageMoment
            {
                Id = 3,
                Image = "Image3.jpg",
                MomentId = moment3.Id
            };

            modelBuilder.Entity<ImageMoment>().HasData(
                momentImg1,
                momentImg2,
                momentImg3
            );

            UserRoute johnMoscowPetersburg = new UserRoute
            {
                Id = 1,
                State = 1,
                UserId = john.Id,
                RouteId = moscowPetersburg.Id
            };

            UserRoute paulMountainTour = new UserRoute
            {
                Id = 2,
                State = 2,
                UserId = paul.Id,
                RouteId = mountainTour.Id
            };

            UserRoute bobEuropeanTravel = new UserRoute
            {
                Id = 3,
                State = 1,
                UserId = bob.Id,
                RouteId = europeanTravel.Id
            };

            modelBuilder.Entity<UserRoute>().HasData(
                johnMoscowPetersburg,
                paulMountainTour,
                bobEuropeanTravel
            );
        }
    }
}
