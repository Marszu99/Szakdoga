using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Model;

namespace TimeSheet.Logic
{
    public class NotificationRepository
    {
        private INotificationLogic _notificationlogic;

        public NotificationRepository(INotificationLogic notificationlogic)
        {
            _notificationlogic = notificationlogic;
        }

        public int CreateNotificationForTask(string message, int taskid)
        {          
            return _notificationlogic.CreateNotificationForTask(message, taskid);
        }

        public string GetTaskNotifications(int taskid)//public List<Notification> GetTaskNotifications(int taskid)

        {
            return _notificationlogic.GetTaskNotifications(taskid);
        }

        public void HasReadNotification(int taskid)
        {
            _notificationlogic.HasReadNotification(taskid);
        }
    }
}
