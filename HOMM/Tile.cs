using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HOMM
{
    public enum TileSkin
    { 
        Grass, 
        Water,
        Forest,

        Castle,
        Enemy,
        Skeleton,
        Zombie,
        Mummy,
        Vampire,
        Linch,
        Bone_Dragon,

        Hero
    }
    public class Tile
    {
        public TileSkin Skin { get; set; }
        public Tuple<int, int> Coords;      
        public IStack Stack { get; set; }

        public Tile(TileSkin type, Tuple<int, int> coords, IStack stack = null)
        {
            Skin = type;
            Coords = coords;
            Stack = stack;
           
           
        }
    }
}
