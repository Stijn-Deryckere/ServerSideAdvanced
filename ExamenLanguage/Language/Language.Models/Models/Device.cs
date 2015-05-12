using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Models.Models
{
    public class Device
    {
        [Required(ErrorMessageResourceType= typeof(Properties.Devices.Devices), ErrorMessageResourceName="IdError")]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "NameError")]
        [Display(Name= "Name", ResourceType= typeof(Properties.Devices.Devices))]
        public String Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "PriceError")]
        [Display(Name = "Price", ResourceType = typeof(Properties.Devices.Devices))]
        private double _price;

        [DataType(DataType.Currency)]
        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "PriceError")]
        [Display(Name = "Price", ResourceType = typeof(Properties.Devices.Devices))]
        public double Price
        {
            get 
            { 
                return _price * double.Parse(Properties.Devices.Devices.ConvertEuroToLocal, CultureInfo.CurrentCulture); 
            }
            set { _price = value; } //Stokeer in euro
        }
        

        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "DescriptionError")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Devices.Devices))]
        public String Description { get; set; }
    }
}
