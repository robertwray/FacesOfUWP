using System;
using System.Drawing;
using System.Windows.Forms;

namespace FacesOfUWP.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnChooseImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PNG Files (*.png)|*.png";
            openFileDialog1.ShowDialog();

            var fileStream = openFileDialog1.OpenFile();

            #if DOTNETCORE

            var facialRecognition = new FacialRecognition();
            var faces = await facialRecognition.Recognise(fileStream);
            lblNumberOfFaces.Text = string.Format("The number of faces found is: {0}", faces.Faces);
            pictureBox1.Image = Image.FromStream(faces.FirstFace);
            
            #endif

        }
    }
}
