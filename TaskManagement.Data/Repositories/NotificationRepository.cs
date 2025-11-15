using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;
using TaskManagement.Data.Contexts;
using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskManagementContext _ctx;

        public NotificationRepository(TaskManagementContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Notification> Add(Notification notification)
        {
            _ctx.Notifications.Add(notification);
            await _ctx.SaveChangesAsync();
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetByUser(int userId)
        {
            var q = _ctx.Notifications.Where(n => n.UserId == userId);
            return await q.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

    }
}
