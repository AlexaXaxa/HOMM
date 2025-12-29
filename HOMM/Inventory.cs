using HOMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
   
    public class Inventory
    {
        public List<Troops> _troops = new List<Troops>();
        public Inventory()
        {
            //Starter troops
            Add(TileSkin.Bone_Dragon, 3);
        }

        public void Add(TileSkin type, int amount)
        {
            _troops.Add(new Troops(type, amount));
        }


    }
}
