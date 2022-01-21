using System;
using TimeSheet.Resource;

namespace TimeSheet.Model.Extension
{
    public static class RecordValidationHelper
    {
        public static string ValidateTask(Task task)
        {
            string result = null;

            if (task == null)
            {
                result = ResourceHandler.GetResourceString("NeedToChooseTaskForRecord");
            }

            return result;
        }

        public static string ValidateDate(DateTime? date, DateTime? TaskCreationDate)
        {
            string result = null;

            if (date == null)
            {
                result = ResourceHandler.GetResourceString("DateIsEmpty");
            }
            else if (date > DateTime.Today)
            {
                result = ResourceHandler.GetResourceString("DateCantBeInFuture");
            }
            else if (date < TaskCreationDate)
            {
                result = ResourceHandler.GetResourceString("DateCantBePastTheTaskCreationDate");
            }

            return result;
        }

        public static string ValidateDuration(int duration)
        {
            string result = null;

            if (duration < 0)
            {
                result = ResourceHandler.GetResourceString("DurationCantBeLowerZero");
            }
            else if (duration > 720)
            {
                result = ResourceHandler.GetResourceString("DurationCantBeHigher12Hours");
            }

            return result;
        }
    }
}
