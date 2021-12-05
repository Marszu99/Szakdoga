using System;
using System.Collections.Generic;
using TimeSheet.DataAccess;
using TimeSheet.Model;
using TimeSheet.Model.Extension;

namespace TimeSheet.Logic
{
    public class UserRepository 
    {
        private IUserLogic _userLogic;
     
        public UserRepository(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public int RegisterUser(User user, string password2, string email2, string companyName2)
        {
            if (UserValidationHelper.ValidateUserName(user.Username) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateUserName(user.Username));
            }

            foreach (User _user in GetAllUsers())
            {
                if (user.Username == _user.Username)
                {
                    throw new UserValidationException(user.Username + " already exists!");
                }
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

            return _userLogic.RegisterUser(user, password2, email2, companyName2);
        }

        public int CreateUser(User user)
        {
            if (UserValidationHelper.ValidateUserName(user.Username) != null)
            {
                throw new UserValidationException(UserValidationHelper.ValidateUserName(user.Username));
            }

            foreach (User _user in GetAllUsers())
            {
                if (user.Username == _user.Username)
                {
                    throw new UserValidationException(user.Username + " already exists!");
                }
            }

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

            return _userLogic.CreateUser(user);
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

        public void DeleteUser(int id)
        {
            _userLogic.DeleteUser(id);
        }

        public bool IsValidLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new LoginException("Username is empty!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new LoginException("Password is empty!");
            }

            return _userLogic.IsValidLogin(username, password);
        }

        public User GetUserByUsername(string username)
        {
            return _userLogic.GetUserByUsername(username);
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
