using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDistances.Model
{
    class Server
    {
        public string name { get; set; }
        public int distance { get; set; }

        //public Server() { }
        public Server(string name, int distance)
        {
            this.name = name;
            this.distance = distance;
        }
    }
}
