using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    //CreatureType must be ICreature
    public class BattleStack : IStack
    {
        public ICreature Prototype { get; }
        public int Amount { get; set; }
        public int Turn { get; set; }
        
        public int Speed { get; }
        public int Damage { get; }
        public int HP { get; }

        public BattleStack(TileSkin creatureSkin, int amount, int turn = 0)
        {
            
            Amount = amount;
            Turn = turn;

            Prototype = creatureSkin switch
            {
                TileSkin.Mummy => new Mummy(),
                TileSkin.Bone_Dragon => new Bone_Dragon(),
                TileSkin.Skeleton => new Skeleton(),

                _ => throw new Exception("Unknown creature")
            };
           
            Speed = Prototype.Speed;
            Damage = Prototype.Damage;
            HP = Prototype.HP;

            for (int i = 0; i < Amount; i++)
            {
                
                
            }
        }
    }
}
