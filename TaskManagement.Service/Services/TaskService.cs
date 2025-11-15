using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;
using TaskManagement.Data;
using TaskManagement.Data.Contexts;
using TaskManagement.Service;

namespace TaskManagement.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationService _notificationService;

        private readonly TaskManagementDBContext _context;


        public TaskService(ITaskRepository taskRepository, INotificationService notificationService, TaskManagementDBContext context)
        {
            _taskRepository = taskRepository;
            _notificationService = notificationService;
            _context = context;
        }

        public async Task<TTask> GetTask(int id)
        {
            return await _taskRepository.GetTaskById(id);
        }
        public async Task<IEnumerable<TTask>> GetAssignedTaskById(int id)
        {
            return await _taskRepository.GetAssignedTaskById(id);
        }

        public async Task<IEnumerable<TTask>> GetTasksByStatus(EnumTaskStatus status, int page, int pageSize)
        {
            return await _taskRepository.GetTasksByStatus(status, page, pageSize);
        }
        public async Task<IEnumerable<TTask>> GetTasksByAssignedUser(int assignedTo, int page, int pageSize)
        {
            return await _taskRepository.GetTasksByAssignedUser(assignedTo, page, pageSize);
        }
        public async Task<IEnumerable<TTask>> GetTasksByTeam(int teamId, int page, int pageSize)
        {
            return await _taskRepository.GetTasksByTeam(teamId, page, pageSize);
        }
        public async Task<IEnumerable<TTask>> GetTasksByDueDate(DateTime dueDate, int page, int pageSize)
        {
            return await _taskRepository.GetTasksByDueDate(dueDate, page, pageSize);
        }


        public async Task<TTask> CreateTaskWithNotfication(TTask task)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _taskRepository.AddTask(task);

                    var notificationMessage = $"Task: '{task.Name}' has been assigned to you";
                    var notification = await _notificationService.Create(task.AssignedToUserId, notificationMessage);

                    await transaction.CommitAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error creating task .", ex);
                }
            }
        }

        public async Task UpdateTask(TTask task)
        {
            await _taskRepository.UpdateTask(task);
        }
        public async Task UpdateTaskStatus(int taskID, EnumTaskStatus status)
        {
            await _taskRepository.UpdateTaskStatus(taskID, status);
        }

        public async Task DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
        }
    }
}
