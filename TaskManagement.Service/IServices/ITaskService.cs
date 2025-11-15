using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;

namespace TaskManagement.Service
{
    public interface ITaskService
    {
        Task<TTask> CreateTaskWithNotfication(TTask task);
        Task<TTask> GetTask(int id);
        Task<IEnumerable<TTask>> GetAssignedTaskById(int id);

        Task<IEnumerable<TTask>> GetTasksByStatus(EnumTaskStatus status, int page, int pageSize);
        Task<IEnumerable<TTask>> GetTasksByAssignedUser(int assignedTo, int page, int pageSize);
            Task<IEnumerable<TTask>> GetTasksByTeam(int teamId, int page, int pageSize);
            Task<IEnumerable<TTask>> GetTasksByDueDate(DateTime dueDate, int page, int pageSize);
        Task UpdateTask(TTask task);
        Task UpdateTaskStatus(int taskID, EnumTaskStatus status);
        Task DeleteTask(int id);
    }
}
