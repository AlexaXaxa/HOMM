using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    class Mummy: IEnemy
    {
        public int Health { get; set; }
        public int Attack { get; set; }

        public Mummy()
        {
            Health = 25;
            Attack = 6;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
            Console.WriteLine("Mummy took damage: " + damage);
        }
    }
}
