using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Resource;

namespace TimeSheet.Model.Extension
{
    public static class TaskValidationHelper
    {
        private const int MaximumTitleLength = 45;

        public static string ValidateUser(User user)
        {
            string result = null;

            if (user == null)
            {
                result = Resources.NeedToChooseUserForTask;
            }

            return result;
        }

        public static string ValidateTitle(string title)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(title))
            {
                result = Resources.TitleIsEmpty;
            }
            else if (title.Length > MaximumTitleLength)
            {
                result = Resources.TitleWrongLength;
            }

            return result;
        }

        public static string ValidateDeadline(DateTime? deadline)
        {
            string result = null;

            if (deadline == null)
            {
                result = Resources.DeadlineIsEmpty;
            }
            else if (deadline <= DateTime.Today)
            {
                result = Resources.DeadlineMoreThan24Hours;
            }

            return result;
        }

        public static string ValidateStatus(TaskStatus status, int taskid)
        {
            string result = null;
            List<Record> recordList = new RecordLogic().GetTaskRecords(taskid);

            if (status == TaskStatus.Done && recordList.Count == 0)
            {
                result = Resources.StatusCannotBeDoneWithNoRecords;
            }
            else if (status == TaskStatus.Created && recordList.Count != 0)
            {
                result = Resources.StatusCannotBeCreatedWithRecords;
            }

            return result;
        }
    }
}