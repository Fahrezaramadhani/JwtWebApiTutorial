using JwtWebApiTutorial.Models;
using Microsoft.EntityFrameworkCore;


namespace JwtWebApiTutorial.Data
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Tables in Database
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            var name = GetAuthenticatedUsername();
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedAt"] = DateTime.Now;
                        entry.CurrentValues["CreatedBy"] = name;
                        entry.CurrentValues["DeletedAt"] = null;
                        entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                        entry.CurrentValues["UpdatedBy"] = name;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                        entry.CurrentValues["UpdatedBy"] = name;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedAt"] = DateTime.Now;
                        entry.CurrentValues["DeletedBy"] = name;
                        break;
                }
            }
        }

        private string GetAuthenticatedUsername()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            return userId != null ? Users.Find(int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name))?.Name : "System";
        }

    }
}