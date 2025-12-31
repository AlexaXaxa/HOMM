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
        public bool IsEnemy { get; set; }
        
        public int Speed { get; }
        public int Damage { get; }
        public int HP { get; }

        public BattleStack(TileSkin creatureSkin, int amount, bool isenemy = false)
        {
            
            Amount = amount;
            IsEnemy = isenemy;

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
