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
            modelBuilder.Entity<SysMenu>().HasKey(t => t.Id);
            modelBuilder.Entity<SysModule>().HasKey(t => t.Id);

            modelBuilder.Entity<SysMenu>().HasOne(t => t.Module).WithMany(t => t.Menus).HasForeignKey(t => t.ModuleId);
            //配置父子关系
            modelBuilder.Entity<SysMenu>().HasOne(t => t.ParentMenu).WithMany(t => t.ChildrenMenu).HasPrincipalKey(t => t.Id).HasForeignKey(t => t.ParentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}


//public class Menu
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public Menu ParentMenu { get; set; }
//}