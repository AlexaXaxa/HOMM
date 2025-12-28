using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public interface IStack
    {
        public EnemyType Type { get; set; }
        public int Amount { get; set; }
    }
}
