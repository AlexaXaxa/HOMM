using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HOMM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fält
        Tile[,] map;
        //может поменяться в зависимости от размера экрана
        int viewSize_Box = 10;
        static int sizeOfMap_Box = 7;
        int cameraX = (int)sizeOfMap_Box/2;
        int cameraY = (int)sizeOfMap_Box/2;
        int heroX = 3;
        int heroY = 1;
        int ViewSquare_Px;
        int sizeOfSquare_Px = 40;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += CameraTick;
            timer.Start();

            this.KeyDown += _KeyDown;

        }
        

        private void MoveHero(object sender, RoutedEventArgs e)
        {
            CustomBorder border = sender as CustomBorder;
            int new_heroX = border.Tile.Coords.Item1;
            int new_heroY = border.Tile.Coords.Item2;

            Tile test_tile = map[new_heroX, new_heroY];

            if ((test_tile.Type != TileType.Water) && (test_tile.Type != TileType.Forest) && (test_tile.Type != TileType.Castle))
            {
                map[heroX, heroY] = new Tile(TileType.Grass, new Tuple<int, int>(heroX, heroY));
                heroX = new_heroX;
                heroY = new_heroY;
                map[heroX, heroY] = new Tile(TileType.Hero, new Tuple<int, int>(heroX, heroY));

            }

            DrawMap();
        }
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.H)
                
            {
                // herox и heroy — координаты героя
                cameraX = heroX;
                cameraY = heroY;
                DrawMap();
            }
        }
        private void CameraTick(object sender, EventArgs e)
        {
            Point pos = Mouse.GetPosition(uniformGrid); // координаты мыши внутри грида

            if (pos.Y <= 0) // вверх
                cameraY--;            
            else if (pos.Y >= uniformGrid.ActualHeight - 10) //вниз
                cameraY++;            
            if (pos.X >= uniformGrid.ActualWidth) // вправо
                cameraX++;             
            else if (pos.X <= 0) // влево
                cameraX--;
               
            DrawMap();
     
        }
        public void DrawNumber(CustomBorder b)
        {
            TextBlock t = new TextBlock();
            t.FontSize = 15;
            t.Text = b.Tile.Coords.Item1 + b.Tile.Coords.Item2.ToString();
            b.Child = t;
        }
        public void DrawMap()
        {
            uniformGrid.Children.Clear();

            map[heroX, heroY] = new Tile(TileType.Hero, new Tuple<int, int>(heroX, heroY));

            for (int y = 0; y < viewSize_Box; y++)
            {
                for (int x = 0; x < viewSize_Box; x++)
                {

                    int mapX = cameraX + x - viewSize_Box / 2;
                    int mapY = cameraY + y - viewSize_Box / 2;

                    CustomBorder b = new CustomBorder();     
                    b.Width = sizeOfSquare_Px;
                    b.Height = sizeOfSquare_Px;
                    b.BorderThickness = new Thickness(1);
                    b.BorderBrush = Brushes.Black;

                    

                    

                    if (mapX < 0 || mapX >= sizeOfMap_Box || mapY < 0 || mapY >= sizeOfMap_Box)
                    {
                        b.Background = Brushes.Gray; // пустое место за пределами карты
                    }
                    else
                    {
                        Tile tile = map[mapX, mapY];
                        b.Tile = tile;
                        b.AddHandler(Border.MouseLeftButtonDownEvent, new RoutedEventHandler(MoveHero), true);

                        switch (tile.Type)
                        {
                            case TileType.Grass:
                                b.Background = Brushes.LightGreen;
                                DrawNumber(b);
                                break;
                            case TileType.Water:
                                b.Background = Brushes.Blue;
                                DrawNumber(b);
                                break;
                            case TileType.Forest:
                                b.Background = Brushes.DarkGreen;
                                DrawNumber(b);
                                break;
                            case TileType.Castle:
                                b.Background = Brushes.Violet;
                                DrawNumber(b);
                                break;
                            case TileType.Enemy:
                                b.Background = Brushes.Red;
                                DrawNumber(b);
                                break;
                            case TileType.Hero:
                                b.Background = Brushes.Violet;
                                DrawNumber(b);
                                break;
                        }
                    }
                    uniformGrid.Children.Add(b);
                }
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double windowWidth = this.ActualWidth;
            double windowHeight = this.ActualHeight;
            Console.WriteLine($"Ширина: {windowWidth}, Высота: {windowHeight}");
            //minimum side of screen
            double minSide  = Math.Min(this.ActualWidth, this.ActualHeight);
            
            ViewSquare_Px = (int)minSide;
            viewSize_Box = (int)(ViewSquare_Px / sizeOfSquare_Px);
            
            map = CreateTestMap();
            DrawMap();
        }
        Tile[,] CreateTestMap()
        {
            //размер карты в тайлах
            int sizeOfMap = 7;
            Tile[,] map = new Tile[sizeOfMap, sizeOfMap];

            for (int y = 0; y < sizeOfMap; y++)
            {
                for (int x = 0; x < sizeOfMap; x++)
                {
                    map[x, y] = new Tile(TileType.Grass, new Tuple<int, int>(x, y));
                }
            }

            //Enemy
            map[5, 1] = new Tile(TileType.Enemy, new Tuple<int, int>(5, 1));

            //Water
            map[6, 6] = new Tile(TileType.Water, new Tuple<int, int>(6, 6));
            map[6, 5] = new Tile(TileType.Water, new Tuple<int, int>(6, 5));
            map[6, 4] = new Tile(TileType.Water, new Tuple<int, int>(6, 4));
            map[5, 6] = new Tile(TileType.Water, new Tuple<int, int>(5, 6));
            map[5, 5] = new Tile(TileType.Water, new Tuple<int, int>(5, 5));
            map[4, 6] = new Tile(TileType.Water, new Tuple<int, int>(4, 6));

            //Forest
            map[0, 6] = new Tile(TileType.Forest, new Tuple<int, int>(0, 6));
            map[0, 5] = new Tile(TileType.Forest, new Tuple<int, int>(0, 5));
            map[0, 4] = new Tile(TileType.Forest, new Tuple<int, int>(0, 4));
            map[1, 6] = new Tile(TileType.Forest, new Tuple<int, int>(1, 6));
            map[1, 5] = new Tile(TileType.Forest, new Tuple<int, int>(1, 5));
            map[2, 6] = new Tile(TileType.Forest, new Tuple<int, int>(2, 6));
            map[0, 3] = new Tile(TileType.Forest, new Tuple<int, int>(0, 3));
            map[1, 4] = new Tile(TileType.Forest, new Tuple<int, int>(1, 4));
            map[1, 3] = new Tile(TileType.Forest, new Tuple<int, int>(1, 3));

            //Castle
            map[0, 0] = new Tile(TileType.Castle, new Tuple<int, int>(0, 0));
            map[0, 1] = new Tile(TileType.Castle, new Tuple<int, int>(0, 1));
            map[0, 2] = new Tile(TileType.Castle, new Tuple<int, int>(0, 2));
            map[1, 0] = new Tile(TileType.Castle, new Tuple<int, int>(1, 0));
            map[1, 1] = new Tile(TileType.Castle, new Tuple<int, int>(1, 1));
            map[1, 2] = new Tile(TileType.Castle, new Tuple<int, int>(1, 2));
            map[2, 0] = new Tile(TileType.Castle, new Tuple<int, int>(2, 0));
            map[2, 1] = new Tile(TileType.Castle, new Tuple<int, int>(2, 1));
            map[2, 2] = new Tile(TileType.Castle, new Tuple<int, int>(2, 2));

            return map;
        }
    }
}