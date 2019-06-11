using AQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.EntityFrameworkCore
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysUser>().HasKey(t => t.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
