using System;


namespace HotelLib
{
    public class Suite
    {
        public enum Type
        {
            Standard, SemiLuxe, Luxe
        }
        public enum Capacity
        {
            Single, Double, Twinn, Family
        }
        private static uint IDcounter = 1;
        public Type RoomType { get; private set; }
        public uint roomID { get; private set; }
        public uint PeopleMaxAmount { get; private set; }
        public uint Area { get; private set; }
        public bool WiFi { get; private set; }
        public bool TVvideoPlayer { get; private set; }
        public bool BigTV { get; private set; }
        public bool AdditionalService { get; private set; }
        public Capacity SuiteCapacity { get; private set; }
        public decimal PriceForNight { get; private set; }
        public bool Free { get; private set; }
        public Suite(Type type, Capacity capacity)
        {
            roomID = IDcounter;
            IDcounter++;
            Area = 0;
            PriceForNight = 0;
            Free = true;
            switch (type)
            {
                case (Type.Standard):
                    Area += 30;
                    PriceForNight += 500;
                    WiFi = false;
                    TVvideoPlayer = false;
                    BigTV = false;
                    AdditionalService = false;
                    break;
                case (Type.SemiLuxe):
                    Area += 40;
                    PriceForNight += 750;
                    WiFi = true;
                    TVvideoPlayer = true;
                    BigTV = false;
                    AdditionalService = false;
                    break;
                case (Type.Luxe):
                    Area += 50;
                    PriceForNight += 1000;
                    WiFi = true;
                    TVvideoPlayer = true;
                    BigTV = true;
                    AdditionalService = true;
                    break;
                default:
                    throw new ArgumentException("Wrong type was set while adding a Suite");
            }
            switch (capacity)
            {
                case (Capacity.Single):
                    Area += 0;
                    PriceForNight *= 1;
                    PeopleMaxAmount = 1;
                    SuiteCapacity = Capacity.Single;
                    return;
                case (Capacity.Double):
                    Area += 5;
                    PriceForNight *= 2;
                    PeopleMaxAmount = 2;
                    SuiteCapacity = Capacity.Double;
                    return;
                case (Capacity.Twinn):
                    Area += 10;
                    PriceForNight = PriceForNight * 2 + 250;
                    PeopleMaxAmount = 2;
                    SuiteCapacity = Capacity.Twinn;
                    return;
                case (Capacity.Family):
                    PriceForNight *= 2;
                    Area += 15;
                    PeopleMaxAmount = 5;
                    SuiteCapacity = Capacity.Family;
                    return;
                default:
                    throw new ArgumentException("Wrong capacity was set while adding a Suite");
            }
        }
        public void RoomChange(Type RoomType, Capacity SuiteCapacity,  uint roomID, uint PeopleMaxAmount, uint Area, bool WiFi, bool TVvideoPlayer, bool BigTV, bool AdditionalService, decimal PriceForNight)
            {
            this.RoomType = RoomType;
            this.SuiteCapacity = SuiteCapacity;
            this.roomID = roomID;
            this.PeopleMaxAmount = PeopleMaxAmount;
            this.Area = Area;
            this.WiFi = WiFi;
            this.TVvideoPlayer = TVvideoPlayer;
            this.BigTV = BigTV;
            this.AdditionalService = AdditionalService;
            this.PriceForNight = PriceForNight;
            }
        public void HoldRoom()
        {
            Free = false;
        }
        public void FreeRoom()
        {
            Free = true;
        }
    }
}

