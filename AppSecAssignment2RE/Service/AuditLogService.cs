using AppSecAssignment2RE.Models;

namespace AppSecAssignment2RE.Service
{
    public class AuditLogService
    {
        private readonly AuthDbContext _context;

        public AuditLogService(AuthDbContext context)
        {
            _context = context;
        }

        public void Log(string userId, string action, string details)
        {
            var logEntry = new AuditLog
            {
                Timestamp = DateTime.UtcNow,
                UserId = userId,
                Action = action,
                Details = details
            };

            _context.AuditLogs.Add(logEntry);
            _context.SaveChanges();
        }
    }

}
