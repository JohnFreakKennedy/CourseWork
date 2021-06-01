using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLib
{
    public class SuiteException:Exception
    {
        public SuiteException()
        {

        }

        public SuiteException(Suite suite)
            : base(String.Format("An error occured while managing suites ID: {0}", suite.roomID.ToString()))
        {

        }
    }
}
