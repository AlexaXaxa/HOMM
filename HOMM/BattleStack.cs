using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public class BattleStack : IStack
    {
        public TileSkin Type { get; set; }
        public int Amount { get; set; }
        public BattleStack(TileSkin type, int amount)
        {
            Type = type;
            Amount = amount;
            //for (int i = 0;i< stackAmount;i++)
            //{
            //    enemytype Enemy = new enemytype();
            //}
        }   
    }
}
