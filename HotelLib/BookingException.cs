using System;

namespace HotelLib
{
    public class BookingException:Exception
    {
        public BookingException()
        {

        }

        public BookingException(Booking booking)
            : base(String.Format("A problem with booking occured: {0}", booking.ID.ToString()))
        {

        }
    }
}
