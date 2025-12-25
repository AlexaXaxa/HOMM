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
        int viewSize_Box;
        static int sizeOfMap_Box = 7;
        int cameraX = (int)sizeOfMap_Box/2;
        int cameraY = (int)sizeOfMap_Box/2;
        int heroX = 3;
        int heroY = 1;
        int sizeOfSquare_Px = 40;
        int toggle = 1;
        int day = 0;
        int week = 0;
        int month = 0;
        private IGameMode mode;
       
        static BattleState battleState;

        #endregion
        public void SetView(UserControl view)
        {
            GameRoot.Children.Clear();
            GameRoot.Children.Add(view);
        }
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

            Tile new_tile = map[new_heroX, new_heroY];

            if((new_tile.Type == TileType.Water) || (new_tile.Type == TileType.Forest) || (new_tile.Type == TileType.Castle))
            {

            }
            else if(new_tile.Type == TileType.Grass)
            {
                map[heroX, heroY] = new Tile(TileType.Grass, new Tuple<int, int>(heroX, heroY));
                heroX = new_heroX;
                heroY = new_heroY;
                map[heroX, heroY] = new Tile(TileType.Hero, new Tuple<int, int>(heroX, heroY));
                mode.Draw();
            }
            else if (new_tile.Type == TileType.Enemy)
            {
                mode = battleState;
                mode.Draw();
            }


             
          
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
            else if (pos.Y >= GameRoot.Height - 1) //вниз
                cameraY++;            
            if (pos.X >= GameRoot.Width) // вправо
                cameraX++;             
            else if (pos.X <= 0) // влево
                cameraX--;
               
            DrawMap();
     
        }
       
       
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double windowWidth = this.ActualWidth;
            double windowHeight = this.ActualHeight;
            Console.WriteLine($"Ширина: {windowWidth}, Высота: {windowHeight}");
            //minimum side of screen
            double squareSize  = Math.Min(windowWidth, windowHeight);
            double infoWidth = squareSize * 0.3;

            GameRoot.Width = squareSize + infoWidth;
            GameRoot.Height = squareSize;

            map = CreateTestMap();

            AdventureState adventureState = new AdventureState(map);
            mode = adventureState;

          
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
        private void HeroFocus_click(object sender, RoutedEventArgs e)
        {
            cameraX = heroX;
            cameraY = heroY;
            DrawMap();
        }
        public void OnPanelClick(object sender, MouseButtonEventArgs e)
        {
            switch (toggle)
            {
                case 0:
                    DayPanel.Visibility = Visibility.Collapsed;
                    toggle++;
                    UnitsPanel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    UnitsPanel.Visibility = Visibility.Collapsed;
                    ResourcesPanel.Visibility = Visibility.Visible;
                    toggle++;
                    break;
                case 2:
                    ResourcesPanel.Visibility = Visibility.Collapsed;
                    toggle = 0;
                    DayPanel.Visibility = Visibility.Visible;
                    break;


            }
            
        }
        private void NextDay_click(object sender, RoutedEventArgs e)
        {
            day++;
            if(day>7)
            {
                day = 0;
                week++;
            }
            if(week>3)
            {
                week = 0;
                month++;
            }
            Day.Text = day.ToString();
            Week.Text = week.ToString();
            Month.Text = month.ToString();
        }
    }
}