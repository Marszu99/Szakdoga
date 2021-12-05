using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public interface ITaskLogic
    {
        int CreateTask(Task task, int userid);
        List<Task> GetAllTasks();
        List<Task> GetUserTasks(int userid);
        List<Task> GetAllActiveTasks();
        List<Task> GetAllActiveTasksFromUser(int userid);
        List<Task> GetAllDoneTasksFromUser(int userid);
        void UpdateTask(Task task, int taskid, int userid);
        void DeleteTask(int taskid);
    }
}
