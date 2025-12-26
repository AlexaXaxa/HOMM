using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace HOMM
{
    /// <summary>
    /// Interaction logic for AdventureView.xaml
    /// </summary>
    public partial class AdventureView : UserControl, IUpdatable
    {
        double viewSize_Px;
        int viewSize_Tile;
        int tileSize_Px;
        Tile[,] Map;
        int heroX = 3;
        int heroY = 1;
        int CameraX;
        int CameraY;
        int MapSize_Tile;
        public event EventHandler EnemyEncountered;
        public AdventureView(Tile[,] map, double minimumSizeOfsceen, int tileSize_Px, int mapSize_Tile)
        {
            InitializeComponent();
            viewSize_Px = minimumSizeOfsceen;
            this.tileSize_Px = tileSize_Px;
            Map = map;
            CameraX = (int)mapSize_Tile / 2;
            CameraY = (int)mapSize_Tile / 2;
            MapSize_Tile = mapSize_Tile;

            this.Loaded += AdventureView_Loaded;
        }

        private void AdventureView_Loaded(object sender, RoutedEventArgs e)
        {
            double infoWidth = viewSize_Px * 0.3;
            MapColumn.Width = new GridLength(viewSize_Px);
            InfoColumn.Width = new GridLength(infoWidth);

            MapSquare.Width = viewSize_Px;
            MapSquare.Height = viewSize_Px;

            viewSize_Tile = (int)(viewSize_Px / tileSize_Px);

            Draw();
        }

        public void DisplayInfo(CustomBorder b)
        {
            TextBlock t = new TextBlock();
            t.FontSize = 15;
            t.Text = b.Tile.Coords.Item1 + "," + b.Tile.Coords.Item2.ToString();
            if (b.Tile.Type == TileType.Enemy)
            {
                StackPanel p = new StackPanel();
                b.Child = p;
                TextBlock t2 = new TextBlock();
                t2.FontSize = 7;
                t2.Text = b.Tile.EnemyType + " " + b.Tile.TroopsCount;
                p.Children.Add(t);
                p.Children.Add(t2);
                return;

            }        
            b.Child = t;
        }
        public void Draw()
        {
            uniformGrid.Children.Clear();

            Map[heroX, heroY] = new Tile(TileType.Hero, new Tuple<int, int>(heroX, heroY));

            for (int y = 0; y < viewSize_Tile; y++)
            {
                for (int x = 0; x < viewSize_Tile; x++)
                {

                    int mapX = CameraX + x - viewSize_Tile / 2;
                    int mapY = CameraY + y - viewSize_Tile / 2;

                    CustomBorder b = new CustomBorder();
                    b.Width = tileSize_Px;
                    b.Height = tileSize_Px;
                    b.BorderThickness = new Thickness(1);
                    b.BorderBrush = Brushes.Black;


                    if (mapX < 0 || mapX >= MapSize_Tile || mapY < 0 || mapY >= MapSize_Tile)
                    {
                        b.Background = Brushes.Gray; // пустое место за пределами карты
                    }
                    else
                    {
                        Tile tile = Map[mapX, mapY];
                        b.Tile = tile;
                        b.AddHandler(Border.MouseLeftButtonDownEvent, new RoutedEventHandler(MoveHero), true);

                        switch (tile.Type)
                        {
                            case TileType.Grass:
                                b.Background = Brushes.LightGreen;
                                DisplayInfo(b);
                                break;
                            case TileType.Water:
                                b.Background = Brushes.Blue;
                                DisplayInfo(b);
                                break;
                            case TileType.Forest:
                                b.Background = Brushes.DarkGreen;
                                DisplayInfo(b);
                                break;
                            case TileType.Castle:
                                b.Background = Brushes.Violet;
                                DisplayInfo(b);
                                break;
                            case TileType.Enemy:
                                b.Background = Brushes.Red;
                                DisplayInfo(b);
                                break;
                            case TileType.Hero:
                                b.Background = Brushes.Violet;
                                DisplayInfo(b);
                                break;
                        }
                    }
                    uniformGrid.Children.Add(b);
                }
            }
        }
        private void MoveHero(object sender, RoutedEventArgs e)
        {
            if (sender is CustomBorder border)
            {
                int new_heroX = border.Tile.Coords.Item1;
                int new_heroY = border.Tile.Coords.Item2;

                Tile new_tile = Map[new_heroX, new_heroY];

                if ((new_tile.Type == TileType.Water) || (new_tile.Type == TileType.Forest) || (new_tile.Type == TileType.Castle))
                {
                    //do nothing
                }
                else if (new_tile.Type == TileType.Grass)
                {
                    Map[heroX, heroY] = new Tile(TileType.Grass, new Tuple<int, int>(heroX, heroY));
                    heroX = new_heroX;
                    heroY = new_heroY;
                    Map[heroX, heroY] = new Tile(TileType.Hero, new Tuple<int, int>(heroX, heroY));
                    Draw();
                }
                else if (new_tile.Type == TileType.Enemy)
                {
                    EnemyEncountered?.Invoke(new_tile, e);
                }
            }
        }
        public void Update()
        {
            Point pos = Mouse.GetPosition(uniformGrid); // координаты мыши внутри грида

            if (pos.Y <= 0) // вверх
                CameraY--;
            else if (pos.Y >= MapSquare.Height - 1) //вниз
                CameraY++;
            if (pos.X >= MapSquare.Width) // вправо
                CameraX++;
            else if (pos.X <= 0) // влево
                CameraX--;

            Draw();
        }
  
    }
}
