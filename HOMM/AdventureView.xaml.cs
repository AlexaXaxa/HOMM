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
//using static System.Net.Mime.MediaTypeNames;

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
        int toggle = 1;
        int day = 0;
        int week = 0;
        int month = 0;
        
    
        public AdventureView(Tile[,] map, double minimumSizeOfsceen, int tileSize_Px, int mapSize_Tile)
        {
            InitializeComponent();
            viewSize_Px = minimumSizeOfsceen;
            this.tileSize_Px = tileSize_Px;
            //Adventure map
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

            //Start with 3 Bone Dragons
            Inventory inventory = new Inventory();
            Globals.Inventory = inventory;
            Globals.Inventory.Add(TileSkin.Skeleton, 10);

            Draw();
            var window = Window.GetWindow(this);
            window.KeyDown += HeroFocus;
        }
        public void DisplayInventory()
        {
            UnitsWrapPanel.Children.Clear();
            foreach (Troops troop in Globals.Inventory._troops)
            {
                //Width, Heoght?
                StackPanel p = new StackPanel();
                p.Orientation = Orientation.Horizontal;
                TextBlock type = new TextBlock();
                type.Text = troop.Type.ToString() + " ";
                TextBlock amount = new TextBlock();
                amount.Text = troop.Amount.ToString() + "   ";
                p.Children.Add(type);
                p.Children.Add(amount);
                UnitsWrapPanel.Children.Add(p);
            }
            
        }

        public void DisplayInfo(CustomBorder b)
        {
            TextBlock t = new TextBlock();
            t.FontSize = 15;
            t.Text = b.Tile.Coords.Item1 + "," + b.Tile.Coords.Item2.ToString();
            if (b.Tile.Skin == TileSkin.Enemy)
            {
                StackPanel p = new StackPanel();
                b.Child = p;
                TextBlock t2 = new TextBlock();
                t2.FontSize = 7;
                t2.Text = b.Tile.EnemyStack.Type + " " + b.Tile.EnemyStack.Amount;
                p.Children.Add(t);
                p.Children.Add(t2);
                return;

            }        
            b.Child = t;
        }
        public void Draw()
        {
            uniformGrid.Children.Clear();

            Map[heroX, heroY] = new Tile(TileSkin.Hero, new Tuple<int, int>(heroX, heroY));

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
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\water.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Forest:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\forest.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Castle:
                                b.Background = Brushes.Violet;
                                DisplayInfo(b);
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
                            case TileSkin.Vampire:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\vampire.png");
                                myBitmapImage.EndInit();
                                Img.Source = myBitmapImage;
                                b.Child = Img;
                                break;
                            case TileSkin.Hero:
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(@"C:\Users\06aleden_edu.uppland\Source\Repos\HOMM_scenes\HOMM\img\hero.png");
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
        private void MoveHero(object sender, RoutedEventArgs e)
        {
            if (sender is CustomBorder border)
            {
                int new_heroX = border.Tile.Coords.Item1;
                int new_heroY = border.Tile.Coords.Item2;

                Tile new_tile = Map[new_heroX, new_heroY];

                if ((new_tile.Skin == TileSkin.Water) || (new_tile.Skin == TileSkin.Forest) || (new_tile.Skin == TileSkin.Castle))
                {
                    //do nothing
                }
                else if (new_tile.Skin == TileSkin.Grass)
                {
                    Map[heroX, heroY] = new Tile(TileSkin.Grass, new Tuple<int, int>(heroX, heroY));
                    heroX = new_heroX;
                    heroY = new_heroY;
                    Map[heroX, heroY] = new Tile(TileSkin.Hero, new Tuple<int, int>(heroX, heroY));
                    Draw();
                }
                else if (new_tile.EnemyStack != null)
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
            if (pos.X >= MapSquare.Width + MapSquare.Width*0.3) // вправо
                CameraX++;
            else if (pos.X <= 0) // влево
                CameraX--;

            Draw();
            DisplayInventory();
        }

        private void HeroFocus(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.H)
            {
                CameraX = heroX;
                CameraY = heroY;
            }
        }
        private void HeroFocus_Click(object sender, RoutedEventArgs e)
        {
            CameraX = heroX;
            CameraY = heroY;
        }
        public void OnPanelClick(object sender, MouseButtonEventArgs e)
        {
            switch (toggle)
            {
                case 0:
                    DayPanel.Visibility = Visibility.Collapsed;
                    toggle++;
                    UnitsWrapPanel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    UnitsWrapPanel.Visibility = Visibility.Collapsed;
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
            if (day > 7)
            {
                day = 0;
                week++;
            }
            if (week > 3)
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
