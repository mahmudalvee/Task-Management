// TaskManagement.API/Controllers/TaskController.cs
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Service;
using TaskManagement.Class;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _taskService.GetTask(id);
        if (task == null)
            return NotFound();
        else return Ok(task);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetTasksByStatus([FromQuery] EnumTaskStatus status, [FromQuery] int page, [FromQuery] int pageSize)
    {
        var tasks = await _taskService.GetTasksByStatus(status, page, pageSize);
        return Ok(tasks);
    }
    [HttpGet("assignedTo")]
    public async Task<IActionResult> GetTasksByAssignedUser([FromQuery] int assignedTo, [FromQuery] int page, [FromQuery] int pageSize)
    {
        var tasks = await _taskService.GetTasksByAssignedUser(assignedTo, page, pageSize);
        return Ok(tasks);
    }
    [HttpGet("teamID")]
    public async Task<IActionResult> GetTasksByTeam([FromQuery] int teamId,[FromQuery] int page, [FromQuery] int pageSize)
    {
        var tasks = await _taskService.GetTasksByTeam(teamId, page, pageSize);
        return Ok(tasks);
    }
    [HttpGet("dueDate")]
    public async Task<IActionResult> GetTasks( DateTime dueDate, [FromQuery] int page, [FromQuery] int pageSize)
    {
        var tasks = await _taskService.GetTasksByDueDate(dueDate, page, pageSize);
        return Ok(tasks);
    }

    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TTask task)
    {
        if (task == null)
        {
            return BadRequest("Task data can not be null");
        }

        var createdTask = await _taskService.CreateTaskWithNotfication(task);
        return Ok();
    }

    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TTask task)
    {
        if (id != task.Id)
            return BadRequest();

        await _taskService.UpdateTask(task);
        return Ok();
    }

    [Authorize(Roles = "Employee")]
    [HttpGet("assigned/{userId}")]
    public async Task<IActionResult> GetAssignedTaskStatusById(int userId)
    {
        var task = await _taskService.GetAssignedTaskById(userId);
        if (task == null)
            return NotFound();
        else return Ok(task);
    }

    [Authorize(Roles = "Employee")]
    [HttpPut("{taskId}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int taskId, EnumTaskStatus status)
    {
        
        await _taskService.UpdateTaskStatus(taskId, status);
        return Ok();
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTask(id);
        return Ok();
    }
}
