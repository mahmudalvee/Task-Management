using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;
using TaskManagement.Data;


namespace TaskManagement.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<Notification> Create(int userId, string message)
        {
            var n = new Notification
            {
                UserId = userId,
                Message = message,
                CreatedAt = DateTime.UtcNow
            };

            var saved = await _repo.Add(n);

//log in file
            return saved;
        }

        public Task<IEnumerable<Notification>> GetForUser(int userId)=> _repo.GetByUser(userId);

    }

}
