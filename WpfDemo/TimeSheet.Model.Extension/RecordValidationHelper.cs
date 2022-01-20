using System;

namespace TimeSheet.Model.Extension
{
    public static class RecordValidationHelper
    {
        public static string ValidateTask(Task task)
        {
            string result = null;

            if (task == null)
            {
                result = "Need to choose a Task for the record!";//ResourceHandler.GetResourceString("NeedToChooseTaskForRecord");
            }

            return result;
        }

        public static string ValidateDate(DateTime? date, DateTime? TaskCreationDate)
        {
            string result = null;

            if (date == null)
            {
                result = "Date is empty!";//ResourceHandler.GetResourceString("DateIsEmpty");
            }
            else if (date > DateTime.Today)
            {
                result = "Record's date can't be in the future!";//ResourceHandler.GetResourceString("DateCantBeInFuture");
            }
            else if (date < TaskCreationDate)
            {
                result = "Record's date can't be earlier than the date when the task was created!";//ResourceHandler.GetResourceString("DateCantBePastTheTaskCreationDate");
            }

            return result;
        }

        public static string ValidateDuration(int duration)
        {
            string result = null;

            if (duration < 0)
            {
                result = "Duration can't be lower than 0!";//ResourceHandler.GetResourceString("DurationCantBeLowerZero");
            }
            else if (duration > 720)
            {
                result = "Duration can't be higher than 12 hours!";//ResourceHandler.GetResourceString("DurationCantBeHigher12Hours");
            }

            return result;
        }
    }
}
