using System;

namespace TimeSheet.Model.Extension
{
    public static class TaskValidationHelper
    {
        private const int MaximumTitleLength = 45;

        public static string ValidateTitle(string title)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(title))
            {
                result = "Title is empty!";
            }
            else if (title.Length > MaximumTitleLength)
            {
                result = "Title can't be more than 45 characters!";
            }

            return result;
        }

        public static string ValidateDeadline(DateTime? deadline)
        {
            string result = null;

            if (deadline == null)
            {
                result = "Deadline is empty!";
            }
            else if (deadline <= DateTime.Today)
            {
                result = "Deadline has to be atleast more than 24 hours away from the current time!";
            }

            return result;
        }
    }
}
