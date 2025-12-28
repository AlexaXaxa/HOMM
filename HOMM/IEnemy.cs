using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public enum EnemyType
    {
        Skeletton,
        Mummy,
        Vampire
    }
    public interface IEnemy
    {
        int Damage { get;}
        int HP { get; }
    }
}
