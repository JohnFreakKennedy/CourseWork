using System;
using System.Collections.Generic;

namespace HotelLib
{
    public class Hotel
    {
        private readonly string _hotelName;
        private List<Suite> Suites;
        private decimal _settlementAccount;
        public Hotel(string hotelName, List<Suite> suites)
        {
            _hotelName = hotelName;
            _settlementAccount = 0;
            Suites = null;
            foreach(var suite in suites)
            {
                Suites.Add(suite);
            }
        }

        public List<Suite>  GetSuitesDatabase()
        {
            return Suites;
        }
        public void AddSuiteToHotel(Suite suite)
        {
            if (suite.Hotel == null) Suites.Add(suite); 
        }
    }
}
