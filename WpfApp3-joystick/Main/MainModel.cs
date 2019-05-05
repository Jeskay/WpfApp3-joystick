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
    public class MainModel
    {
        private Image firstimage;
        private Image seconimage;

        public Image FirstImage
        {
            get { return firstimage; }
            set { firstimage = value; }
        }
        public Image SecondImage
        {
            get { return seconimage; }
            set { seconimage = value; }
        }
    }
}
