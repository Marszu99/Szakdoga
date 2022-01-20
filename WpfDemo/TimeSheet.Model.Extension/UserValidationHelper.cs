using PhoneNumbers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using TimeSheet.DataAccess;

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
        private const int MinimumCompanyNameLength = 10;
        private const int MaximumCompanyNameLength = 60;

        public static string ValidateUserName(string username)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(username))
            {
                result = "Username is empty!";//ResourceHandler.GetResourceString("UsernameIsEmpty");
            }
            else if (username.Length < MinimumUsernameLength || username.Length > MaximumUsernameLength)
            {
                result = "Username have to reach minimum 6 characters and also can't be more than 45!";//ResourceHandler.GetResourceString("UsernameWrongLength")
            }
            else if (username.Contains(" "))
            {
                result = "Username needs to be one word!";//ResourceHandler.GetResourceString("UsernameOneWord")
            }

            foreach (User _user in new UserLogic().GetAllUsers())
            {
                if (username == _user.Username)
                {
                    result = username + " already exists!";//ResourceHandler.GetResourceString("UsernameAlreadyExists")
                }
            }

            return result;
        }

        public static string ValidatePassword(string password)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password))
            {
                result = "Password is empty!";//ResourceHandler.GetResourceString("PasswordIsEmpty")
            }
            else if (password.Length < MinimumPasswordLength || password.Length > MaximumPasswordLength)
            {
                result = "Password have to reach minimum 6 characters and also can't be more than 45!";//ResourceHandler.GetResourceString("PasswordWrongLength")
            }
            else if (password.Contains(" "))
            {
                result = "Password needs to be one word!";//ResourceHandler.GetResourceString("PasswordOneWord")
            }

            return result;
        }

        public static string ValidatePassword2(string password, string password2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(password2))
            {
                result = "Password2 is empty!";//ResourceHandler.GetResourceString("Password2IsEmpty")
            }
            else if (password2 != password)
            {
                result = "Password2 doesn't match with Password!";//ResourceHandler.GetResourceString("Password2DoesntMatch")
            }

            return result;
        }

        public static string ValidateFirstName(string firstname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(firstname))
            {
                result = "FirstName is empty!";//ResourceHandler.GetResourceString("FirstNameIsEmpty")
            }
            else if (firstname.Length < MinimumFirstNameLength || firstname.Length > MaximumFirstNameLength)
            {
                result = "FirstName have to reach minimum 3 characters and also can't be more than 45!";//ResourceHandler.GetResourceString("FirstNameWrongLength")
            }
            else if (!firstname.ToCharArray().All(char.IsLetter))
            {
                result = "FirstName needs to be one word that contains only letters of the alphabet!";//ResourceHandler.GetResourceString("FirstNameNoNumbers")
            }

            return result;
        }

        public static string ValidateLastName(string lastname)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(lastname))
            {
                result = "LastName is empty!";//ResourceHandler.GetResourceString("LastNameIsEmpty")
            }
            else if (lastname.Length < MinimumLastNameLength || lastname.Length > MaximumLastNameLength)
            {
                result = "LastName have to reach minimum 3 characters and also can't be more than 45!";//ResourceHandler.GetResourceString("LastNameWrongLength")
            }
            else if (!lastname.ToCharArray().All(char.IsLetter))
            {
                result = "LastName needs to be one word that contains only letters of the alphabet!";//ResourceHandler.GetResourceString("LastNameNoNumbers")
            }

            return result;
        }

        public static string ValidateEmail(string email)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email))
            {
                result = "Email is empty!"; //ResourceHandler.GetResourceString("EmailIsEmpty")
            }
            else if (email.Length < MinimumEmailLength || email.Length > MaximumEmailLength)
            {
                result = "Email have to reach minimum 11 characters and also can't be more than 100!"; //ResourceHandler.GetResourceString("EmailWrongLength")
            }
            else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                result = "Invalid email!"; //ResourceHandler.GetResourceString("EmailIsInvalid")
            }

            return result;
        }

        public static string ValidateEmail2(string email, string email2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(email2))
            {
                result = "Email2 is empty!";//ResourceHandler.GetResourceString("Email2IsEmpty")
            }
            else if (email2 != email)
            {
                result = "Email2 doesn't match with Email!";//ResourceHandler.GetResourceString("Email2DoesntMatch")
            }

            return result;
        }

        public static string ValidateTelephone(string telephone)
        {
            string result = null;

            string telephoneNumber = null;
            foreach (char telephoneCharacter in telephone)
            {
                if (Char.IsDigit(telephoneCharacter))
                {
                    telephoneNumber += telephoneCharacter;
                }
            }

            if (string.IsNullOrWhiteSpace(telephone))
            {
                result = "Telephone is empty!";//ResourceHandler.GetResourceString("TelephoneIsEmpty")
            }

            else if (telephoneNumber.Length < MinimumTelephoneLength || telephoneNumber.Length > MaximumTelephoneLength)
            {
                result = "Telephone number needs to be greater than 10 and less than 14!";//ResourceHandler.GetResourceString("TelephoneWrongLength")
            }
            else if (!PhoneNumberUtil.IsViablePhoneNumber(telephone))//jo ez vagy specifikaljak magyar telefonszamokra mert igy elfogana a 999999999999-et?
            {
                result = "Invalid telephone number!";//ResourceHandler.GetResourceString("TelephoneIsInvalid")
            }

            return result;
        }

        public static string ValidateCompanyName(string companyName)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                result = "Company's name is empty!";//ResourceHandler.GetResourceString("CompanyNameIsEmpty")
            }
            else if (companyName.Length < MinimumCompanyNameLength || companyName.Length > MaximumCompanyNameLength)
            {
                result = "Company's name have to reach minimum 10 characters and also can't be more than 60!";//ResourceHandler.GetResourceString("CompanyNameWrongLength")
            }

            return result;
        }

        public static string ValidateCompanyName2(string companyName, string companyName2)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(companyName2))
            {
                result = "Company2's name is empty!";//ResourceHandler.GetResourceString("CompanyName2IsEmpty")
            }
            else if (companyName2 != companyName)
            {
                result = "Company2 doesn't match with Company!";//ResourceHandler.GetResourceString("CompanyName2DoesntMatch")
            }

            return result;
        }
    }
}
