using System;

namespace TimeSheet.Model
{
    public class Record
    {
        public int IdRecord { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Duration { get; set; }
        //public string DurationFormatUserProfile { get; set; } // UserProfile miatt
        public Task Task { get; set; }
        public User User { get; set; }
        public int User_idUser { get; set; }// ez igy jo-e??
        public string User_Username { get; set; }// ez igy jo-e??
        public int Task_idTask { get; set; }// ez igy jo-e??
        public string Task_Title { get; set; }// ez igy jo-e??
        public TaskStatus Task_Status { get; set; }// ez igy jo-e??
    }
}
