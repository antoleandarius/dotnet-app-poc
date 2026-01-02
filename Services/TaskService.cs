using System;
using System.Collections.Generic;
using System.Linq;
using DotNetAppPoc.Models;

namespace DotNetAppPoc.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks;

        public TaskService()
        {
            // Initialize with sample data
            _tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1", Description = "First task", Status = TaskStatus.Open, Site = "Site A" },
                new TaskItem { Id = 2, Title = "Task 2", Description = "Second task", Status = TaskStatus.InProgress, Site = "Site A" },
                new TaskItem { Id = 3, Title = "Task 3", Description = "Third task", Status = TaskStatus.Completed, Site = "Site B" },
                new TaskItem { Id = 4, Title = "Task 4", Description = "Fourth task", Status = TaskStatus.Open, Site = "Site B" },
                new TaskItem { Id = 5, Title = "Task 5", Description = "Fifth task", Status = TaskStatus.Completed, Site = "Site A" },
                new TaskItem { Id = 6, Title = "Task 6", Description = "Sixth task", Status = TaskStatus.Cancelled, Site = "Site C" }
            };
        }

        public Dictionary<string, int> GetTaskCountByStatus(string? site = null)
        {
            var tasks = _tasks.AsEnumerable();

            // Filter by site if provided
            if (!string.IsNullOrEmpty(site))
            {
                tasks = tasks.Where(t => t.Site != null && t.Site.Equals(site, StringComparison.OrdinalIgnoreCase));
            }

            // Group by status and count
            var result = tasks
                .GroupBy(t => t.Status.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            return result;
        }
    }
}
