using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder.Entity<User>().HasData(
                   new User() { 
                       Id = 1, 
                       Avatar = "avatar1.jpg", 
                       Biography = "Biography1..", 
                       Email = "one@mail.com", 
                       Name="John", 
                       Password="ddfsdfwfe", 
                       Username="john1" 
                   },
                   new User() { 
                       Id = 2, 
                       Avatar = "avatar2.jpg", 
                       Biography = "Biography2..", 
                       Email = "two@mail.com", 
                       Name = "Paul", 
                       Password = "ddfsdfwfe", 
                       Username = "paul1" 
                   }
            );
        }
    }
}
