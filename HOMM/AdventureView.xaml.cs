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
    /// Interaction logic for AdventureView.xaml
    /// </summary>
    public partial class AdventureView : UserControl
    {
        public AdventureView(Tile[,] map)
        {
            InitializeComponent();
            this.Loaded += AdventureView_Loaded;
        }

        private void AdventureView_Loaded(object sender, RoutedEventArgs e)
        {
            MapColumn.Width = new GridLength(squareSize);
            InfoColumn.Width = new GridLength(infoWidth);

            MapSquare.Width = squareSize;
            MapSquare.Height = squareSize;

            viewSize_Box = (int)(squareSize / sizeOfSquare_Px);
        }

        public void DrawNumber(CustomBorder b)
        {
            TextBlock t = new TextBlock();
            t.FontSize = 15;
            t.Text = b.Tile.Coords.Item1 + b.Tile.Coords.Item2.ToString();
            b.Child = t;
        }
        public void Draw()
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
    }
}
