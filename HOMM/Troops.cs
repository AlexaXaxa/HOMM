using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{

    public class Troops
    {

        public int Amount;
        public TileSkin Type;
        public Troops(TileSkin type, int amount)
        {
            Amount = amount;
            Type = type;
        }

    }
}
