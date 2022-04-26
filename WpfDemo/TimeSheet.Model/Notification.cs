namespace TimeSheet.Model
{
    public class Notification
    {
        public int IdNotification { get; set; }
        public string Message { get; set; }
        public int NotificationFor { get; set; }
        public int Task_idTask { get; set; }
    }
}
