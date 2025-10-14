using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;
using TaskMgmtSys.Web.Entities;

namespace TaskMgmtSys.Web.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long,
            AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentAttachment> CommentAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---------------- IDENTITY ----------------
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");
            builder.Entity<AppUserRole>().ToTable("UserRoles");
            builder.Entity<AppUserClaim>().ToTable("UserClaims");
            builder.Entity<AppUserLogin>().ToTable("UserLogins");
            builder.Entity<AppRoleClaim>().ToTable("RoleClaims");
            builder.Entity<AppUserToken>().ToTable("UserTokens");

            builder.Entity<AppUser>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(u => u.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(u => u.CreatedByUser).WithMany().HasForeignKey(u => u.CreatedBy).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(u => u.UpdatedByUser).WithMany().HasForeignKey(u => u.UpdatedBy).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.UserProjects).WithOne(up => up.User).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.TaskItems).WithOne(t => t.ActiveAssignedUser).HasForeignKey(t => t.ActiveAssignedUserId).OnDelete(DeleteBehavior.SetNull);
                entity.HasMany(u => u.TaskAssignments).WithOne(ta => ta.User).HasForeignKey(ta => ta.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.TaskAttachments).WithOne(wo => wo.User).HasForeignKey(wo => wo.UserId).OnDelete(DeleteBehavior.SetNull);
                entity.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.Property(r => r.Description).HasMaxLength(500);
            });

            builder.Entity<AppUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserClaim>(entity =>
            {
                entity.HasOne(uc => uc.User).WithMany(u => u.Claims).HasForeignKey(uc => uc.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppRoleClaim>(entity =>
            {
                entity.HasOne(rc => rc.Role).WithMany(r => r.RoleClaims).HasForeignKey(rc => rc.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserLogin>(entity =>
            {
                entity.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
                entity.HasOne(ul => ul.User).WithMany(u => u.Logins).HasForeignKey(ul => ul.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserToken>(entity =>
            {
                entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
                entity.HasOne(ut => ut.User).WithMany(u => u.Tokens).HasForeignKey(ut => ut.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ProjectName).IsRequired().HasMaxLength(300);
                entity.Property(p => p.Description).HasMaxLength(5000);
                entity.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

                entity.HasMany(p => p.UserProjects).WithOne(up => up.Project).HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(p => p.TaskItems).WithOne(t => t.Project).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserProject>(entity =>
            {
                entity.HasKey(up => up.UserProjectId);
                entity.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(up => up.User).WithMany(u => u.UserProjects).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(up => up.Project).WithMany(p => p.UserProjects).HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(300);
                entity.Property(t => t.Description).HasMaxLength(30000);
                entity.Property(t => t.StatusId).HasConversion<int>().IsRequired();
                entity.Property(t => t.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(t => t.IsDeleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(t => t.Project).WithMany(p => p.TaskItems).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(t => t.ActiveAssignedUser).WithMany(u => u.TaskItems).HasForeignKey(t => t.ActiveAssignedUserId).OnDelete(DeleteBehavior.SetNull);
                entity.HasMany(t => t.TaskAssignments).WithOne(ta => ta.TaskItem).HasForeignKey(ta => ta.TaskItemId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(t => t.TaskAttachments).WithOne(ta => ta.TaskItem).HasForeignKey(ta => ta.TaskItemId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(t => t.Comments).WithOne(c => c.TaskItem).HasForeignKey(c => c.TaskItemId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(ta => ta.Id);
                entity.Property(ta => ta.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(ta => ta.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(ta => ta.TaskItem).WithMany(t => t.TaskAssignments).HasForeignKey(ta => ta.TaskItemId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ta => ta.User).WithMany(u => u.TaskAssignments).HasForeignKey(ta => ta.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TaskAttachment>(entity =>
            {
                entity.HasKey(ta => ta.Id);
                entity.Property(ta => ta.FilePath).IsRequired().HasMaxLength(2000);
                entity.Property(ta => ta.FileName).IsRequired().HasMaxLength(500);
                entity.Property(ta => ta.ContentType).HasMaxLength(100);
                entity.Property(ta => ta.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(ta => ta.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(ta => ta.TaskItem).WithMany(t => t.TaskAttachments).HasForeignKey(ta => ta.TaskItemId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ta => ta.User).WithMany(u => u.TaskAttachments).HasForeignKey(ta => ta.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Content).IsRequired().HasMaxLength(10000);
                entity.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(c => c.TaskItem).WithMany(t => t.Comments).HasForeignKey(c => c.TaskItemId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(c => c.CommentAttachments).WithOne(ca => ca.Comment).HasForeignKey(ca => ca.CommentId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<CommentAttachment>(entity =>
            {
                entity.HasKey(ca => ca.Id);
                entity.Property(ca => ca.FilePath).IsRequired().HasMaxLength(2000);
                entity.Property(ca => ca.FileName).IsRequired().HasMaxLength(500);
                entity.Property(ca => ca.ContentType).HasMaxLength(100);
                entity.Property(ca => ca.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(ca => ca.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(ca => ca.Comment).WithMany(c => c.CommentAttachments).HasForeignKey(ca => ca.CommentId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ca => ca.User).WithMany(u => u.CommentAttachments).HasForeignKey(ca => ca.UserId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
