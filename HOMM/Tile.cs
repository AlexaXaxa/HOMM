using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HOMM
{
    public enum TileType 
    { 
        Grass, 
        Water,
        Forest,

        Castle,
        Enemy,
        Skeletton, 
        Mummy,

        Hero
    }
    public class Tile
    {
        public TileType Type { get; set; }
        public Tuple<int, int> Coords;
        public int TroopsCount{get;set;}
        public TileType EnemyType;

        public Tile(TileType type, Tuple<int, int> coords,TileType enemyType = 0, int troopsCount = 0)
        {
            Type = type;
            Coords = coords;
            if (troopsCount >= 9)
                TroopsCount = troopsCount;
            EnemyType = enemyType;
        }
        
    }
}
