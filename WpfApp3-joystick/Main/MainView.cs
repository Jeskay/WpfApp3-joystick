using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace WpfApp3_joystick
{
    public class MainView
    {
        MainModel mainModel;

        public Image RecognitionImage
        {
            get { return mainModel.FirstImage; }
            set { mainModel.FirstImage = value; }
        }
        public Image RulerImage
        {
            get { return mainModel.SecondImage; }
            set { mainModel.SecondImage = value; }
        }

        public MainView(MainModel mainModel)
        {
            this.mainModel = mainModel;
        }
    }
}
