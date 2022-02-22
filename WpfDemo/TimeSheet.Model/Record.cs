using System;

namespace TimeSheet.Model
{
    public class Record
    {
        public int IdRecord { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Duration { get; set; }
        public int User_idUser { get; set; }
        public int Task_idTask { get; set; }
    }
}
