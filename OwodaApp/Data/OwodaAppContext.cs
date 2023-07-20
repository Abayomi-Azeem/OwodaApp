using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OwodaApp.Models;

namespace OwodaApp.Data
{
    public class OwodaAppContext : DbContext
    {
        public OwodaAppContext (DbContextOptions<OwodaAppContext> options)
            : base(options)
        {
        }

        public DbSet<OwodaApp.Models.Member> Member { get; set; } = default!;

        public DbSet<OwodaApp.Models.Vehicle>? Vehicle { get; set; }

        public DbSet<OwodaApp.Models.Payment>? Payment { get; set; }
    }
}
