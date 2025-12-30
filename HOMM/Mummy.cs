using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    internal class Mummy: ICreature
    {
        public int Damage => 4;
        public int HP => 25;
        public int Speed => 4;
        public Mummy()
        {


        }
    }
}
