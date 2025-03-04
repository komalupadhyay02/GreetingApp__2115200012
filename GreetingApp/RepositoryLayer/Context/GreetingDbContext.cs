using System;
using Microsoft.EntityFrameworkCore;
using GreetingApp.Models;

using System.Collections.Generic;
namespace RepositoryLayer.Context
    {
        public class GreetingDbContext : DbContext
        {
            public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

            public DbSet<Greeting> Greetings { get; set; }
        } 
    }