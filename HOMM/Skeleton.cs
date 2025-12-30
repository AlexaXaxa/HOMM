using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    internal class Skeleton : ICreature
    {
        public int Damage => 3;

        public int HP => 4;

        public int Speed => 10;
    }
}
