using System;

namespace HotelLib
{
    public class BookingException : Exception
    {
        public BookingException(Booking booking)
            : base(String.Format("The suite is booked on these dates: {0}", booking.ID.ToString()))
        {

        }
    }
}