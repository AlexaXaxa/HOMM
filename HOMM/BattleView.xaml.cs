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

namespace HOMM
{
    /// <summary>
    /// Interaction logic for BattleView.xaml
    /// </summary>
    public partial class BattleView : UserControl, IUpdatable
    {
        Tile[,] BattleMap;
        int heroX = 0;
        int heroY = 0;
        int viewYSize_Tile;
        int viewXSize_Tile;
        int TileSize_Px;
        TileSkin enemyType;
        int TroopsCountTot;
        TileSkin EnemySkin;
        int CameraX;
        int CameraY;
        
        public BattleView(int tileSize_Px, double YGameRoot_Px, double XGameRoot_Px, Tile EnemyTile_WithStack)
        {
            InitializeComponent();

            TileSize_Px = tileSize_Px;
            viewYSize_Tile = (int)((YGameRoot_Px) / TileSize_Px);
            viewXSize_Tile = (int)((XGameRoot_Px) / TileSize_Px);

            EnemySkin = EnemyTile_WithStack.Skin;
            enemyType = EnemyTile_WithStack.EnemyStack.Type;
            //amount of troops on Adventurefield (in total)
            TroopsCountTot = EnemyTile_WithStack.EnemyStack.Amount; 

            this.Loaded += BattleView_Loaded;
            CameraX = (int)viewXSize_Tile / 2;
            CameraY = (int)viewYSize_Tile / 2;
        }
        private void BattleView_Loaded(object sender, RoutedEventArgs e)
        {
            uniformGrid.Rows = viewYSize_Tile;
            uniformGrid.Columns = viewXSize_Tile;

            BattleMap = CreateBattleMap();
            CreateEnemyMap();
            CreateTroopsMap();
            Draw();
        }
        public void Update()
        {

        }
        public void DrawNumber(CustomBorder b)
        {
            TextBlock t = new TextBlock();
            t.FontSize = 15;
            t.Text = b.Tile.Coords.Item1 +","+ b.Tile.Coords.Item2.ToString();
            b.Child = t;
        }
        private void MoveHero(object sender, RoutedEventArgs e)
        {
            if (sender is CustomBorder border)
            {
                int new_heroX = border.Tile.Coords.Item1;
                int new_heroY = border.Tile.Coords.Item2;

                Tile new_tile = BattleMap[new_heroX, new_heroY];

                if ((new_tile.Skin == TileSkin.Water) || (new_tile.Skin == TileSkin.Forest) || (new_tile.Skin == TileSkin.Castle))
                {
                    //do nothing
                }
                else if (new_tile.Skin == TileSkin.Grass)
                {
                    BattleMap[heroX, heroY] = new Tile(TileSkin.Grass, new Tuple<int, int>(heroX, heroY));
                    heroX = new_heroX;
                    heroY = new_heroY;
                    BattleMap[heroX, heroY] = new Tile(TileSkin.Hero, new Tuple<int, int>(heroX, heroY));
                    Draw();
                }

            }
        }
        public void Draw()
        {
            uniformGrid.Children.Clear();
            BattleMap[heroX, heroY] = new Tile(TileSkin.Hero, new Tuple<int, int>(heroX, heroY));

            for (int y = 0; y < viewYSize_Tile; y++)
            {
                for (int x = 0; x < viewXSize_Tile; x++)
                {
                    int mapX = CameraX + x - viewXSize_Tile / 2; 
                    int mapY = CameraY + y - viewYSize_Tile / 2; ;

                    CustomBorder b = new CustomBorder();
                    b.Width = TileSize_Px;
                    b.Height = TileSize_Px;
                    b.BorderThickness = new Thickness(1);
                    b.BorderBrush = Brushes.Black;

                   
                    //b.AddHandler(Border.MouseLeftButtonDownEvent, new RoutedEventHandler(MoveHero), true);

                    if (mapX < 0 || mapX >= viewXSize_Tile || mapY < 0 || mapY >= viewYSize_Tile)
                    {
                        b.Background = Brushes.Gray; // пустое место за пределами карты
                    }
                    else
                    {
                        Tile tile = BattleMap[mapX, mapY];
                        b.Tile = tile;
                        b.AddHandler(Border.MouseLeftButtonDownEvent, new RoutedEventHandler(MoveHero), true);
                        Image Img = new Image();
                        BitmapImage myBitmapImage = new BitmapImage();
                        switch (tile.Skin)
                        {
                            case TileSkin.Grass:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\grass.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Water:
                                b.Background = Brushes.Blue;
                                DrawNumber(b);
                                break;
                            case TileSkin.Forest:
                                b.Background = Brushes.DarkGreen;
                                DrawNumber(b);
                                break;
                            case TileSkin.Castle:
                                b.Background = Brushes.Violet;
                                DrawNumber(b);
                                break;
                            case TileSkin.Enemy:
                                b.Background = Brushes.Red;
                                DrawNumber(b);
                                break;
                            case TileSkin.Hero:
                                b.Background = Brushes.Violet;
                                DrawNumber(b);
                                break;
                            case TileSkin.Vampire:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\vampire.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Skeleton:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\skeletton.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Mummy:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\mummy.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Bone_Dragon:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\bone_dragon.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                        }
                        
                     
                       
                            
                    }
                    uniformGrid.Children.Add(b);
                }
            }
        }
        public void CreateEnemyMap()
        {
            int stack = TroopsCountTot / viewYSize_Tile-4;

            for (int y = 0; y < viewYSize_Tile; y=y+4)
            {
                Tile enemy = new Tile(
                    EnemySkin, 
                    new Tuple<int, int>(viewXSize_Tile - 2, y),
                    new BattleStack(enemyType, stack));

                BattleMap[viewXSize_Tile - 2, y] = enemy;
            }
        }
        public void CreateTroopsMap()
        {
            int y = 0;
            foreach (Troops t in Globals.Inventory._troops)
            {
                Tile troop = new Tile(
                    (TileSkin)t.Type, new Tuple<int, int>(1, y), new BattleStack((TileSkin)t.Type, t.Amount));
                BattleMap[1, y] = troop;
                y = y + 4;

            }
        }
        Tile[,] CreateBattleMap()
        {
            //размер карты в тайлах
            int mapYSize_Tile = viewYSize_Tile;
            int mapXSize_Tile = viewXSize_Tile;
            Tile[,] map = new Tile[mapXSize_Tile, mapYSize_Tile];

            for (int x = 0; x < mapXSize_Tile; x++)
            {
                for (int y = 0; y < mapYSize_Tile; y++)
                {
                    map[x, y] = new Tile(TileSkin.Grass, new Tuple<int, int>(x, y));
                }
            }
            return map;
        }
    }
}
