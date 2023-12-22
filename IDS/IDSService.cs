using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDS
{
    public class IDSService : IIDS
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
