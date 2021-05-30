﻿using System;


namespace HotelLib
{
    public class Booking
    {
        private static uint IDcounter = 1;
        public Hotel Hotel { get; private set; }
        public Guest Guest { get; private set; }
        public uint ID { get; private set; }
        public Suite Suite { get; private set; }
        public DateTime BookingFrom { get; private set; }
        public DateTime BookingTo { get; private set; }
        public uint GuestAmount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Booking(Hotel hotel, Guest guest, Suite suite, DateTime bookingFrom, DateTime bookingTo, uint guestAmount, decimal totalPrice)
        {
            if (hotel == null) throw new ArgumentNullException("Hotel", "You have didn't set some of the parameters, please, check your input and try again...");
            if (guest == null) throw new ArgumentNullException("Guest (Bookmaker)", "You have didn't set some of the parameters, please, check your input and try again...");
            if (suite == null) throw new ArgumentNullException("Suite", "You have didn't set some of the parameters, please, check your input and try again...");
            if (bookingFrom == null) throw new ArgumentNullException("Booking from", "You have didn't set some of the parameters, please, check your input and try again...");
            if (bookingTo == null) throw new ArgumentNullException("Booking to", "You have didn't set some of the parameters, please, check your input and try again...");
            if (guestAmount == 0) throw new ArgumentNullException("Guest amount", "You have didn't set some of the parameters, please, check your input and try again...");
            if (DateTime.Compare(bookingFrom, bookingTo) >= 0) throw new ArgumentException("Dates are incorrect, please, check your input and try again...");
            if (guestAmount > 5) throw new ArgumentException("The hotel isn't able to room such amount of guests, please divide your bookings or put another amount of guests");
            if (bookingFrom < BookingHandlerSingleton.Instance.CurrentDate || bookingTo < BookingHandlerSingleton.Instance.CurrentDate) throw new ArgumentException("Booking from", "Date,that you want to book has passed already, please, try again...");
            if (suite.SuiteCapacity == Suite.Capacity.Single && guestAmount > 1) throw new TooManyPeopleException(guestAmount);
            if ((bookingTo - bookingFrom).TotalDays < 1)
            {
                TimeSpan minTimespan = new TimeSpan(1, 0, 0, 0);
                bookingFrom -= minTimespan;
            }
            if (suite.SuiteCapacity == Suite.Capacity.Double && guestAmount > 2) throw new TooManyPeopleException(guestAmount);
            if (suite.SuiteCapacity == Suite.Capacity.Twinn && guestAmount > 2) throw new TooManyPeopleException(guestAmount);
            ID = IDcounter;
            IDcounter++;
            TimeSpan cits = new TimeSpan(14, 0, 0); // check-in Timespan (to set time for check-in as 14:00)
            TimeSpan cots = new TimeSpan(12, 0, 0); // check-out Timespan (to set time for check-out as 12:00)
            bookingFrom = bookingFrom + cits;
            bookingTo = bookingTo + cots;
            Hotel = hotel;
            Guest = guest;
            Suite = suite;
            GuestAmount = guestAmount;
            TotalPrice = suite.PriceForNight * (int)Math.Round((bookingTo - bookingFrom).TotalDays);
            if (BookingHandlerSingleton.Instance.TryAddBookingToDB(this) == false) throw new BookingException(this);
        }
        public void ResetBooking()
        {
            Hotel = null;
            Guest = null;
            ID = 0;
            Suite = null;
            BookingFrom = DateTime.MinValue;
            BookingTo = DateTime.MinValue;
            GuestAmount = 0;
            TotalPrice = 0;
        }
    }
}