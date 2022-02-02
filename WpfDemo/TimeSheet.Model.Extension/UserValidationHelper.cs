using System.Linq;
using System.Text.RegularExpressions;
using TimeSheet.DataAccess;
using TimeSheet.Resource;

namespace TimeSheet.Model.Extension
{
    public static class UserValidationHelper
    {
        private const int MinimumUsernameLength = 6;
        private const int MaximumUsernameLength = 45;
        private const int MinimumPasswordLength = 6;
        private const int MaximumPasswordLength = 45;
        private const int MinimumFirstNameLength = 3;
        private const int MaximumFirstNameLength = 45;
        private const int MinimumLastNameLength = 3;
        private const int MaximumLastNameLength = 45;
        private const int MinimumEmailLength = 11;
        private const int MaximumEmailLength = 100;
        private const int MinimumCompanyNameLength = 10;
        private const int MaximumCompanyNameLength = 60;

        public static string ValidateUserName(string username)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(username))
            {             
                result = ResourceHandler.GetResourceString("UsernameIsEmpty");
            }
            else if (username.Length < MinimumUsernameLength || username.Length > MaximumUsernameLength)
            {
                result = ResourceHandler.GetResourceString("UsernameWrongLength");
            }
            else if (username.Contains(" "))
            {
                result = ResourceHandler.GetResourceString("UsernameOneWord");
            }

            foreach (User _user in new UserLogic().GetAllUsers())
            {
                if (username == _user.Username)
                {
                    result = username + ResourceHandler.GetResourceString("UsernameAlreadyExists");
                }
            }

            return result;
        }

        public static string ValidatePassword(string password)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password))
            {
                result = ResourceHandler.GetResourceString("PasswordIsEmpty");
            }
            else if (password.Length < MinimumPasswordLength || password.Length > MaximumPasswordLength)
            {
                result = ResourceHandler.GetResourceString("PasswordWrongLength");
            }
            else if (password.Contains(" "))
            {
                result = ResourceHandler.GetResourceString("PasswordOneWord");
            }

            return result;
        }

        public static string ValidatePassword2(string password, string password2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password2))
            {
                result = ResourceHandler.GetResourceString("Password2IsEmpty");
            }
            else if (password2 != password)
            {
                result = ResourceHandler.GetResourceString("Password2DoesntMatch");
            }

            return result;
        }

        public static string ValidateFirstName(string firstname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(firstname))
            {
                result = ResourceHandler.GetResourceString("FirstNameIsEmpty");
            }
            else if (firstname.Length < MinimumFirstNameLength || firstname.Length > MaximumFirstNameLength)
            {
                result = ResourceHandler.GetResourceString("FirstNameWrongLength");
            }
            else if (!firstname.ToCharArray().All(char.IsLetter))
            {
                result = ResourceHandler.GetResourceString("FirstNameNoNumbers");
            }

            return result;
        }

        public static string ValidateLastName(string lastname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(lastname))
            {
                result = ResourceHandler.GetResourceString("LastNameIsEmpty");
            }
            else if (lastname.Length < MinimumLastNameLength || lastname.Length > MaximumLastNameLength)
            {
                result = ResourceHandler.GetResourceString("LastNameWrongLength");
            }
            else if (!lastname.ToCharArray().All(char.IsLetter))
            {
                result = ResourceHandler.GetResourceString("LastNameNoNumbers");
            }

            return result;
        }

        public static string ValidateEmail(string email)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email))
            {
                result = ResourceHandler.GetResourceString("EmailIsEmpty");
            }
            else if (email.Length < MinimumEmailLength || email.Length > MaximumEmailLength)
            {
                result = ResourceHandler.GetResourceString("EmailWrongLength");
            }
            else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                result = ResourceHandler.GetResourceString("EmailIsInvalid");
            }

            return result;
        }

        public static string ValidateEmail2(string email, string email2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email2))
            {
                result = ResourceHandler.GetResourceString("Email2IsEmpty");
            }
            else if (email2 != email)
            {
                result = ResourceHandler.GetResourceString("Email2DoesntMatch");
            }

            return result;
        }

        public static string ValidateTelephone(string telephone)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(telephone))
            {
                result = ResourceHandler.GetResourceString("TelephoneIsEmpty");
            }
            /*else if (!PhoneNumberUtil.IsViablePhoneNumber(telephone))
            {
                result = ResourceHandler.GetResourceString("TelephoneIsInvalid");
            }*/
            else if (!Regex.IsMatch(telephone, @"^((?:\+?3|0)6)(?:-|\()?(\d{1,2})(?:-|\))?(\d{3})-?(\d{3,4})$"))    // magyar telefonszamra specifikalt
            {
                result = ResourceHandler.GetResourceString("TelephoneIsInvalid");
            }

            return result;
        }

        public static string ValidateCompanyName(string companyName)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                result = ResourceHandler.GetResourceString("CompanyNameIsEmpty");
            }
            else if (companyName.Length < MinimumCompanyNameLength || companyName.Length > MaximumCompanyNameLength)
            {
                result = ResourceHandler.GetResourceString("CompanyNameWrongLength");
            }

            return result;
        }

        public static string ValidateCompanyName2(string companyName, string companyName2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName2))
            {
                result = ResourceHandler.GetResourceString("CompanyName2IsEmpty");
            }
            else if (companyName2 != companyName)
            {
                result = ResourceHandler.GetResourceString("CompanyName2DoesntMatch");
            }

            return result;
        }
    }
}
