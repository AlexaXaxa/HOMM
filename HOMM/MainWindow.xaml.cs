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
    public partial class MainWindow : Window
    {
        Tile[,]? map;
        static int mapSize_Tile = 7;
        int tileSize_Px = 40;
     
        IUpdatable? currentView;
        AdventureView? adventureView;
        BattleView? battleView;
      
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            

        }
     
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double windowWidth = this.ActualWidth;
            double windowHeight = this.ActualHeight;
            double minimumSideOfsceen  = Math.Min(windowWidth, windowHeight);
            GameRoot.Width = minimumSideOfsceen + minimumSideOfsceen*0.3;
            GameRoot.Height = minimumSideOfsceen;

            map = CreateTestMap();
            adventureView = new AdventureView(map, minimumSideOfsceen, tileSize_Px, mapSize_Tile);

            SetView(adventureView);
            adventureView.EnemyEncountered += EnemyEncountered;

         


            StartTimer();
            
        }
        void EnemyEncountered(object? sender, EventArgs e)
        {
            Tile tile = sender as Tile;
            battleView = new BattleView(tileSize_Px, GameRoot.Height, GameRoot.Width, tile);
            SetView(battleView);
        }
        void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(160); //160
            timer.Tick += GameTick;
            timer.Start();
        }
        public void SetView(UserControl view)
        {
            currentView = (IUpdatable)view;
            GameRoot.Children.Clear();
            GameRoot.Children.Add(view);
        }
        void GameTick(object? sender, EventArgs e)
        {
            currentView?.Update();
            
            
        }

        Tile[,] CreateTestMap()
        {
            //размер карты в тайлах
            int sizeOfMap = mapSize_Tile;
            Tile[,] map = new Tile[sizeOfMap, sizeOfMap];

            for (int y = 0; y < sizeOfMap; y++)
            {
                for (int x = 0; x < sizeOfMap; x++)
                {
                    map[x, y] = new Tile(TileSkin.Grass, new Tuple<int, int>(x, y));
                }
            }

            #region Enemy
            map[5, 1] = new Tile(TileSkin.Skeleton, new Tuple<int, int>(5, 1), new Stack(TileSkin.Skeleton, 9));
            map[6, 2] = new Tile(TileSkin.Mummy, new Tuple<int, int>(6, 2), new Stack(TileSkin.Mummy, 9));
            map[3, 5] = new Tile(TileSkin.Vampire, new Tuple<int, int>(3, 5), new Stack(TileSkin.Vampire, 9));
            #endregion

            #region Water
            map[6, 6] = new Tile(TileSkin.Water, new Tuple<int, int>(6, 6));
            map[6, 5] = new Tile(TileSkin.Water, new Tuple<int, int>(6, 5));
            map[6, 4] = new Tile(TileSkin.Water, new Tuple<int, int>(6, 4));
            map[5, 6] = new Tile(TileSkin.Water, new Tuple<int, int>(5, 6));
            map[5, 5] = new Tile(TileSkin.Water, new Tuple<int, int>(5, 5));
            map[4, 6] = new Tile(TileSkin.Water, new Tuple<int, int>(4, 6));
            #endregion

            #region Forest
            map[0, 6] = new Tile(TileSkin.Forest, new Tuple<int, int>(0, 6));
            map[0, 5] = new Tile(TileSkin.Forest, new Tuple<int, int>(0, 5));
            map[0, 4] = new Tile(TileSkin.Forest, new Tuple<int, int>(0, 4));
            map[1, 6] = new Tile(TileSkin.Forest, new Tuple<int, int>(1, 6));
            map[1, 5] = new Tile(TileSkin.Forest, new Tuple<int, int>(1, 5));
            map[2, 6] = new Tile(TileSkin.Forest, new Tuple<int, int>(2, 6));
            map[0, 3] = new Tile(TileSkin.Forest, new Tuple<int, int>(0, 3));
            map[1, 4] = new Tile(TileSkin.Forest, new Tuple<int, int>(1, 4));
            map[1, 3] = new Tile(TileSkin.Forest, new Tuple<int, int>(1, 3));
            #endregion

            #region Castle
            map[0, 0] = new Tile(TileSkin.Castle, new Tuple<int, int>(0, 0));
            map[0, 1] = new Tile(TileSkin.Castle, new Tuple<int, int>(0, 1));
            map[0, 2] = new Tile(TileSkin.Castle, new Tuple<int, int>(0, 2));
            map[1, 0] = new Tile(TileSkin.Castle, new Tuple<int, int>(1, 0));
            map[1, 1] = new Tile(TileSkin.Castle, new Tuple<int, int>(1, 1));
            map[1, 2] = new Tile(TileSkin.Castle, new Tuple<int, int>(1, 2));
            map[2, 0] = new Tile(TileSkin.Castle, new Tuple<int, int>(2, 0));
            map[2, 1] = new Tile(TileSkin.Castle, new Tuple<int, int>(2, 1));
            map[2, 2] = new Tile(TileSkin.Castle, new Tuple<int, int>(2, 2));
            #endregion

            return map;
        }
 
    }
}