using System;

namespace TimeSheet.Model
{
    public class Record
    {
        public int IdRecord { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Duration { get; set; }
        public Task Task { get; set; }// nem kell
        public User User { get; set; }// nem kell
        public int User_idUser { get; set; }// kell
        public string User_Username { get; set; }// nem kell
        public int Task_idTask { get; set; }// kell
        public string Task_Title { get; set; }// nem kell
        public TaskStatus Task_Status { get; set; }// nem kell
    }
}
