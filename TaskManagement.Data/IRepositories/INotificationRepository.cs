using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;

namespace TaskManagement.Data
{
    public interface INotificationRepository
    {
        Task<Notification> Add(Notification notification);
        Task<IEnumerable<Notification>> GetByUser(int userId);
    }
}
