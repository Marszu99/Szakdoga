using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Model;
using TimeSheet.Model.Extension;

namespace TimeSheet.Logic
{
    public class TaskRepository
    {
        private ITaskLogic _tasklogic;

        public TaskRepository(ITaskLogic tasklogic)
        {
            _tasklogic = tasklogic;
        }

        public int CreateTask(Task task, int userid)
        {

            if (TaskValidationHelper.ValidateTitle(task.Title) != null)
            {
                throw new TaskValidationException(TaskValidationHelper.ValidateTitle(task.Title));
            }

            if (TaskValidationHelper.ValidateDeadline(task.Deadline) != null)
            {
                throw new TaskValidationException(TaskValidationHelper.ValidateDeadline(task.Deadline));
            }

            return _tasklogic.CreateTask(task, userid);
        }

        public List<Task> GetAllTasks()
        {
            return _tasklogic.GetAllTasks();
        }

        public List<Task> GetUserTasks(int userid)//(UserProfil)
        {
            return _tasklogic.GetUserTasks(userid);
        }

        public List<Task> GetAllActiveTasks()
        {
            return _tasklogic.GetAllActiveTasks();
        }

        public List<Task> GetAllActiveTasksFromUser(int userid)//(MyProfil)
        {
            return _tasklogic.GetAllActiveTasksFromUser(userid);
        }

        public List<Task> GetAllDoneTasksFromUser(int userid)//(MyProfil)
        {
            return _tasklogic.GetAllDoneTasksFromUser(userid);
        }

        public void UpdateTask(Task task, int taskid, int userid)
        {
            if (TaskValidationHelper.ValidateTitle(task.Title) != null)
            {
                throw new TaskValidationException(TaskValidationHelper.ValidateTitle(task.Title));
            }

            if (TaskValidationHelper.ValidateDeadline(task.Deadline) != null)
            {
                throw new TaskValidationException(TaskValidationHelper.ValidateDeadline(task.Deadline));
            }

            if (TaskValidationHelper.ValidateStatus(task.Status, task.IdTask) != null)
            {
                throw new TaskValidationException(TaskValidationHelper.ValidateStatus(task.Status, task.IdTask));
            }

            _tasklogic.UpdateTask(task, taskid, userid);
        }

        public void DeleteTask(int taskid)
        {
            _tasklogic.DeleteTask(taskid);
        }

    }

    public class TaskValidationException : Exception
    {
        public TaskValidationException()
        {
        }

        public TaskValidationException(string message) : base(message)
        {
        }

    }
}