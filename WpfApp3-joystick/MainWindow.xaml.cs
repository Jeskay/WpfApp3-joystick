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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Windows.Interop;
using OpenCvSharp;
using OpenCvSharp.ML;
using WebCam_Capture;
using System.ComponentModel;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Diagnostics;


namespace WpfApp3_joystick
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        //линейка
        double x, y, x1, y1, x2, y2, x3, y3, x4, y4;
        double k;
        double distance;
        bool Mouseclick = false;//клавиша нажата
        bool IsBoxChecked = false;//если был проверен checkbox

        //изображение
        private WebCamCapture webcam;
        private System.Windows.Controls.Image _FrameImage;

        int mark = 0;//если таймер положительный
        int mark2 = 0;//значение таймера отрицательное
        public Line myLine;
        public Line my2Line;

        int ImageCounter = 0;

        private void MainWin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(e.LeftButton);
            Mouseclick = true;
            if (Mouseclick)
            {
                //начало первой линии
                if ( e.LeftButton == MouseButtonState.Pressed)
                {
                    x1 = x;
                    y1 = y;
                    mark = 1;
                    myLine.X1 = x1;
                    myLine.Y1 = y1;
                }
                //начало второй линии
                if ( e.RightButton == MouseButtonState.Pressed)
                {
                    x3 = x;
                    y3 = y;
                    mark = 3;
                    my2Line.X1 = x3;
                    my2Line.Y1 = y3;
                }
                if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    myLine.X1 = 0;
                    myLine.Y1 = 0;
                    myLine.X2 = 0;
                    myLine.Y2 = 0;
                    my2Line.X1 = 0;
                    my2Line.Y1 = 0;
                    my2Line.X2 = 0;
                    my2Line.Y2 = 0;
                }
            }
        }

        private void MainWin_MouseMove(object sender, MouseEventArgs e)
        {
            //получение координат текущей позиции мыши и привязка к ней конца линии
            System.Windows.Point position = System.Windows.Input.Mouse.GetPosition(null);
            x = position.X;
            y = position.Y;

            //конец первой линии
            if ( e.LeftButton == MouseButtonState.Pressed)
            {
                myLine.X2 = x;
                myLine.Y2 = y;
                k = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) / distance;
                TextBox1.Text = "range = " + Math.Round(Math.Sqrt((x4 - x3) * (x4 - x3) + (y4 - y3) * (y4 - y3)) / k);//вывод результата
            }
            //конец второй линии
            if (e.RightButton == MouseButtonState.Pressed)
            {
                my2Line.X2 = x;
                my2Line.Y2 = y;
                try
                {
                    distance = Convert.ToDouble(TextBox2.Text);
                }
                catch (Exception ex)
                {
                    distance = 50;
                }
                x4 = x;
                y4 = y;
                k = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) / distance;
                TextBox1.Text = "range = " + Math.Round(Math.Sqrt((x4 - x3) * (x4 - x3) + (y4 - y3) * (y4 - y3)) / k);//вывод результата

            }
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage myBitmapImage = new BitmapImage();//создание битмапа для хранения изображения
            //установка цветов некоторым элементам
            Label_0.Background = System.Windows.Media.Brushes.Lavender;            
            GroupBox_Ruler.BorderBrush = System.Windows.Media.Brushes.SteelBlue; 
            Button2.Background = System.Windows.Media.Brushes.SkyBlue;
            Form1.Background = System.Windows.Media.Brushes.LightSteelBlue;
            ME_test.Visibility = Visibility.Collapsed;

            //инициализация камеры и очистка текстбоксов
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = 30;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageWebcam1;

            //инициализация линий
            myLine = new Line();
            myLine.StrokeThickness = 1;
            myGrid.Children.Add(myLine);
            my2Line = new Line();
            my2Line.StrokeThickness = 1;
            myLine.Stroke = System.Windows.Media.Brushes.Blue;
            my2Line.Stroke = System.Windows.Media.Brushes.Red;
            myGrid.Children.Add(my2Line);

            webcam.Start(0);//запуск потока видео

        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_N_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Cp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_p_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_WS_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Keyboard_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.P)
            {
                if (ME_test.Visibility != Visibility.Visible)
                {
                    ME_test.Visibility = Visibility.Visible;
                    webcam.Stop();
                    ME_test.Play();
                }
                else
                {
                    ME_test.Stop();
                    ME_test.Visibility = Visibility.Collapsed;
                    webcam.Start(0);
                }
            }
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                
                System.Windows.Controls.Image capturedimage = new System.Windows.Controls.Image();
                capturedimage.Source = _FrameImage.Source;
                capturedimage.MaxWidth = 40;
                capturedimage.MaxHeight = 40;
                capturedimage.MouseDown += capturedimage_MouseDown;
                Images_Box.Items.Add(capturedimage);

            }
        }
        private void capturedimage_MouseDown(object sender, RoutedEventArgs e)
        {
            SelectedImage selectedImage = new SelectedImage();
            System.Windows.Controls.Image mainimage = (System.Windows.Controls.Image)sender;
            selectedImage.Owner = this;
            selectedImage.Selected_Image.Source = mainimage.Source;
            selectedImage.Show();
            webcam.Stop();
        }
        private void ComboBox_StartP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_WA_Checked(object sender, RoutedEventArgs e)
        {
            IsBoxChecked = true;
        }


        // кнопка остановки потока видео
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (mark2 == 0)
            {
                webcam.Stop();
                mark2 = 1;
            }
            else
            {
                webcam.Start(0);
                mark2 = 0;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        //класс для выделения памяти для хранения изображения
        class Helper
        {
            //Block Memory Leak
            [System.Runtime.InteropServices.DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr handle);
            public static BitmapSource bs;
            public static IntPtr ip;
            public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
            {

                ip = source.GetHbitmap();

                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,

                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ip);

                return bs;

            }
        }
        void webcam_ImageCaptured(object source, WebcamEventArgs e)//запись изображения
        {
            _FrameImage.Source = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Form1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouseclick = false;
            System.Windows.Point position = System.Windows.Input.Mouse.GetPosition(null);//запись позиции мыши
            Console.WriteLine(e.MiddleButton);
            if (e.LeftButton == MouseButtonState.Released)//конец 2 линии
            {
                x4 = x;
                y4 = y;
                try
                {
                    distance = Convert.ToDouble(TextBox2.Text);
                }
                catch (Exception ex)
                {
                    distance = 50;
                }
                k = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) / distance;
                TextBox1.Text = "range = " + Math.Round(Math.Sqrt((x4 - x3) * (x4 - x3) + (y4 - y3) * (y4 - y3)) / k);//вывод результата
                mark = 0;
            }

            if (e.LeftButton == MouseButtonState.Released)//конец 1 линии
            {
                x2 = x;
                y2 = y;
                mark = 2;
            }
        }

        private void MainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
