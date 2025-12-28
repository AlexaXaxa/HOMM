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
        EnemyType enemyType;
        int TroopsCount;
        TileSkin Skin;

        public BattleView(int tileSize_Px, double YGameRoot_Px, double XGameRoot_Px, Tile EnemyTile_WithStack)
        {
            InitializeComponent();

            TileSize_Px = tileSize_Px;
            viewYSize_Tile = (int)((YGameRoot_Px) / TileSize_Px);
            viewXSize_Tile = (int)((XGameRoot_Px) / TileSize_Px);

            Skin = EnemyTile_WithStack.Skin;
            enemyType = EnemyTile_WithStack.EnemyStack.Type;
            //amount of troops on Adventurefield (in total)
            TroopsCount = EnemyTile_WithStack.EnemyStack.Amount; 

            this.Loaded += BattleView_Loaded;
        }
        private void BattleView_Loaded(object sender, RoutedEventArgs e)
        {
            uniformGrid.Rows = viewYSize_Tile;
            uniformGrid.Columns = viewXSize_Tile;

            BattleMap = CreateBattleMap();
            CreateEnemyMap();
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
        public void Draw()
        {
            uniformGrid.Children.Clear();
            BattleMap[heroX, heroY] = new Tile(TileSkin.Hero, new Tuple<int, int>(heroX, heroY));

            for (int y = 0; y < viewYSize_Tile; y++)
            {
                for (int x = 0; x < viewXSize_Tile; x++)
                {
                    int mapX = x;
                    int mapY = y;

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
                        Image Img = new Image();
                        BitmapImage myBitmapImage = new BitmapImage();
                        switch (tile.Skin)
                        {
                            case TileSkin.Grass:
                                b.Background = Brushes.LightGreen;
                                DrawNumber(b);
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
                            case TileSkin.Skeletton:
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
                        }
                        
                     
                       
                            
                    }
                    uniformGrid.Children.Add(b);
                }
            }
        }
        public void CreateEnemyMap()
        {
            int stack = TroopsCount / viewYSize_Tile;

            for (int y = 0; y < viewYSize_Tile; y=y+4)
            {
                Tile enemy = new Tile(
                    Skin, 
                    new Tuple<int, int>(viewXSize_Tile, y),
                    new BattleStack(enemyType, stack));

                BattleMap[viewXSize_Tile - 2, y] = enemy;
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
