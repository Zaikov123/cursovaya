using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPZaikovAPP.Model
{
    public class DTP
    {
        public DateTime DateDTP { get; set; }
        public int Id { get; set; }
        public string Place { get; set; }
        public uint СasualtyRate { get; set; }
        public ulong ActNumber { get; set; }
        public string DTPCause { get; set; }
        public string TypeOfDTP { get; set; }
        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
