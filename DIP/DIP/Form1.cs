using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DIP
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        Bitmap imgA, imgB, colorgreen;
        ImageProcessing img = new ImageProcessing();        
        public Form1()
        {
            InitializeComponent();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {

        }

        private async void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = await img.GreyScale(loaded);
            pictureBox2.Image = processed;
        }

        private async void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = await img.BasicCopy(loaded);
            pictureBox2.Image = processed;
        }

        private async void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = await img.Histogram(loaded);
            pictureBox2.Image = processed;
        }

        private async void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = await img.Sepia(loaded);
            pictureBox2.Image = processed;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imgB = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imgB;
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imgA = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imgA;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            imgA = (Bitmap)pictureBox2.Image;
            imgB = (Bitmap)pictureBox1.Image;

            pictureBox3.Image = await img.Subtract(imgA, imgB);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Image";
                saveFileDialog.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    processed.Save(saveFileDialog.FileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private async void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = await img.ColorInversion(loaded);
            pictureBox2.Image = processed;
        }
    }
}