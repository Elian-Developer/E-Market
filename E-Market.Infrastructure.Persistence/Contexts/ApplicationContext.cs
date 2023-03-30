
using E_Market.Core.Domain.Common;
using E_Market.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Adverstisements> Adverstisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //Table configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FLUENT API

            #region "Tables"
            modelBuilder.Entity<Adverstisements>().ToTable("Adverstisements");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<User>().ToTable("User");
            #endregion

            #region "Prymary keys"
            modelBuilder.Entity<Adverstisements>().HasKey(adverstisements => adverstisements.Id);
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Category>()
                .HasMany<Adverstisements>(category => category.Adverstisements)
                .WithOne(adverstisements => adverstisements.Category)
                .HasForeignKey(adverstisements => adverstisements.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Adverstisements>(user => user.Adverstisements)
                .WithOne(adverstisements => adverstisements.User)
                .HasForeignKey(adverstisements => adverstisements.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Property Configuration"

            #region "Adverstisements"
            modelBuilder.Entity<Adverstisements>()
                .Property(adverstisements => adverstisements.Name)
                .IsRequired();
            #endregion

            #region "Category"
            modelBuilder.Entity<Category>()
                .Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(70);
            #endregion

            #region "User"
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}
