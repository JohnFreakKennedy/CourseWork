using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLib
{
    public class UserException:Exception
    {
        public UserException()
        {

        }

        public UserException(User user)
            : base(String.Format("A problem occured, while adding User to DataBase(Login/passport ID is already in DB): {0}", user.UserID.ToString()))
        {

        }
    }
}
