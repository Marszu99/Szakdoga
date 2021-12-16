using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public interface INotificationLogic
    {
        int CreateNotificationForTask(string message, int taskid);
        string GetTaskNotifications(int taskid);//List<Notification> GetTaskNotifications(int taskid);
        void HasReadNotification(int id);
    }
}
