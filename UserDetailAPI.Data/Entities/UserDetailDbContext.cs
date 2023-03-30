using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserDetailAPI.DataAccessLayer.Entities;

public partial class UserDetailDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public UserDetailDbContext(DbContextOptions<UserDetailDbContext> options) : base(options) { }
}
