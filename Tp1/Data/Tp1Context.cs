using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp1.Models;

namespace Tp1.Data
{
    public class Tp1Context : DbContext
    {
        public Tp1Context(DbContextOptions<Tp1Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
