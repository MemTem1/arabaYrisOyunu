using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace HIZLI_AMA_SAKİN
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            timerProgressBar.Start();
            yukleniyor();
            
        }
        int yuklenGit = 10;
        PictureBox yuklen = new PictureBox();
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cıksınMi = MessageBox.Show("oyundan çıkmak istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(cıksınMi == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }
        private void yukleniyor()
        {
            yuklen.Image = Properties.Resources.yandan_kırmızı_araba;
            yuklen.Size = new Size(50, 50);
            yuklen.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(yuklen);
            yuklen.BackColor = Color.Transparent;
        }

        private void timerProgressBar_Tick(object sender, EventArgs e)
        {
            yuklen.Location = new Point(yuklenGit, 400);
            yuklenGit += 8;
            if (yuklen.Left > 910)
            {
                this.Controls.Remove(yuklen);
                timerProgressBar.Stop();
                Form1 fr1 = new Form1
                {
                    isim = textBox1.Text
                };
                fr1.ShowDialog();
                this.Hide();
            }

        }

        private void progressBarYukle_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        Form3 fr3; 
        private void button3_Click(object sender, EventArgs e)
        {
            fr3 = new Form3();
            fr3.ShowDialog();

        }
    }
}
