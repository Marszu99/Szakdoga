using System;

namespace TimeSheet.Model.Extension
{
    public static class RecordValidationHelper
    {
        public static string ValidateDate(DateTime? date, DateTime? TaskCreationDate)
        {
            string result = null;

            if (date == null)
            {
                result = "Date is empty!";
            }
            else if (date > DateTime.Today)
            {
                result = "Record's date can't be in the future!";
            }
            else if (date < TaskCreationDate)
            {
                result = "Record's date can't be earlier than the date when the task was created!";
            }

            return result;
        }

        public static string ValidateDuration(int duration)
        {
            string result = null;

            if (duration < 0)
            {
                result = "Duration can't be lower than 0!";
            }
            else if (duration > 720)
            {
                result = "Duration can't be higher than 12 hours!";
            }

            return result;
        }
    }
}
