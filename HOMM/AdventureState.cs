using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HOMM
{
    class AdventureState: IGameMode
    {
        AdventureView view;
        public AdventureState(Tile[,] map)
        {
            view = new AdventureView(map);
        }
        public void Enter(MainWindow window)
        {
            window.SetView(view);
            view.DrawMap();
        }
    }
}
