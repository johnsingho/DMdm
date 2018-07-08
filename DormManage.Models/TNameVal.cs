using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Models
{
    public class TNameVal
    {
        public TNameVal(string n, string v)
        {
            Name = n;
            Value = v;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
