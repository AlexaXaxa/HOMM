using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public class Stack:IStack
    {
        public EnemyType Type{get;set;}
        public int Amount { get; set; }
        public Stack(EnemyType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
