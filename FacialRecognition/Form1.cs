using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Vision.Detection;
using DirectShowLib;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FacialRecognition
{
    public partial class Form1 : Form
    {
        VideoCapture capture;

        public Form1()
        {
            InitializeComponent();
            capture = new VideoCapture(0);
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    

    private void ProcessFrame(object sender, EventArgs e)
        {
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier =
                new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_alt2.xml");
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {
                    var grayframe = imageFrame.Convert<Gray, byte>();
                    var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10,
                        Size.Empty); //the actual face detection happens here


                    foreach (var face in faces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood),
                            3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }

                pictureBox1.Image = imageFrame.ToBitmap();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FinalFrame = new VideoCaptureDevice(captureDevice[comboBox1.SelectedIndex].MonikerString);
            //FinalFrame.NewFrame+= new NewFrameEventHandler(FinalFrame_NewFrame);
            //FinalFrame.Start();

            if (button1.Text == "Pause")
            {
                button1.Text = "Resume";
                Application.Idle -= ProcessFrame;
            }
            else
            {
                button1.Text = "Pause";
                Application.Idle += ProcessFrame;
            }
        }
    }
}
