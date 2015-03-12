using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Oefening1.Models
{
    public interface IItemProduct
    {
        int Amount { get; set; }
        Device RentDevice { get; set; }
    }
}
