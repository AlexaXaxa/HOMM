using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    internal class Mummy: IEnemy
    {
        private int damage = 4;
        private int hp = 25;
        public int Damage => damage;
        public int HP => hp;
        public int Units;

        public Mummy()
        {


        }
    }
}
