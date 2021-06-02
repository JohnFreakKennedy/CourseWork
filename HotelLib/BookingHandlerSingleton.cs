using System;
using System.Collections.Generic;

namespace HotelLib
{
    public class BookingHandlerSingleton
    {
        private static BookingHandlerSingleton instance;
        public List<Booking> BookingDB { get; private set; }
        public List<Guest> GuestDB { get; private set; }
        public List<Admin> AdminDB { get; private set; }
        public List<Hotel> HotelDB { get; private set; }
        /// <summary>
        /// Put private access level on DBs later
        /// </summary>
        /// 
       
        public static BookingHandlerSingleton Instance { get { if (instance == null) instance = new BookingHandlerSingleton(); return instance; } }
        public BookingHandlerSingleton()
        {
            CurrentDate = DateTime.Now;
            BookingDB = new List<Booking>();
            GuestDB = new List<Guest>();
            AdminDB = new List<Admin>();
            HotelDB = new List<Hotel>();
        }
        public DateTime CurrentDate { get; private set; }
        public void ChangeDate()
        {
            TimeSpan span = new TimeSpan(1, 0, 0, 0, 0);
            CurrentDate = CurrentDate + span;
            Status();
        }
        public bool TryAddBookingToDB(Booking booking)
        {
            bool suiteMatch = false;
            foreach(var DBBooking in BookingDB)
            {
                if (DBBooking.Hotel == booking.Hotel && DBBooking.Suite == booking.Suite) suiteMatch = true;
                if (suiteMatch && booking.BookingFrom > DBBooking.BookingFrom && booking.BookingTo < DBBooking.BookingTo) return false;
                if (suiteMatch && booking.BookingTo > DBBooking.BookingFrom && booking.BookingFrom < DBBooking.BookingFrom) return false;
                if (suiteMatch && booking.BookingFrom < DBBooking.BookingTo && booking.BookingTo > DBBooking.BookingTo) return false;
            }
            BookingDB.Add(booking);
            return true;
        }

        public void TryAddGuestToDB(Guest guest)
        {
            foreach(var DBguest in GuestDB)
            {
                if (DBguest.PassportID == guest.PassportID) throw new UserException(guest);
                if (DBguest.Login == guest.Login) throw new UserException(guest);
            }
            GuestDB.Add(guest);
        }
        public void TryAddAdminToDB(Admin admin)
        {
            foreach (var DBadmin in AdminDB)
            {
                if (DBadmin.Login == admin.Login) throw new UserException(admin);
            }
            AdminDB.Add(admin);
        }
        public bool TryAddHotelToDB(Hotel hotel)
        {
            if (hotel.hotelName == null) return false;
            if (hotel.Suites == null) return false;
            foreach (var DBHotel in HotelDB)
            {
                if (DBHotel.hotelName == hotel.hotelName) return false;
            }
            HotelDB.Add(hotel);
            return true;
        }

        public Guest GetGuestByID(uint value)
        {
            foreach(var guest in GuestDB)
            {
                if (guest.UserID == value) return guest;
            }
            return null;
        }
        public Admin GetAdminByID(uint value)
        {
            foreach (var admin in AdminDB)
            {
                if (admin.UserID == value) return admin;
            }
            return null;
        }
        public void RemoveBooking(Booking booking)
        {
            foreach(var DBBooking in BookingDB)
            {
                if (DBBooking.ID == booking.ID) BookingDB.Remove(booking);
            }    
        }
        public void Status()
        {
            foreach(var booking in BookingDB)
            {
                if (CurrentDate > booking.BookingTo) 
                {
                    BookingDB.Remove(booking);
                    booking.Suite.FreeSuite();
                }
                if (CurrentDate.Date==booking.BookingFrom.Date)
                {
                    booking.Suite.HoldSuite();
                    booking.Hotel.PutOnSettlementAccount(booking.TotalPrice);
                }
            }
        }
    }
}
