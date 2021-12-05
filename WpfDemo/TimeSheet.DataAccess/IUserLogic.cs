﻿using System.Collections.Generic;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public interface IUserLogic
    {
        int RegisterUser(User user, string password2, string email2, string companyName2);
        int CreateUser(User user);
        List<User> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool IsValidLogin(string username, string password);
        User GetUserByUsername(string username);
    }
}