using System;

namespace HotelLib
{
    public class TooManyPeopleException:Exception
    {
        public TooManyPeopleException()
        {

        }

        public TooManyPeopleException(uint amount)
            : base(String.Format("Too much people for suite,that was set: {0}", amount.ToString()))
        {

        }
    }
}
