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
using System.Windows.Shapes;

namespace WpfApp3_joystick
{
    /// <summary>
    /// Логика взаимодействия для MapDialogBox.xaml
    /// </summary>
    public partial class MapDialogBox : Window
    {
        public MapDialogBox()
        {
            InitializeComponent();
        }
        public double AscentVector;
        public int AscentDegrees;
        public double WindVector;
        public int WindDirection;
        public int Transfer;

        public Line Line1;
        public Line Line2;
        public Ellipse Circle;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow.Close();
        }
        // отрисовка места падения
        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position1 = System.Windows.Input.Mouse.GetPosition(null);
            Console.WriteLine("X " + position1.X + "Y " + position1.Y);
            if ((bool)ThirdSelection_RB.IsChecked) DrawOnMap(position1.X,position1.Y,AscentVector,WindVector,AscentDegrees,WindDirection);
        }

        private void DialogWin_Loaded(object sender, RoutedEventArgs e)
        {
            Line1 = new Line();
            Line2 = new Line();
            Circle = new Ellipse
            {
                Width = 5,
                Height = 5,
                StrokeThickness = 1,
                Stroke = Brushes.Red
            };
            Line1.StrokeThickness = 1;
            Line2.StrokeThickness = 1;
            DialogGrid.Children.Add(Line1);
            DialogGrid.Children.Add(Line2);
            DialogGrid.Children.Add(Circle);
            Line1.Stroke = Brushes.Blue;
            Line2.Stroke = Brushes.Red;
            if (Transfer == 1)
            {
                ComboBox_Position.SelectedItem = FirstSelection_RB;
                FirstSelection_RB.IsChecked = true;
            }
            else if (Transfer == 2)
            {
                ComboBox_Position.SelectedItem = SecondSelection_RB;
                SecondSelection_RB.IsChecked = true;
            }
            else
            {
                ComboBox_Position.SelectedItem = ThirdSelection_RB;
                ThirdSelection_RB.IsChecked = true;
            }
        }

        private void TextBox_Kf_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_Position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FirstSelection_RB_Checked(object sender, RoutedEventArgs e)
        {
            DrawOnMap(166,469,AscentVector,WindVector,AscentDegrees,WindDirection);
            ComboBox_Position.SelectedItem = FirstSelection_RB;
        }

        private void SecondSelection_RB_Checked(object sender, RoutedEventArgs e)
        {
            DrawOnMap(870,390,AscentVector,WindVector,AscentDegrees,WindDirection);
            ComboBox_Position.SelectedItem = SecondSelection_RB;
        }
        public void DrawOnMap(double x1, double y1, double lenght1, double lenght2, double angle1, double angle2)
        {
            //отрисовка 1 линии
            Line1.X1 = x1;
            Line1.Y1 = y1;
            double AscentAngle = angle1 / 180.0 * Math.PI;
            Line1.Y2 = Line1.Y1 + Math.Sin(AscentAngle) * 0.037 * lenght1;
            Line1.X2 = Line1.X1 + Math.Cos(AscentAngle) * 0.037 * lenght1;

            //отрисовка 2 линии
            Line2.X1 = Line1.X2;
            Line2.Y1 = Line1.Y2;
            double WindAngle = angle2 / 180.0 * Math.PI;
            Line2.Y2 = Line2.Y1 + Math.Sin(WindAngle) * 0.037 * lenght2;
            Line2.X2 = Line2.X1 + Math.Cos(WindAngle) * 0.037 * lenght2;

            Circle.HorizontalAlignment = HorizontalAlignment.Left;
            Circle.VerticalAlignment = VerticalAlignment.Top;
            Circle.Margin = new Thickness(left:Line2.X2, top:Line2.Y2, right:0, bottom:0);
            Console.WriteLine("X1line: " + Line1.X1 + ", X2Line: " + Line1.X2);
            Console.WriteLine("X1line: " + Line2.X1 + ", X2Line: " + Line2.X2);
        }
        private void ThirdSelection_RB_Checked(object sender, RoutedEventArgs e)
        {
            ComboBox_Position.SelectedItem = ThirdSelection_RB;
        }
    }
}
