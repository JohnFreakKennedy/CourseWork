using System;
using System.Collections.Generic;

namespace HotelLib
{
    public class Hotel
    {
        public readonly string hotelName;
        private List<Suite> Suites;
        public decimal SettlementAccount { get; private set; }
        public Hotel(string hotelName, List<Suite> suites)
        {
            this.hotelName = hotelName;
            SettlementAccount = 0;
            Suites = suites;
            if (BookingHandlerSingleton.Instance.TryAddHotelToDB(this) == false) throw new ArgumentException("A problem occured while creating a Hotel");
        }

        public List<Suite>  GetSuitesDatabase()
        {
            return Suites;
        }

        public void AddSuiteToHotel(Suite suite)
        {
            if (suite.Hotel == null) Suites.Add(suite); 
        }
        public void PutOnSettlementAccount(decimal amount)
        {
            SettlementAccount+=amount;
        }
    }
}
