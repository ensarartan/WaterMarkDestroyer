using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resim_isleme
{
    public partial class Form1 : Form
    {
        static Color GetPixel(Point position)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }


        public Form1()
        {
            InitializeComponent();

        }
        Bitmap resim;


        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label8.Text = "Silmek istediğiniz rengi seçin ve \nişleyin.İşlem biraz zaman alabilir.";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap Dosyaları (*.bmp)|*.bmp|Jpeg Dosyaları (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                resim = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
            }
            pictureBox1.Image = resim;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
          
            try
            {
                int x, y;
                for (x = 0; x < resim.Width; x++)
                {
                    for (y = 0; y < resim.Height; y++)
                    {
                        Color pixelColor = resim.GetPixel(x, y);
                        Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);

                        if (pixelColor.R <= Convert.ToInt32(label2.Text) && pixelColor.R >= Convert.ToInt32(label2.Text) 
                            || pixelColor.G <= Convert.ToInt32(label3.Text)  && pixelColor.G >= Convert.ToInt32(label3.Text) 
                            || pixelColor.B <= Convert.ToInt32(label4.Text)  && pixelColor.B >= Convert.ToInt32(label4.Text) )
                        {
                            resim.SetPixel(x, y, Color.White);
                        }


                    }
                }
                // Görüntümüz pictureboxda gösteriliyor
                pictureBox1.Image = resim;

                // Piksel Formatları label1 de gösteriliyor
               
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resim.Save(@"C:\Users\User\Desktop\MasaustuDersleri (1)\Sonuç\cikti.jpg");
            MessageBox.Show("Resim Kaydedildi!");
        }




        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs rato = e as MouseEventArgs;
            Bitmap b = ((Bitmap)pictureBox1.Image);
            Bitmap b2 = ((Bitmap)pictureBox2.Image);
            int x = rato.X * b.Width / pictureBox1.ClientSize.Width;
            int y = rato.Y * b.Height / pictureBox1.ClientSize.Height;
            Color c = b.GetPixel(x, y);
            label2.Text = c.R.ToString();
            label3.Text = c.G.ToString();
            label4.Text = c.B.ToString();
            pictureBox2.BackColor = GetPixel(e.Location);



        }


    }
}
