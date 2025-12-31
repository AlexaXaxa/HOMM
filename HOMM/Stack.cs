using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public class Stack:IStack
    {
        public TileSkin CreatureSkin{get;set;}
        public int Amount { get; set; }
        public int Turn { get; set; }
        public Stack(TileSkin type, int amount)
        {
            CreatureSkin = type;
            Amount = amount;
        }

        public void UpdateStack()
        {
            throw new NotImplementedException();
        }

        public void Attack(IStack stack)
        {
            throw new NotImplementedException();
        }
    }
}
