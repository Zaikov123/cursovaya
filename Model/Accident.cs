using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPZaikovAPP.Model
{
    public class Accident
    {
        public DTP DTPId { get; set; }
        public List<Driver> DriversId { get; set; }
        public Car CarId { get; set; }
    }
}
