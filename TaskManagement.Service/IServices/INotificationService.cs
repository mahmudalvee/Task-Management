using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;

namespace TaskManagement.Service
{
    public interface INotificationService
    {
        Task<Notification> Create(int userId, string message);
        Task<IEnumerable<Notification>> GetForUser(int userId);
    }
}
