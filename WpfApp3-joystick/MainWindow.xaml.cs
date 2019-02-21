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
      
        //изображение
        private WebCamCapture webcam;
        private System.Windows.Controls.Image _FrameImage;
        
        public Line myLine;
        public Line my2Line;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage myBitmapImage = new BitmapImage();//создание битмапа для хранения изображения
            //установка цветов некоторым элементам
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

        public void ContinueCamera(object sender, EventArgs e)
        {
            webcam.Start(0);
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
                capturedimage.MaxWidth = 60;
                capturedimage.MaxHeight = 60;
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
            selectedImage.WindowClosed += ContinueCamera;
            selectedImage.Show();
            webcam.Stop();
        }
        private void ComboBox_StartP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
    }
}
