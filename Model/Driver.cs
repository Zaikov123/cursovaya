using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPZaikovAPP.Model
{
    public class Driver
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public uint Exp { get; set; }
        public string Certificate { get; set; }
        public override string ToString()
        {
            return $"{Id}";
        }

    }
}
