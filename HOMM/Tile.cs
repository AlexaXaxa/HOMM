using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMM
{
    public enum TileType 
    { 
        Grass, 
        Water,
        Forest,

        Castle,

        Enemy, 
        Hero
    }
    public class Tile
    {
        public TileType Type { get; set; }
        public Tuple<int, int> Coords;
       
        public Tile(TileType type, Tuple<int, int> coords)
        {
            Type = type;
            Coords = coords;
            
        }
        
    }
}
