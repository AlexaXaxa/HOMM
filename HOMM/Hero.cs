using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
   
    class Hero
    {
        string Name {get; set; }
        string Color { get;set; }

        public Hero(string name, string color)
        {
            Name = name;
            Color = color;
        }

    }
}
