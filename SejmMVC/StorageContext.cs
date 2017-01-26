namespace SejmMVC
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StorageContext : DbContext
    {
        public StorageContext()
            : base("name=StorageContext")
        {
        }

        public virtual DbSet<Głos> Głos { get; set; }
        public virtual DbSet<Klub> Klub { get; set; }
        public virtual DbSet<Poseł> Poseł { get; set; }
        public virtual DbSet<Ustawa> Ustawa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Głos>()
                .Property(e => e.Stamp)
                .IsFixedLength();

            modelBuilder.Entity<Klub>()
                .Property(e => e.Nazwa)
                .IsFixedLength();

            modelBuilder.Entity<Klub>()
                .Property(e => e.Skrót)
                .IsFixedLength();

            modelBuilder.Entity<Klub>()
                .Property(e => e.Stamp)
                .IsFixedLength();

            modelBuilder.Entity<Klub>()
                .HasMany(e => e.Poseł)
                .WithOptional(e => e.Klub)
                .HasForeignKey(e => e.Klub_parlamentarny);

            modelBuilder.Entity<Klub>()
                .HasMany(e => e.Ustawa)
                .WithRequired(e => e.Klub)
                .HasForeignKey(e => e.ZgłoszonaPrzez);

            modelBuilder.Entity<Poseł>()
                .Property(e => e.Imie)
                .IsFixedLength();

            modelBuilder.Entity<Poseł>()
                .Property(e => e.Nazwisko)
                .IsFixedLength();

            modelBuilder.Entity<Poseł>()
                .Property(e => e.Stamp)
                .IsFixedLength();

            modelBuilder.Entity<Poseł>()
                .HasMany(e => e.Głos)
                .WithRequired(e => e.Poseł1)
                .HasForeignKey(e => e.Poseł);

            modelBuilder.Entity<Ustawa>()
                .Property(e => e.Nazwa)
                .IsFixedLength();

            modelBuilder.Entity<Ustawa>()
                .Property(e => e.Stamp)
                .IsFixedLength();

            modelBuilder.Entity<Ustawa>()
                .HasMany(e => e.Głos)
                .WithRequired(e => e.Ustawa1)
                .HasForeignKey(e => e.Ustawa);
        }
    }
}
