using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Task_manager.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_manager.DAL
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<DbContext> options): base(options) { Database.EnsureCreated();}
        public DbSet<TaskEntity> tasks { get; set; }


       public AppDbContext()        {           Database.EnsureCreated();        }
               
       protected override void OnConfiguring(DbContextOptionsBuilder options)
       {
          options.UseSqlServer("Server=ECHIPSCOMPUTER;Database=task_manager;Trusted_Connection=True;TrustServerCertificate=True");
        }
       
    }


    
    
}

