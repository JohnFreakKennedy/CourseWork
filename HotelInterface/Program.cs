using System;
using System.Collections.Generic;


namespace HotelLib
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Suite> suites = new List<Suite>();
            Hotel California = HotelFillingSimulation();
            HandlerFillingSimulation(California);
            PrintMainMenu();
            string firstres = String.Empty;
            try
            {
                firstres=EternalEnter();
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
            switch(firstres[0])
            {
                case ('1'):
                    for (int i = 1; i < firstres.Length; i++)
                    {
                        UserIDstr = UserIDstr + firstres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    guest = BookingHandlerSingleton.Instance.GetGuestByID(UserID);
                    break;
                case ('2'):
                    for (int i = 1; i < firstres.Length; i++)
                    {
                        UserIDstr = UserIDstr + firstres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    admin = BookingHandlerSingleton.Instance.GetAdminByID(UserID);
                    break;
                case ('3'):
                    for (int i = 1; i < firstres.Length; i++)
                    {
                        UserIDstr = UserIDstr + firstres[i];
                    }
                    UserID = Convert.ToUInt32(UserIDstr);
                    guest = BookingHandlerSingleton.Instance.GetGuestByID(UserID);
                    break;
            }
            //if (admin == null) EternalGuestMenu();
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
            List<Suite> suites = hotel.GetSuitesDatabase();
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
    }
}
