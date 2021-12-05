using System.Linq;
using System.Text.RegularExpressions;

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
        private const int MinimumTelephoneLength = 11;
        private const int MaximumTelephoneLength = 13;

        public static string ValidateUserName(string username)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(username))
            {
                result = "Username is empty!";
            }
            else if (username.Length < MinimumUsernameLength || username.Length > MaximumUsernameLength)
            {
                result = "Username have to reach minimum 6 characters and also can't be more than 45!";
            }
            else if (username.Contains(" "))
            {
                result = "Username needs to be one word!";
            }

            return result;
        }

        public static string ValidatePassword(string password)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password))
            {
                result = "Password is empty!";
            }
            else if (password.Length < MinimumPasswordLength || password.Length > MaximumPasswordLength)
            {
                result = "Password have to reach minimum 6 characters and also can't be more than 45!";
            }
            else if (password.Contains(" "))
            {
                result = "Password needs to be one word!";
            }

            return result;
        }

        public static string ValidatePassword2(string password, string password2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password2))
            {
                result = "Password2 is empty!";
            }
            else if (password2 != password)
            {
                result = "Password2 doesn't match with Password!";
            }

            return result;
        }

        public static string ValidateFirstName(string firstname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(firstname))
            {
                result = "FirstName is empty!";
            }
            else if (firstname.Length < MinimumFirstNameLength || firstname.Length > MaximumFirstNameLength)
            {
                result = "FirstName have to reach minimum 3 characters and also can't be more than 45!";
            }
            else if (!firstname.ToCharArray().All(char.IsLetter))
            {
                result = "FirstName needs to be one word that contains only letters of the alphabet!";
            }

            return result;
        }

        public static string ValidateLastName(string lastname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(lastname))
            {
                result = "LastName is empty!";
            }
            else if (lastname.Length < MinimumLastNameLength || lastname.Length > MaximumLastNameLength)
            {
                result = "LastName have to reach minimum 3 characters and also can't be more than 45!";
            }
            else if (!lastname.ToCharArray().All(char.IsLetter))
            {
                result = "LastName needs to be one word that contains only letters of the alphabet!";
            }

            return result;
        }

        public static string ValidateEmail(string email)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email))
            {
                result = "Email is empty!";
            }
            else if (email.Length < MinimumEmailLength || email.Length > MaximumEmailLength)
            {
                result = "Email have to reach minimum 11 characters and also can't be more than 100!";
            }
            else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                result = "Invalid email!";
            }

            return result;
        }

        public static string ValidateEmail2(string email, string email2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email2))
            {
                result = "Email2 is empty!";
            }
            else if (email2 != email)
            {
                result = "Email2 doesn't match with Email!";
            }

            return result;
        }

        public static string ValidateTelephone(string telephone)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(telephone))
            {
                result = "Telephone is empty!";
            }
            else if (telephone.Length < MinimumTelephoneLength || telephone.Length > MaximumTelephoneLength)
            {
                result = "Telephone number needs to be greater than 10 and less than 14!";
            }
            else if (!telephone.ToCharArray().All(char.IsDigit))// lehet rossz mert elfogadna igy barmilyen kombinaciot pl: 999999999999
            {
                result = "Telephone can only contain numbers!";
            }

            return result;
        }

        public static string ValidateCompanyName(string companyName)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                result = "Company's name is empty!";
            }

            return result;
        }

        public static string ValidateCompanyName2(string companyName, string companyName2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName2))
            {
                result = "Company2's name is empty!";
            }
            else if (companyName2 != companyName)
            {
                result = "Company2 doesn't match with Company!";
            }

            return result;
        }
    }
}
