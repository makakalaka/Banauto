using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banauto.Backend
{
    public class Order:Backend.User
    {
        private string orderID;
        private string autoID;
        private string newUsername;

        public string NewUsername
        {
            get { return newUsername; }
            set { newUsername = value; }
        }


        public string AutoID
        {
            get { return autoID; }
            set { autoID = value; }
        }

        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }


    }
}
