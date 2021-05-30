using System;
using System.Collections.Generic;

namespace HotelLib
{
    public class BookingHandlerSingleton
    {
        private static BookingHandlerSingleton instance;
        private List <Booking> BookingDB;
        /// <summary>
        /// Put delegate and event for Tomorrow later
        /// </summary>
        /// 
        public static BookingHandlerSingleton Instance { get { if (instance == null) instance = new BookingHandlerSingleton(); return instance; } }
        private BookingHandlerSingleton()
        {
            CurrentDate = DateTime.Now;
            BookingDB = null;
        }
        public DateTime CurrentDate { get; private set; }
        public void ChangeDate()
        {
            CurrentDate.AddDays(1);
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
    }
}
