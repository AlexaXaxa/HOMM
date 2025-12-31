using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public interface IStack
    {
        
        public int Amount { get; set; }
        public int Turn { get; set; }
        public void UpdateStack();
        public void Attack(IStack stack);
    }
}
