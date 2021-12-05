using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public interface IRecordLogic
    {
        int CreateRecord(Record record, int userid, int taskid);
        List<Record> GetAllRecords();
        List<Record> GetUserRecords(int userid);
        void UpdateRecord(Record record,int recordid, int taskid, int userid);
        void DeleteRecord(int recordid);
    }
}
