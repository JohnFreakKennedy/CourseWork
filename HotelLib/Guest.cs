using System;


namespace HotelLib
{
    public class Guest : User
    {
        private static uint GuestIDcounter = 0;
        public uint PassportID { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Guest(string name, string login, string password, uint passportID, DateTime birthDate)
        {
            if (!Char.IsUpper(name[0])) throw new UserException(this);
            int i = 0;
            for (int j = 0; j < name.Length; j++)
            {
                if (Char.IsDigit(name[j])) throw new UserException(this);
                if (Char.IsWhiteSpace(name[j])) i = j + 1;
            }
            if (!Char.IsUpper(name[i])) throw new UserException(this);
            Name = name;
            Login = login;
            Password = password;
            PassportID = passportID;
            BirthDate = birthDate;
            UserID = GuestIDcounter;
            try
            {
                BookingHandlerSingleton.Instance.TryAddGuestToDB(this);
            }

            catch (UserException exception)
            {
                throw exception;
            }
            GuestIDcounter++;
        }

        public void ChangeGuestData(string name, string login, string password, uint passportID, DateTime birthDate)
        {
            Name = name;
            Login = login;
            Password = password;
            PassportID = passportID;
            BirthDate = birthDate;
        }
        public string GetPassID()
        {
            return PassportID.ToString("D9");
        }
    }
}