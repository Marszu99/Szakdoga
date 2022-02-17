using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public interface INotificationLogic
    {
        int CreateNotificationForTask(string message, int notificationFor, int taskid);
        //string GetTaskNotificationsForEmployee(int taskid);
        //string GetTaskNotificationsForAdmin(int taskid);
        List<string> GetTaskNotificationsForEmployee(int taskid);
        List<string> GetTaskNotificationsForAdmin(int taskid);
        void HasReadNotification(int id, int notificationFor);
    }
}
