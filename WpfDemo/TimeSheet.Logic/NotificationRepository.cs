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

        public int CreateNotificationForTask(string message, int notificationFor, int taskid)
        {          
            return _notificationlogic.CreateNotificationForTask(message, notificationFor, taskid);
        }

        public string GetTaskNotifications(int taskid)//public List<Notification> GetTaskNotifications(int taskid)

        {
            return _notificationlogic.GetTaskNotifications(taskid);
        }

        public string GetTaskNotificationsForEmployee(int taskid)

        {
            return _notificationlogic.GetTaskNotificationsForEmployee(taskid);
        }

        public string GetTaskNotificationsForAdmin(int taskid)

        {
            return _notificationlogic.GetTaskNotificationsForAdmin(taskid);
        }

        public void HasReadNotification(int taskid, int notificationFor)
        {
            _notificationlogic.HasReadNotification(taskid, notificationFor);
        }
    }
}
