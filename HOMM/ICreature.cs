using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public interface ICreature

    {
        int Damage { get;}
        int HP { get; }

        int Speed { get; }
    }
}
