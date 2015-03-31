using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7RaspJobConsole
{
    public class SensorValue : TableEntity
    {
        public int Id { get; set; }
        public String Location { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
