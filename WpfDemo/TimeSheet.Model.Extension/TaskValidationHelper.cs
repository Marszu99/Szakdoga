using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;

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
                result = "Need to choose a User for the task!";//ResourceHandler.GetResourceString("NeedToChooseUserForTask");
            }

            return result;
        }

        public static string ValidateTitle(string title)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(title))
            {
                result = "Title is empty!";//ResourceHandler.GetResourceString("TitleIsEmpty");
            }
            else if (title.Length > MaximumTitleLength)
            {
                result = "Title can't be more than 45 characters!";//ResourceHandler.GetResourceString("TitleWrongLength");
            }

            return result;
        }

        public static string ValidateDeadline(DateTime? deadline)
        {
            string result = null;

            if (deadline == null)
            {
                result = "Deadline is empty!";//ResourceHandler.GetResourceString("DeadlineIsEmpty");
            }
            else if (deadline <= DateTime.Today)
            {
                result = "Deadline has to be atleast more than 24 hours away from the current time!";//ResourceHandler.GetResourceString("DeadlineMoreThan24Hours");
            }

            return result;
        }

        public static string ValidateStatus(TaskStatus status, int taskid)
        {
            string result = null;
            List<Record> recordList = new RecordLogic().GetTaskRecords(taskid);

            if (status == TaskStatus.Done && recordList.Count == 0)
            {
                result = "Status cannot be Done when there is no recods for the task!";//ResourceHandler.GetResourceString("StatusCannotBeDoneWithNoRecords");
            }
            else if (status == TaskStatus.Created && recordList.Count != 0)
            {
                result = "Status cannot be Created when there is recods for the task!";//ResourceHandler.GetResourceString("StatusCannotBeCreatedWithRecords");
            }

            return result;
        }
    }
}