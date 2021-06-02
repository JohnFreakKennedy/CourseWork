﻿using System;
using System.Collections.Generic;


namespace HotelLib
{
    class Program
    {
        static void Main()
        {
            List<Suite> suites = new List<Suite>();
            Hotel California = HotelFillingSimulation();
            HandlerFillingSimulation(California);
            PrintMainMenu();
            string choiceres = String.Empty;
            try
            {
                choiceres=EternalEnter();
            }
            catch(FormatException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
                EternalEnter();
            }
            uint UserID = 0;
            string UserIDstr = "";
            Guest guest = null;
            Admin admin = null;
            switch(choiceres[0])
            {
                case ('1'):
                    for (int i = 1; i < choiceres.Length; i++)
                    {
                        UserIDstr = UserIDstr + choiceres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    guest = BookingHandlerSingleton.Instance.GetGuestByID(UserID);
                    break;
                case ('2'):
                    for (int i = 1; i < choiceres.Length; i++)
                    {
                        UserIDstr = UserIDstr + choiceres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    admin = BookingHandlerSingleton.Instance.GetAdminByID(UserID);
                    break;
                case ('3'):
                    for (int i = 1; i < choiceres.Length; i++)
                    {
                        UserIDstr = UserIDstr + choiceres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    guest = BookingHandlerSingleton.Instance.GetGuestByID(UserID);
                    break;
            }
            if (admin == null)
            {
                try
                {
                    choiceres = EternalGuestMenu();
                    switch (choiceres)
                    {
                        case ("2"):
                            string res = GuestBooking(guest);
                            if (res == " ") EternalGuestMenu();
                            break;
                        case ("3"):
                            string resExt = GuestBooking(guest);
                            if (resExt == " ") EternalGuestMenu();
                            break;
                        case ("4"):
                            BookingHandlerSingleton.Instance.ChangeDate();
                            Console.WriteLine("Today is " + BookingHandlerSingleton.Instance.CurrentDate);
                            EternalGuestMenu();
                            break;
                        default:
                            Console.WriteLine("Wrong input, returning to guest menu...");
                            EternalGuestMenu();
                            break;
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    EternalGuestMenu();
                    switch (choiceres)
                    {
                        case ("2"):
                            string res = GuestBooking(guest);
                            if (res == " ") EternalGuestMenu();
                            break;
                        case ("3"):
                            string resExt = GuestBooking(guest);
                            if (resExt == " ") EternalGuestMenu();
                            break;
                        case ("4"):
                            BookingHandlerSingleton.Instance.ChangeDate();
                            Console.WriteLine("Today is " + BookingHandlerSingleton.Instance.CurrentDate);
                            EternalGuestMenu();
                            break;
                        default:
                            Console.WriteLine("Wrong input, returning to guest menu...");
                            EternalGuestMenu();
                            break;
                    }
                }
            }
            if (guest == null)
            {
                try
                {
                    choiceres = EternalAdminMenu();
                    switch (choiceres)
                    {
                        case ("2"):
                            ShowHotelSuitesInfo(California);
                            EternalAdminMenu();
                            break;
                        case ("3"):
                            string res3 = EternalAdminMenu();
                            if (res3 == "3") EternalAdminMenu();
                            break;
                        case ("4"):
                            string res4 = EternalAdminMenu();
                            BookingHandlerSingleton.Instance.ChangeDate();
                            Console.WriteLine("Today is " + BookingHandlerSingleton.Instance.CurrentDate);
                            if (res4 == "4") EternalAdminMenu();
                            break;
                        default:
                            Console.WriteLine("Wrong input, returning to admin menu...");
                            EternalAdminMenu();
                            break;
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    EternalGuestMenu();
                    switch (choiceres)
                    {
                        case ("2"):
                            string res = GuestBooking(guest);
                            if (res == " ") EternalGuestMenu();
                            break;
                        case ("3"):
                            string resExt = GuestBooking(guest);
                            if (resExt == "3") EternalAdminMenu();
                            break;
                        case ("4"):
                            BookingHandlerSingleton.Instance.ChangeDate();
                            Console.WriteLine("Today is " + BookingHandlerSingleton.Instance.CurrentDate);
                            EternalAdminMenu();
                            break;
                        default:
                            Console.WriteLine("Wrong input, returning to guest menu...");
                            EternalGuestMenu();
                            break;
                    }
                }
            }
        }
        static Hotel HotelFillingSimulation()
        {
            List<Suite> suites = new List<Suite>();
            Suite.Type[] typeArr = new Suite.Type[3];
            typeArr[0] = Suite.Type.Standard;
            typeArr[1] = Suite.Type.SemiLuxe;
            typeArr[2] = Suite.Type.Luxe;
            Suite.Capacity[] capArr = new Suite.Capacity[4];
            capArr[0] = Suite.Capacity.Single;
            capArr[1] = Suite.Capacity.Double;
            capArr[2] = Suite.Capacity.Twinn;
            capArr[3] = Suite.Capacity.Family;
            for(int i = 0; i<typeArr.Length;i++)
            {
                for(int j=0; j<capArr.Length;j++)
                {
                    Suite suite = new Suite(typeArr[i], capArr[j]);
                    suites.Add(suite);
                }
            }
            Hotel hotel = new Hotel("California", suites);
            return hotel;
        }

        static void HandlerFillingSimulation(Hotel hotel)
        {
            BookingHandlerSingleton bookingHandler = new BookingHandlerSingleton();
            string[] names = { "Serediuk Valentyn","Dankov Artem","Gusak Mykhaylo","Kurenna Anna"};
            string[] logins = {"serediukit","asapforever", "hurmaze","tteaman"};
            string[] passwords = { "valik050703", "hvost1234", "mishakrut228", "kuranet17" };
            string[] passportIDs = { "105410589", "171384404", "134567682", "517790397" };
            DateTime[] birthDates = { new DateTime(2003, 07, 05), new DateTime(2003, 06, 26), new DateTime(2002, 01, 28) };
            for(int i = 0;i<names.Length-1;i++)
            {
                uint parsePass = Convert.ToUInt32(passportIDs[i]);
                Guest guest = new Guest(names[i], logins[i], passwords[i], parsePass, birthDates[i]);
            }
            string[] adminData = { "asapforever","hvost1234","Dankov Artem"};
            Admin admin = new Admin(adminData[0], adminData[1], adminData[2]);
            return;
        }
        
        static void ShowHotelSuitesInfo(Hotel hotel)
        {
            List<Suite> suites = hotel.Suites;
            foreach (var suite in suites)
            {
                PrintSuiteInfo(suite);
            }
        }

        static void PrintSuiteInfo(Suite suite)
        {
            Console.WriteLine("'''''''''''''''''''''''''''''''''''");
            if (suite.Free == true) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Suite "+suite.roomID.ToString()+" INFO:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Number: "+suite.roomID.ToString());
            Console.WriteLine("Type: "+suite.RoomType.ToString());
            Console.WriteLine("Capacity:"+suite.SuiteCapacity.ToString());
            Console.WriteLine("Maximum people to live: "+suite.PeopleMaxAmount.ToString());
            Console.WriteLine("Suite area: "+ suite.Area.ToString()+" sq m");
            Console.WriteLine("Has WiFi : "+suite.WiFi.ToString());
            Console.WriteLine("Has TV Video player : "+suite.TVvideoPlayer.ToString());
            Console.WriteLine("Has big TV: "+suite.BigTV.ToString());
            Console.WriteLine("Has additional service: "+suite.AdditionalService.ToString());
            Console.WriteLine("Price for night: "+suite.PriceForNight.ToString());
            Console.WriteLine("'''''''''''''''''''''''''''''''''''");
        }

        static void PrintMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to the hotel 'California'!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Today's date:" + BookingHandlerSingleton.Instance.CurrentDate.ToString());
            Console.WriteLine("1-Login as a Guest");
            Console.WriteLine("2-Login as an Admin");
            Console.WriteLine("3-Sign up (as a guest only)");
            Console.WriteLine("4-Exit");
        }

        static string EternalEnter()
        {
            Console.WriteLine("Please, put an integer to continue:");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case(1):
                    Guest guest = GuestLogin();
                    if (guest != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Succesfully logged in");
                        Console.WriteLine("Greetings, " + guest.Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        string res = "1";
                        res += guest.UserID.ToString();
                        return res;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input(Login/password)/User not found");
                        Console.ForegroundColor = ConsoleColor.White;
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        PrintMainMenu();
                        EternalEnter();
                        return " ";
                    }
                case (2):
                    Admin admin = AdminLogin();
                    if (admin != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Succesfully logged in");
                        Console.WriteLine("Greetings, " + admin.Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        string res2 = "2";
                        res2 = res2 + admin.UserID.ToString();
                        return res2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input(Login/password)/User not found");
                        Console.ForegroundColor = ConsoleColor.White;
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        PrintMainMenu();
                        EternalEnter();
                        return " ";
                    }
                case (3):
                    Guest sGuest = GuestSignUp();
                    string res3;
                    if (sGuest != null)
                    {
                        res3 = "3" + sGuest.UserID;
                        return res3;
                    }
                    else
                    {
                        Console.WriteLine("Please,try again...");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        PrintMainMenu();
                        EternalEnter();
                    }
                    return " ";
                case (4):
                    return "4";
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input, please try again");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    PrintMainMenu();
                    EternalEnter();
                    return " ";

            }
        }
        static Guest GuestLogin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please, put your login and password below");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Login:");
            string login = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = ReadPassword();
            foreach(var guest in BookingHandlerSingleton.Instance.GuestDB)
            {
                if (guest.Login == login && guest.Password == password) return guest;
            }
            return null;
        }

        static Admin AdminLogin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please, put your login and password below");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Login:");
            string login = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = ReadPassword();
            foreach (var admin in BookingHandlerSingleton.Instance.AdminDB)
            {
                if (admin.Login == login && admin.Password == password) return admin;
            }
            return null;
        }
        static Guest GuestSignUp()
        {
            Console.WriteLine("Please, follow the instructions below:");
            Console.WriteLine("Put your login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Put your password: ");
            string password = ReadPassword();
            Console.WriteLine("Put your first name(first lowercase):");
            string fname = Console.ReadLine();
            Console.WriteLine("Put your last name(first lowercase):");
            string lname = Console.ReadLine();
            string name = fname + " " + lname;
            Console.WriteLine("Put your passport ID:");
            string passport = Console.ReadLine();
            for (int i = 0; i < passport.Length; i++)
            {
                if (!Char.IsDigit(passport[i])) return null;
            }
            uint parsedPassport = Convert.ToUInt32(passport);
            Guest guest;
            try
            {
                Console.WriteLine("Put your birth date (DateTime format):");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());
                guest = new Guest(name, login, password, parsedPassport, birthDate);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (UserException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return guest;
        }
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }

        static string EternalGuestMenu()
        {
            Console.WriteLine("Guest Menu:");
            Console.WriteLine("1-Show suites info");
            Console.WriteLine("2-Book/Rent a number");
            Console.WriteLine("3-Extend booking");
            Console.WriteLine("4-Change date");
            Console.WriteLine("5-Log Out");
            Console.WriteLine("6-Exit");
            uint choice = Convert.ToUInt32(Console.ReadLine());
            switch(choice)
            {
                case (1):
                    ShowHotelSuitesInfo(BookingHandlerSingleton.Instance.HotelDB[0]);
                    EternalGuestMenu();
                    break;
                case (2):
                    return "2";
                case (3):
                    return "3";
                case (4):
                    BookingHandlerSingleton.Instance.ChangeDate();
                    return "4";
                case (5):
                    Console.Clear();
                    Main();
                    break;
                case (6):
                    return " ";
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input");
                    EternalGuestMenu();
                    break;
            }
            return " ";
        }
        static string GuestBooking(Guest guest)
        {
            uint SuiteID = 0;
            DateTime dateFrom = DateTime.MinValue;
            DateTime dateTo = DateTime.MinValue;
            Suite suite;
            Console.WriteLine("Please, put the number of suite you want to book:");
            SuiteID = Convert.ToUInt32(Console.ReadLine());
            suite = BookingHandlerSingleton.Instance.HotelDB[0].GetSuiteByID(SuiteID);
            if (suite==null)
            {
                Console.WriteLine("Suite was not found, please, try again...");
                GuestBooking(guest);
            }
            PrintSuiteInfo(suite);
            Console.WriteLine("Please,put the date you want to check-in (YYYY/MM/DD):");
            try
            {
                dateFrom = StringToDateTime(Console.ReadLine());
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return " ";
            }
            Console.WriteLine("Please,put the date you want to check-out (YYYY/MM/DD):");
            try
            {
                dateTo = StringToDateTime(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return " ";
            }
            Console.WriteLine("Please, put the amount of guests for your booking:");
            uint amount= Convert.ToUInt32(Console.ReadLine());
            Booking booking = null;
            try
            {
                booking = new Booking(BookingHandlerSingleton.Instance.HotelDB[0], guest, suite, dateFrom, dateTo, amount);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex.ParamName);
                Console.WriteLine(ex.Message);
                Console.WriteLine("You've set the wrong parameters, returning to Guest menu");
                return " ";
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.ParamName);
                Console.WriteLine(ex.Message);
                Console.WriteLine("Something went wrong, returning to Guest menu");
                return " ";
            }
            catch(BookingException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Something went wrong, returning to Guest menu");
                return " ";
            }
            Console.WriteLine("The total price is:" + booking.TotalPrice.ToString());
            Console.WriteLine("Do you agree to book this suite?(Y/N):");
            string agree = " ";
            while(true)
            {
                agree = Console.ReadLine();
                if(agree=="Y")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Booking was made succesfully");
                    Console.WriteLine("We are waiting for you on " + booking.BookingFrom.Date.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Returning to main menu...");
                    return " ";
                }
                if (agree == "N")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    BookingHandlerSingleton.Instance.RemoveBooking(booking);
                    Console.WriteLine("Booking was discarded");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Returning to main menu...");
                    return " ";
                }
                else Console.WriteLine("Wrong input,try again:");
            }
        }
        static string EternalAdminMenu()
        {
            Console.WriteLine("Guest Menu:");
            Console.WriteLine("1-Show suites info");
            Console.WriteLine("2-Show booking info");
            Console.WriteLine("3-Check settlement account");
            Console.WriteLine("4-Change date");
            Console.WriteLine("5-Change suites");
            Console.WriteLine("6-Log Out");
            Console.WriteLine("7-Exit");
            uint choice = Convert.ToUInt32(Console.ReadLine());
            switch (choice)
            {
                case (1):
                    ShowHotelSuitesInfo(BookingHandlerSingleton.Instance.HotelDB[0]);
                    EternalAdminMenu();
                    break;
                case (2):
                    foreach(var booking in BookingHandlerSingleton.Instance.BookingDB)
                    {
                        PrintBookingInfo(booking);
                    }
                    return "2";
                case (3):
                    foreach(var hotel in BookingHandlerSingleton.Instance.HotelDB)
                    {
                        Console.WriteLine("Hotel: " + hotel.hotelName);
                        Console.WriteLine("On account: " + hotel.SettlementAccount.ToString());
                    }    
                    return "3";
                case (4):
                    return "4";
                case (5):
                    break;
                case (6):
                    Console.Clear();
                    Main();
                    break;
                case (7):
                    return " ";
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input");
                    EternalAdminMenu();
                    break;
            }
            return " ";
        }
        static DateTime StringToDateTime(string strDate) // (YYYY/MM/DD)-string format
        {
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            int year = 0;
            int month = 0;
            int day = 0;
            for (int i = 0; i<4;i++)
            {
                strYear = strYear+strDate[i];
            }
            for (int i = 5; i < 7; i++)
            {
                strMonth = strMonth + strDate[i];
            }
            for(int i = 8;i<10;i++)
            {
                strDay = strDay + strDate[i];
            }
            year = Convert.ToInt32(strYear);
            month = Convert.ToInt32(strMonth);
            day = Convert.ToInt32(strDay);
            DateTime date = new DateTime(year, month, day);
            return date;
        }
        
        static void PrintBookingInfo(Booking booking)
        {
            Console.WriteLine("Booking ID: " + booking.ID.ToString());
            Console.WriteLine("Booking Hotel: " + booking.Hotel.hotelName.ToString());
            PrintGuestInfo(booking.Guest);
            Console.WriteLine("Booking check-in date:" + booking.BookingFrom.Date.ToString());
            Console.WriteLine("Booking check-out date:" + booking.BookingTo.Date.ToString());
            Console.WriteLine("Booking people amount:" + booking.GuestAmount.ToString());
            PrintSuiteInfo(booking.Suite);
            Console.WriteLine("Booking total price: " + booking.TotalPrice.ToString());
        }
        static void PrintGuestInfo(Guest guest)
        {
            Console.WriteLine("Guest ID: " + guest.UserID.ToString());
            Console.WriteLine("Guest name:" + guest.Name);
            Console.WriteLine("Guest passport:" + guest.GetPassID());
        }
    }
}