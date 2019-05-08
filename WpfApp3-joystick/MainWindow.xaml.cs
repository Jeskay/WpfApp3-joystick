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
using OpenCvSharp.ML;
using WebCam_Capture;
using System.ComponentModel;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WpfApp3_joystick
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        //изображение
        private WebCamCapture RulerCam = new WebCamCapture();
        private WebCamCapture RecognitionCam = new WebCamCapture();
        DispatcherTimer VideoTimer = new DispatcherTimer();
        private Recognition Recognition = new Recognition();
        private FiguresView figuresView = new FiguresView(new FiguresModel());
        private MainView mainView = new MainView(new MainModel());
        private bool FirstWindowaCtivated = true;

        public MainWindow()
        {
            InitializeComponent();
            GoupBox_Grid.DataContext = figuresView;
        }
        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            //установка цветов некоторым элементам
            ME_test.Visibility = Visibility.Collapsed;
            RulerCam.FrameNumber = ((ulong)(0ul));
            RulerCam.TimeToCapture_milliseconds = 10;
            RulerCam.ImageCaptured += new WebCamCapture.WebCamEventHandler(firstcam_ImageCaptured);
            RecognitionCam.FrameNumber = ((ulong)(0ul));
            RecognitionCam.TimeToCapture_milliseconds = 10;
            RecognitionCam.ImageCaptured += new WebCamCapture.WebCamEventHandler(secondcam_ImageCaptured);
            //инициализация камер
            VideoTimer.Tick += new EventHandler(VTimer_Tick);
            VideoTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            VideoTimer.Start();

        }
        void firstcam_ImageCaptured(object source, WebcamEventArgs e)//M
        {
            if(FirstWindowaCtivated)mainView.RulerImage = (System.Drawing.Bitmap)e.WebCamImage;
        }
        void secondcam_ImageCaptured(object source, WebcamEventArgs e)//M
        {
            if(!FirstWindowaCtivated)mainView.RecognitionImage = (System.Drawing.Bitmap)e.WebCamImage;
        }
        public void ContinueCamera(object sender, EventArgs e)
        {
            VideoTimer.Start();
        }

        private void Keyboard_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.P)
            {
                if (ME_test.Visibility != Visibility.Visible)
                {
                    ME_test.Visibility = Visibility.Visible;
                    VideoTimer.Stop();
                    ME_test.Play();
                }
                else
                {
                    ME_test.Stop();
                    ME_test.Visibility = Visibility.Collapsed;
                    VideoTimer.Start();
                }
            }
            if (e.Key == System.Windows.Input.Key.Enter)
            {

                System.Windows.Controls.Image capturedimage = new System.Windows.Controls.Image();
                capturedimage.Source = ImageWebcam1.Source;
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
            VideoTimer.Stop();
        }
        private void ComboBox_StartP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void VTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Mat Rulerimage = new Mat();
                Mat Recognitionimage = new Mat();             
                Recognitionimage = GetMatFromSDImage(mainView.RecognitionImage);
                Rulerimage = GetMatFromSDImage(mainView.RulerImage);
                
                if (FirstWindowaCtivated) ImageWebcam1.Source = BitmapSourceConvert.ToBitmapSource(Rulerimage.ToImage<Bgr, Byte>());
                else
                {
                    Image1.Source = Recognition.FindFigures(Recognitionimage);
                    figuresView.Circles = Recognition.Circles;
                    figuresView.Lines = Recognition.Lines;
                    figuresView.Triangles = Recognition.Triangles;
                    figuresView.Squares = Recognition.Squares;
                }
            }
            catch (Exception ex)
            { }
        }

        private void Recognition_Tab_GotFocus(object sender, RoutedEventArgs e)
        {
            FirstWindowaCtivated = false;
        }

        private void Ruler_Tab_GotFocus(object sender, RoutedEventArgs e)
        {
            FirstWindowaCtivated = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double Density = Convert.ToDouble(Density_TextBox.Text);
            double R1 = Convert.ToDouble(R1_TextBox.Text);
            double R2 = Convert.ToDouble(R2_TextBox.Text);
            double R3 = Convert.ToDouble(R3_TextBox.Text);
            double V = Math.PI * Convert.ToDouble(Height_TextBox.Text) * (R1 * R1 + R1 * R2 + R2 * R2) / 3.0;
            double M = V * Density;
            OutputData_Label.Content = "V= " + V + '\n' + "M= " + M;
        }
        private Mat GetMatFromSDImage(System.Drawing.Image image)
        {
            int stride = 0;
            Bitmap bmp = new Bitmap(image);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                stride = bmp.Width * 4;
            }
            else
            {
                stride = bmp.Width * 3;
            }

            Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

            bmp.UnlockBits(bmpData);

            return cvImage.Mat;
        }
        private void Calculation_Tab_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void RecognitionCameraSelect_Button_Click(object sender, RoutedEventArgs e)
        {
            RecognitionCam.Start(1);
        }

        private void RulerCameraSelect_Button_Click(object sender, RoutedEventArgs e)
        {
            RulerCam.Start(0);
        }
    }
}
