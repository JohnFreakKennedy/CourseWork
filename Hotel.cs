using System;

namespace HotelLib
{
    public class Hotel
    {
        private readonly string _hotelName;
        private Suite[] _rooms;
        private Booking[] _bookings;
        public static DateTime currentDate = DateTime.UtcNow;
        public Hotel(string hotelName,Suite[] rooms)
        {
            _hotelName = hotelName;
            _rooms = rooms;
        }
        public void Tomorrow()
        {
            currentDate.AddDays(1);
        }
        public Booking[] GetBookingDataBase()
        {
            return _bookings;
        }
        public Suite[] GetSuitesDatabase()
        {
            return _rooms;
        }
    }
}
