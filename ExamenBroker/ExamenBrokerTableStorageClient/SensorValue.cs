using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenBroker.Receiver
{
    public class SensorValue : TableEntity
    {
        public String Name { get; set; }
    }
}
