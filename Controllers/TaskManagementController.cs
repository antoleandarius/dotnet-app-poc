using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DotNetAppPoc.Services;

namespace DotNetAppPoc.Controllers
{
    [ApiController]
    [Route("api/taskmgmt")]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskManagementController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get count of tasks grouped by status
        /// </summary>
        /// <param name="site">Optional site filter</param>
        /// <returns>Dictionary with status names as keys and counts as values</returns>
        [HttpGet("task/count/by-status")]
        public ActionResult<Dictionary<string, int>> GetTaskCountByStatus([FromQuery] string? site = null)
        {
            var result = _taskService.GetTaskCountByStatus(site);
            return Ok(result);
        }
    }
}
