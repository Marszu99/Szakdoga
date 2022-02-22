using System;

namespace TimeSheet.Model
{
    public class Task
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime Deadline { get; set; }
        public int User_idUser { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
