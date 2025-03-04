using System;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

using System.Collections.Generic;
namespace RepositoryLayer.Context
    {
        public class GreetingDbContext : DbContext
        {
            public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

            public DbSet<Greeting> Greetings { get; set; }
        }
    }