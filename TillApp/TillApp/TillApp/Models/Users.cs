using System;
using System.Collections.Generic;
using System.Text;

namespace TillApp.Model
{
    class Users
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public Users() { }


        public Users(string UserId, string UserName)
        {
            this.UserId = UserId;
            this.UserName = UserName;
        }   // Users()

        public Users(string UserId)
        {
            this.UserId = UserId;
        }

        public static Users getUsers()
        {
            var NeilB = new Users();
            {
                NeilB.UserId = "999";
                NeilB.UserName = "Neil Byrne";
            };
            return NeilB;
        }







    }   // Users class
}   // namespace
