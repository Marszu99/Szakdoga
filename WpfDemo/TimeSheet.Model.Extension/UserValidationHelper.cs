using System.Data.SqlClient;
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
        //private const int MinimumEmailLength = 11;
        private const int MaximumEmailLength = 100;
        private const int MinimumCompanyNameLength = 10;
        private const int MaximumCompanyNameLength = 60;

        public static string ValidateUserName(string username)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(username))
            {
                result = Resources.UsernameIsEmpty;
            }
            else if (username.Length < MinimumUsernameLength || username.Length > MaximumUsernameLength)
            {
                result = Resources.UsernameWrongLength;
            }
            else if (username.Contains(" "))
            {
                result = Resources.UsernameOneWord;
            }
            else
            {
                try
                {
                    foreach (User user in new UserLogic().GetAllUsers())
                    {
                        if (username == user.Username)
                        {
                            result = username + Resources.UsernameAlreadyExists;
                        }
                    }
                }
                catch (SqlException)
                {

                }
            }

            return result;
        }

        public static string ValidatePassword(string password)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password))
            {
                result = Resources.PasswordIsEmpty;
            }
            else if (password.Length < MinimumPasswordLength || password.Length > MaximumPasswordLength)
            {
                result = Resources.PasswordWrongLength;
            }
            else if (password.Contains(" "))
            {
                result = Resources.PasswordOneWord;
            }

            return result;
        }

        public static string ValidatePassword2(string password, string password2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password2))
            {
                result = Resources.Password2IsEmpty;
            }
            else if (password2 != password)
            {
                result = Resources.Password2DoesntMatch;
            }

            return result;
        }

        public static string ValidateFirstName(string firstname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(firstname))
            {
                result = Resources.FirstNameIsEmpty;
            }
            else if (firstname.Length < MinimumFirstNameLength || firstname.Length > MaximumFirstNameLength)
            {
                result = Resources.FirstNameWrongLength;
            }
            else if (!firstname.ToCharArray().All(char.IsLetter))
            {
                result = Resources.FirstNameNoNumbers;
            }

            return result;
        }

        public static string ValidateLastName(string lastname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(lastname))
            {
                result = Resources.LastNameIsEmpty;
            }
            else if (lastname.Length < MinimumLastNameLength || lastname.Length > MaximumLastNameLength)
            {
                result = Resources.LastNameWrongLength;
            }
            else if (!lastname.ToCharArray().All(char.IsLetter))
            {
                result = Resources.LastNameNoNumbers;
            }

            return result;
        }

        public static string ValidateEmail(string email)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email))
            {
                result = Resources.EmailIsEmpty;
            }
            else if (email.Length > MaximumEmailLength) // (email.Length < MinimumEmailLength || email.Length > MaximumEmailLength) 
            {
                result = Resources.EmailWrongLength;
            }
            else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                result = Resources.EmailIsInvalid;
            }

            return result;
        }

        public static string ValidateEmail2(string email, string email2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email2))
            {
                result = Resources.Email2IsEmpty;
            }
            else if (email2 != email)
            {
                result = Resources.Email2DoesntMatch;
            }

            return result;
        }

        public static string ValidateTelephone(string telephone)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(telephone))
            {
                result = Resources.TelephoneIsEmpty;
            }
            /*else if (!PhoneNumberUtil.IsViablePhoneNumber(telephone))
            {
                result = Resources.TelephoneIsInvalid");
            }*/
            else if (!Regex.IsMatch(telephone, @"^((?:\+?3|0)6)(?:-|\()?(\d{1,2})(?:-|\))?(\d{3})-?(\d{3,4})$"))    // magyar telefonszamra specifikalt
            {
                result = Resources.TelephoneIsInvalid;
            }

            return result;
        }

        public static string ValidateCompanyName(string companyName)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                result = Resources.CompanyNameIsEmpty;
            }
            else if (companyName.Length < MinimumCompanyNameLength || companyName.Length > MaximumCompanyNameLength)
            {
                result = Resources.CompanyNameWrongLength;
            }

            return result;
        }

        public static string ValidateCompanyName2(string companyName, string companyName2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName2))
            {
                result = Resources.CompanyName2IsEmpty;
            }
            else if (companyName2 != companyName)
            {
                result = Resources.CompanyName2DoesntMatch;
            }

            return result;
        }
    }
}
