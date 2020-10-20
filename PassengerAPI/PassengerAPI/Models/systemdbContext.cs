using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PassengerAPI.Models
{
    public partial class systemdbContext : DbContext
    {
        public systemdbContext()
        {
        }

        public systemdbContext(DbContextOptions<systemdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminAcc> AdminAcc { get; set; }
        public virtual DbSet<BookingDetail> BookingDetail { get; set; }
        public virtual DbSet<PassengerAcc> PassengerAcc { get; set; }
        public virtual DbSet<TicketType> TicketType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-P69QGKA0;Database=systemdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminAcc>(entity =>
            {
                entity.ToTable("admin_acc");

                entity.HasIndex(e => e.AdminId)
                    .HasName("UQ__admin_ac__43AA41402601554C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("admin_password")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.ToTable("booking_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.PassengerName)
                    .HasColumnName("passenger_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<PassengerAcc>(entity =>
            {
                entity.ToTable("passenger_acc");

                entity.HasIndex(e => e.PassengerId)
                    .HasName("UQ__passenge__03764587073C3C50")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PassengerAddress)
                    .HasColumnName("passenger_address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.PassengerMobile)
                    .HasColumnName("passenger_mobile")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasColumnName("passenger_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerPassword)
                    .IsRequired()
                    .HasColumnName("passenger_password")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.ToTable("ticket_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
