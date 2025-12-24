using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    class Skeleton : IEnemy
    {
        public int Health {get; set;}
        public int Attack { get; set; }

        public Skeleton()
        {
            Health = 4;
            Attack = 4;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
            Console.WriteLine("Skeleton took damage: " + damage);
        }

    }
}
