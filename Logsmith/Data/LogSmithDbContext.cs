using Logsmith.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logsmith.Api.Data
{
    public class LogSmithDbContext : DbContext
    {
        public LogSmithDbContext(DbContextOptions<LogSmithDbContext> options) : base(options) { }

        // get만 허용
        // 프로퍼티 자체를 외부에서 엉뚱하게 바꾸지 못하게 하려는 보호(Protection)목적
        public DbSet<EventLog> EventLogs => Set<EventLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>(e =>
            {
                e.ToTable("EventLog");
                e.HasKey(el => el.Id);
                e.Property(el => el.EventType).HasMaxLength(50).IsRequired();
                e.Property(el => el.Device).HasMaxLength(40);
                e.Property(el => el.Country).HasMaxLength(2);
                e.Property(el => el.CreatedDate).HasDefaultValueSql("DATEADD(HOUR, 9, SYSUTCDATETIME())");

                e.HasIndex(el => el.CreatedDate);
                e.HasIndex(el => new { el.EventType, el.CreatedDate });
                e.HasIndex(el => new { el.UserId, el.CreatedDate });
            });
        }
    }
}
