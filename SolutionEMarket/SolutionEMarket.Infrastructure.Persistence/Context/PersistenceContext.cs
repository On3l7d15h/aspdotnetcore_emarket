using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Microsoft.EntityFrameworkCore;
using SolutionEMarket.Core.Domain.Commom;
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Infrastructure.Persistence.Context
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {

        }

        //DbSets
        #region DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        //Configuration Async
        public override Task<int> SaveChangesAsync(CancellationToken cancelToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State) 
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "App";
                        break;
                    case EntityState.Modified:
                        entry.Entity.Updated = DateTime.Now;
                        entry.Entity.UpdatedBy = "App";
                        break;
                }
            }
            return base.SaveChangesAsync(cancelToken);
        }

        //OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //settings
            #region Tables
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region PrimaryKeys
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            #endregion

            #region Properties

            #region Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .IsRequired();
            #endregion

            #region Product
            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(c => c.Description)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(c => c.Price)
                .IsRequired()
                .HasDefaultValue(0.00);

            modelBuilder.Entity<Product>()
                .Property(c => c.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(c => c.UserId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(c => c.ImagePath1)
                .IsRequired();
            #endregion

            #region User
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .IsRequired();
            #endregion

            #endregion

            #region RelationShips

            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Product>(u => u.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
