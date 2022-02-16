using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ParallelApp.Shared.Models;

namespace ParallelApp.Server.Models
{
    public partial class paralleldbContext : DbContext
    {
        public paralleldbContext()
        {
        }

        public paralleldbContext(DbContextOptions<paralleldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Messagetag> Messagetags { get; set; } = null!;
        public virtual DbSet<Notificationservice> Notificationservices { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userlike> Userlikes { get; set; } = null!;
        public virtual DbSet<Usernotificationservice> Usernotificationservices { get; set; } = null!;
        public virtual DbSet<Userrole> Userroles { get; set; } = null!;
        public virtual DbSet<Usertag> Usertags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=ParallelDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.7.3-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasIndex(e => e.SchoolId, "SchoolID");

                entity.HasIndex(e => e.SenderUserId, "SenderUserID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Likes).HasColumnType("int(11)");

                entity.Property(e => e.SchoolId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SchoolID");

                entity.Property(e => e.SenderUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SenderUserID");

                entity.Property(e => e.Subject).HasMaxLength(255);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("messages_ibfk_1");

                entity.HasOne(d => d.SenderUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("messages_ibfk_2");
            });

            modelBuilder.Entity<Messagetag>(entity =>
            {
                entity.ToTable("messagetags");

                entity.HasIndex(e => e.MessageId, "MessageID");

                entity.HasIndex(e => new { e.TagId, e.MessageId }, "unique_tags")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.MessageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("MessageID");

                entity.Property(e => e.TagId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TagID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Messagetags)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("messagetags_ibfk_1");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Messagetags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("messagetags_ibfk_2");
            });

            modelBuilder.Entity<Notificationservice>(entity =>
            {
                entity.ToTable("notificationservices");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Enabled)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("schools");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Enabled)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LogoUrl).HasMaxLength(255);

                entity.Property(e => e.LongName).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasColumnType("int(11)");

                entity.Property(e => e.ShortName).HasMaxLength(255);

                entity.Property(e => e.WebsiteUrl).HasMaxLength(255);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.HasIndex(e => e.SchoolId, "SchoolID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SchoolId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SchoolID");

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tags_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.SchoolId, "SchoolID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Enabled)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.HrEduUserPersistentId)
                    .HasMaxLength(255)
                    .HasColumnName("HrEduUserPersistentID");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.ProfilePictureUrl).HasMaxLength(255);

                entity.Property(e => e.SchoolId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SchoolID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_1");
            });

            modelBuilder.Entity<Userlike>(entity =>
            {
                entity.ToTable("userlikes");

                entity.HasIndex(e => e.MessageId, "MessageID");

                entity.HasIndex(e => new { e.UserId, e.MessageId }, "unique_tags")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.MessageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("MessageID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Userlikes)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userlikes_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userlikes_ibfk_2");
            });

            modelBuilder.Entity<Usernotificationservice>(entity =>
            {
                entity.ToTable("usernotificationservices");

                entity.HasIndex(e => e.NotificationServiceId, "NotificationServiceID");

                entity.HasIndex(e => new { e.UserId, e.NotificationServiceId }, "unique_tags")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Account).HasMaxLength(255);

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.NotificationServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("NotificationServiceID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.NotificationService)
                    .WithMany(p => p.Usernotificationservices)
                    .HasForeignKey(d => d.NotificationServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usernotificationservices_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usernotificationservices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usernotificationservices_ibfk_1");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userroles");

                entity.HasIndex(e => e.RoleId, "RoleID");

                entity.HasIndex(e => new { e.UserId, e.RoleId }, "unique_tags")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userroles_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userroles_ibfk_1");
            });

            modelBuilder.Entity<Usertag>(entity =>
            {
                entity.ToTable("usertags");

                entity.HasIndex(e => e.TagId, "TagID");

                entity.HasIndex(e => new { e.UserId, e.TagId }, "unique_tags")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.TagId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TagID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Usertags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usertags_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usertags)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usertags_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
