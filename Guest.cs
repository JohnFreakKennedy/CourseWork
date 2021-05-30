using System;


namespace HotelLib
{
    public class Guest:User
    {
        private int _passportID;
        private DateTime _birthDate;
        public Guest(string name,string login, string password, int passportID,DateTime birthDate)
        {
            _name = name;
            _login = login;
            _password = password;
            _passportID = passportID;
            _birthDate = birthDate;
        }
    }
}
