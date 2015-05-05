using System;

namespace KavaAPI
{
    public interface IUserManager
    {
        void LoginUser(string username, string password);
        void LogoffUser(string username);
        User GetUserByUserName(string username);
        User GetUserByPkey(Guid pkey);
    }
}