using System.Collections.Generic;
using DotNetAppPoc.Models;

namespace DotNetAppPoc.Services
{
    public interface ITaskService
    {
        Dictionary<string, int> GetTaskCountByStatus(string? site = null);
    }
}
