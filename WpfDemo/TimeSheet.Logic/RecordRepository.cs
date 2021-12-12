using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Model;
using TimeSheet.Model.Extension;

namespace TimeSheet.Logic
{
    public class RecordRepository
    {
        private IRecordLogic _recordlogic;

        public RecordRepository(IRecordLogic recordlogic)
        {
            _recordlogic = recordlogic;
        }
        public int CreateRecord(Record record, int userid, int taskid)
        {
            if (RecordValidationHelper.ValidateDate(record.Date, record.Task.CreationDate) != null)
            {
                throw new RecordValidationException(RecordValidationHelper.ValidateDate(record.Date, record.Task.CreationDate));
            }

            if (RecordValidationHelper.ValidateDuration(record.Duration) != null)
            {
                throw new RecordValidationException(RecordValidationHelper.ValidateDuration(record.Duration));
            }

            return _recordlogic.CreateRecord(record, userid, taskid);
        }

        public List<Record> GetAllRecords()
        {
            return _recordlogic.GetAllRecords();
        }

        public List<Record> GetUserRecords(int userid)//(UserProfil)
        {
            return _recordlogic.GetUserRecords(userid);
        }
        public List<Record> GetTaskRecords(int taskid)//(TaskValidation.ValidateStatus miatt)
        {
            return _recordlogic.GetTaskRecords(taskid);
        }

        public void UpdateRecord(Record record, int recordid, int taskid, int userid)
        {
            if (RecordValidationHelper.ValidateDate(record.Date, record.Task.CreationDate) != null)
            {
                throw new RecordValidationException(RecordValidationHelper.ValidateDate(record.Date, record.Task.CreationDate));
            }

            if (RecordValidationHelper.ValidateDuration(record.Duration) != null)
            {
                throw new RecordValidationException(RecordValidationHelper.ValidateDuration(record.Duration));
            }

            _recordlogic.UpdateRecord(record, recordid, taskid, userid);
        }

        public void DeleteRecord(int recordid)
        {
            _recordlogic.DeleteRecord(recordid);
        }
    }

    public class RecordValidationException : Exception
    {
        public RecordValidationException()
        {
        }

        public RecordValidationException(string message) : base(message)
        {
        }

    }
}
