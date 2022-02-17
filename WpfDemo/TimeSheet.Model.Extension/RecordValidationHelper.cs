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
                result = Resources.NeedToChooseTaskForRecord;
            }

            return result;
        }

        public static string ValidateDate(DateTime? date, DateTime? TaskCreationDate)
        {
            string result = null;

            if (date == null)
            {
                result = Resources.DateIsEmpty;
            }
            else if (date > DateTime.Today)
            {
                result = Resources.DateCantBeInFuture;
            }
            else if (date < TaskCreationDate)
            {
                result = Resources.DateCantBePastTheTaskCreationDate;
            }

            return result;
        }

        public static string ValidateDuration(int duration)
        {
            string result = null;

            if (duration < 0)
            {
                result = Resources.DurationCantBeLowerZero;
            }
            else if (duration > 720)
            {
                result = Resources.DurationCantBeHigher12Hours;
            }

            return result;
        }
    }
}
