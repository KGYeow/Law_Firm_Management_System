using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Law_Firm_Management_System_API.Models
{
    public partial class Law_Firm_Management_System_DBContext : DbContext
    {
        public Law_Firm_Management_System_DBContext()
        {
        }

        public Law_Firm_Management_System_DBContext(DbContextOptions<Law_Firm_Management_System_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentCategory> AppointmentCategories { get; set; } = null!;
        public virtual DbSet<Case> Cases { get; set; } = null!;
        public virtual DbSet<CaseStatus> CaseStatuses { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Paralegal> Paralegals { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<RoleAccessPage> RoleAccessPages { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ROBIN;Database=Law_Firm_Management_System_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.PartnerUserId).HasColumnName("PartnerUserID");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.PartnerUser)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.PartnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Announcement_Partner");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppointmentTime).HasColumnType("datetime");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.PartnerUserId).HasColumnName("PartnerUserID");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_AppointmentCategory");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Client");

                entity.HasOne(d => d.PartnerUser)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PartnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Partner");
            });

            modelBuilder.Entity<AppointmentCategory>(entity =>
            {
                entity.ToTable("AppointmentCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.ToTable("Case");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ClosedTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PartnerUserId).HasColumnName("PartnerUserID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Client_Case");

                entity.HasOne(d => d.PartnerUser)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.PartnerUserId)
                    .HasConstraintName("FK_Partner_Case");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Case_Status");
            });

            modelBuilder.Entity<CaseStatus>(entity =>
            {
                entity.ToTable("CaseStatus");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).HasMaxLength(256);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Client_User");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaseId).HasColumnName("CaseID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PartnerUserId).HasColumnName("PartnerUserID");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_Document_Case");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_DocumentCategory");

                entity.HasOne(d => d.PartnerUser)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.PartnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Partner");
            });

            modelBuilder.Entity<DocumentCategory>(entity =>
            {
                entity.ToTable("DocumentCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaseId).HasColumnName("CaseID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_Event_Case");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Page");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccessName).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paralegal>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Paralegal");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.PhoneNumber).HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Paralegal)
                    .HasForeignKey<Paralegal>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Paralegal_User");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Partner");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.ParalegalUserId).HasColumnName("ParalegalUserID");

                entity.Property(e => e.PhoneNumber).HasMaxLength(256);

                entity.HasOne(d => d.ParalegalUser)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.ParalegalUserId)
                    .HasConstraintName("FK_Partner_Paralegal");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Partner)
                    .HasForeignKey<Partner>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_User");
            });

            modelBuilder.Entity<RoleAccessPage>(entity =>
            {
                entity.ToTable("RoleAccessPage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.RoleAccessPages)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleAccessPage_Page");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.RoleAccessPages)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleAccessPage_UserRole");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedTime).HasColumnType("datetime");

                entity.Property(e => e.CaseId).HasColumnName("CaseID");

                entity.Property(e => e.CompletedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.DueTime).HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ParelegalUserId).HasColumnName("ParelegalUserID");

                entity.Property(e => e.PartnerUserId).HasColumnName("PartnerUserID");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_Task_Case");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("FK_Task_Document");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Task_Event");

                entity.HasOne(d => d.ParelegalUser)
                    .WithMany(p => p.TaskParelegalUsers)
                    .HasForeignKey(d => d.ParelegalUserId)
                    .HasConstraintName("FK_Task_Parelegal");

                entity.HasOne(d => d.PartnerUser)
                    .WithMany(p => p.TaskPartnerUsers)
                    .HasForeignKey(d => d.PartnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Partner");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
