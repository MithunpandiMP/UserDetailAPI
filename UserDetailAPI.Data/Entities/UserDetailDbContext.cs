using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserDetailAPI.DataAccessLayer.Entities;

public partial class UserDetailDbContext : DbContext
{
    public string DbPath { get; }
    public virtual DbSet<User> Users { get; set; }
    public UserDetailDbContext()
    {
        var folder = Environment.CurrentDirectory;
        DbPath = $"{folder}{System.IO.Path.DirectorySeparatorChar}UserDetails.sqlite";
    }
   
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    
}
