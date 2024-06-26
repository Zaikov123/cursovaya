﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPZaikovAPP.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firm { get; set; }
        public string Brand { get; set; }
        public string BodyType { get; set; }
        public string LicensePlates { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
