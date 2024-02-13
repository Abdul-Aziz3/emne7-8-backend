using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Data;

public class StudentBloggDbContext : DbContext
{
    public StudentBloggDbContext(DbContextOptions<StudentBloggDbContext> options)
        : base(options)
    {
    }

    // tabellenes lages
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // User configuration
    //    modelBuilder.Entity<User>()
    //        .HasKey(u => u.Id);

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.UserName)
    //        .IsRequired()
    //        .HasMaxLength(30);


    //    modelBuilder.Entity<User>()
    //        .Property(u => u.FirstName)
    //        .IsRequired()
    //        .HasMaxLength(50);

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.LastName)
    //        .IsRequired()
    //        .HasMaxLength(100);

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.HashedPassword)
    //        .IsRequired();

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.Salt)
    //        .IsRequired();

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.Email)
    //        .HasColumnType("varchar(255)");  // Assuming max email length is 255. Adjust if necessary.

    //    modelBuilder.Entity<User>()
    //        .Property(u => u.IsAdminUser)
    //        .IsRequired();

    //    // Post configuration
    //    modelBuilder.Entity<Post>()
    //        .HasKey(p => p.Id);

    //    modelBuilder.Entity<Post>()
    //        .Property(p => p.Title)
    //        .IsRequired()
    //        .HasMaxLength(3);

    //    modelBuilder.Entity<Post>()
    //        .Property(p => p.Content)
    //        .IsRequired();

    //    modelBuilder.Entity<Post>()
    //        .HasOne(p => p.User)
    //        .WithMany(u => u.Posts)
    //        .HasForeignKey(p => p.UserId);

    //    // Comment configuration
    //    modelBuilder.Entity<Comment>()
    //        .HasKey(c => c.Id);

    //    modelBuilder.Entity<Comment>()
    //        .Property(c => c.Content)
    //        .IsRequired();

    //    modelBuilder.Entity<Comment>()
    //        .HasOne(c => c.Post)
    //        .WithMany(p => p.Comments)
    //        .HasForeignKey(c => c.PostId);

    //    modelBuilder.Entity<Comment>()
    //        .HasOne(c => c.User)
    //        .WithMany(u => u.Comments)
    //        .HasForeignKey(c => c.UserId);
    //}
}
