using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;

namespace TimeSheet.Logic
{
    public class UserRepository
    {
        private IUserLogic _userLogic;
        
        public UserRepository(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public int RegisterAdmin(User user, string password2, string email2,string companyName, string companyName2)
        {
            if (UserValidationHelper.ValidateUserName(user.Username) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateUserName(user.Username));
            }

            if (UserValidationHelper.ValidatePassword(user.Password) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidatePassword(user.Password));
            }

            if (UserValidationHelper.ValidatePassword2(user.Password, password2) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidatePassword(user.Password));
            }

            if (UserValidationHelper.ValidateFirstName(user.FirstName) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateFirstName(user.FirstName));
            }

            if (UserValidationHelper.ValidateLastName(user.LastName) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateLastName(user.LastName));
            }

            if (UserValidationHelper.ValidateEmail(user.Email) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateEmail(user.Email));
            }

            if (UserValidationHelper.ValidateEmail2(user.Email, email2) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateEmail(user.Email));
            }

            if (UserValidationHelper.ValidateTelephone(user.Telephone) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateTelephone(user.Telephone));
            }

            if (UserValidationHelper.ValidateCompanyName(companyName) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateCompanyName(companyName));
            }

            if (UserValidationHelper.ValidateCompanyName2(companyName, companyName2) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateCompanyName2(companyName, companyName2));
            }

            return _userLogic.RegisterAdmin(user, password2, email2, companyName, companyName2);
        }

        public int CreateUser(User user, string createdUserRandomPassword)
        {
            if (UserValidationHelper.ValidateUserName(user.Username) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateUserName(user.Username));
            }

            foreach (User _user in GetAllUsers())
            {
                if (user.Username == _user.Username)
                {
                    throw new UserValidationException(user.Username + Resources.UsernameAlreadyExists);
                }
            }

            if (UserValidationHelper.ValidateEmail(user.Email) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateEmail(user.Email));
            }
            return _userLogic.CreateUser(user, createdUserRandomPassword);
        }

        public List<User> GetAllUsers()
        {
            return _userLogic.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            if (UserValidationHelper.ValidatePassword(user.Password) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidatePassword(user.Password));
            }


            if (UserValidationHelper.ValidateFirstName(user.FirstName) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateFirstName(user.FirstName));
            }


            if (UserValidationHelper.ValidateLastName(user.LastName) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateLastName(user.LastName));
            }


            if (UserValidationHelper.ValidateEmail(user.Email) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateEmail(user.Email));
            }


            if (UserValidationHelper.ValidateTelephone(user.Telephone) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateTelephone(user.Telephone));
            }

            _userLogic.UpdateUser(user);
        }

        public void DeleteUser(int id, int status)
        {
            _userLogic.DeleteUser(id, status);
        }

        public bool IsValidLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new LoginException(Resources.UsernameIsEmpty);
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new LoginException(Resources.PasswordIsEmpty);
            }

            return _userLogic.IsValidLogin(username, password);
        }

        public User GetUserByUsername(string username)
        {
            return _userLogic.GetUserByUsername(username);
        }

        public User GetUserByID(int userid)
        {
            return _userLogic.GetUserByID(userid);
        }

        public User GetAdmin()
        {
            return _userLogic.GetAdmin();
        }
    }

    

    public class UserValidationException : Exception
    {
        public UserValidationException()
        {
        }

        public UserValidationException(string message) : base(message)
        {
        }
    }
    public class LoginUserException : Exception
    {
        public LoginUserException()
        {
        }

        public LoginUserException(string message) : base(message)
        {
        }
    }
    public class LoginException : Exception
    {
        public LoginException()
        {
        }

        public LoginException(string message) : base(message)
        {
        }
    }

}
