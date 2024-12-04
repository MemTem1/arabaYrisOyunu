using HIZLI_AMA_SAKİN.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIZLI_AMA_SAKİN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DBGame.accdb");
        public string isim = "oyuncu";
        int geriSayim = 3;
        SoundPlayer girisMıusic;
        SoundPlayer kazaSesi;
        SoundPlayer kornaSesi;
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void verileriKayitEt()
        {
            baglanti.Open();
            string veriEkle = "INSERT INTO oyuncular (ad,score) VALUES (@ad , @score)";
            OleDbCommand cmd = new OleDbCommand(veriEkle, baglanti);
            cmd.Parameters.AddWithValue("@ad", labelAd.Text);
            cmd.Parameters.AddWithValue("@score",labelRoad.Text );
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
        int seritSayisi = 1, road = 0, speed = 70;

        Random R = new Random();
        
        class Random_Car
        {
            public bool FakeHaveCar = false;
            public PictureBox fakeCar;
            public bool vakit = false;

        }
        Random_Car[] rndCar = new Random_Car[2];

        void BringRandomCar(PictureBox pb)
        {
            int rnd  =R.Next(0,5);

            switch (rnd)
            {
                case 0:
                    pb.Image = Properties.Resources.car0;
                    break;

                case 1:
                    pb.Image = Properties.Resources.car1;
                    break;

                case 2:
                    pb.Image = Properties.Resources.car2;
                    break;

                case 3:
                    pb.Image = Properties.Resources.car3;
                    break;
                case 4:
                    pb.Image = Properties.Resources.pngwing_com;
                    break;
            }
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AracYerine()
        {
            if(seritSayisi == 1)
            {
                RedCar.Location = new Point(300, 456);
            }
            else if(seritSayisi == 0)
            {
                RedCar.Location = new Point(70, 450);
            }
            else if (seritSayisi == 2)
            {
                RedCar.Location = new Point(540, 456);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (seritSayisi < 2)
                    seritSayisi++;

            }
            else if(e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if(seritSayisi > 0)
                    seritSayisi--;
            }
            if(e.KeyCode == Keys.G)
            {
                kornaSesi = new SoundPlayer();
                kornaSesi.SoundLocation = @"C:\Users\KAYIP BALIKMEMO\Desktop\C# ÖDEV\HIZLI AMA SAKİN\HIZLI AMA SAKİN\korna.wav";
                //kornaSesi.Play();
            }
            
            AracYerine();
        }
        private void RandomMusicEkle()
        {
                      
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
             girisMıusic = new SoundPlayer();
            girisMıusic.SoundLocation = @"C:\Users\KAYIP BALIKMEMO\Desktop\C# ÖDEV\HIZLI AMA SAKİN\HIZLI AMA SAKİN\track5.wav";
           // girisMıusic.Play();
            verileriKayitEt();
            labelAd.Text = isim.ToUpper();
            
                for (var i = 0; i < rndCar.Length; i++)
                {
                    rndCar[i] = new Random_Car();
                }
                rndCar[0].vakit = true;

                AracYerine();
                RandomMusicEkle();
                labelHighScore.Text = Settings.Default.HighScore.ToString();
                timerRandomCar.Start();
                timerSerit.Start();

        }
        bool sesKontrol = true;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(sesKontrol == true)
            {
                sesKontrol = false;
               
                pictureBox2.Image = Properties.Resources.volumeOff;
            }
            else if(sesKontrol == false)
            {
                sesKontrol = true;  
                
                pictureBox2.Image = Properties.Resources.volumeON;
            }
        }

        private void timerRandomCar_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rndCar.Length; i++)
            {
                if (!rndCar[i] .FakeHaveCar && rndCar[i].vakit)
                {
                    rndCar[i].fakeCar = new PictureBox();
                    BringRandomCar(rndCar[i].fakeCar);
                    rndCar[i].fakeCar.Size = new Size(100,200);
                    rndCar[i].fakeCar.Top = -rndCar[i].fakeCar.Height;

                    int SeriteYerlestir = R.Next(0, 3);

                    if (SeriteYerlestir == 0)
                    {
                        rndCar[i].fakeCar.Left = 55;
                    }
                    else if (SeriteYerlestir == 1)
                    {
                        rndCar[i].fakeCar.Left = 300;
                    }
                    else if (SeriteYerlestir == 2)
                    {
                        rndCar[i].fakeCar.Left = 550;
                    }
                    
                    this.Controls.Add(rndCar[i].fakeCar);
                    rndCar[i].FakeHaveCar = true;
                }
                else
                {
                    if (rndCar[i].vakit)
                    {
                        rndCar[i].fakeCar.Top += 5;
                        if (rndCar[i].fakeCar.Top >= 250)
                        {
                            for(int j = 0;j < rndCar.Length; j++)
                            {
                                if (!rndCar[j].vakit)
                                {
                                    rndCar[j].vakit = true;
                                    break;
                                }
                            }
                        }
                        if (rndCar[i].fakeCar.Top >= this.Height - 20)
                        {
                            rndCar[i].fakeCar.Dispose();
                            rndCar[i].FakeHaveCar = false;
                            rndCar[i].vakit = false;
                        }

                    }

                }

                // KAZA DURUMU
                if (rndCar[i].vakit)
                {
                    float mutlakX = Math.Abs((RedCar.Left + (RedCar.Width / 2)) - (rndCar[i].fakeCar.Left + (rndCar[i].fakeCar.Width / 2)));
                    float MutlakY = Math.Abs((RedCar.Top + (RedCar.Height / 2)) - (rndCar[i].fakeCar.Top + (rndCar[i].fakeCar.Height / 2)));
                    float FarkGenislik = (RedCar.Width / 2) + (rndCar[i].fakeCar.Width / 2);
                    float FarkYukseklik = (RedCar.Height / 2) + (rndCar[i].fakeCar.Height / 2);

                    if((FarkGenislik > mutlakX) && (FarkYukseklik > MutlakY))
                    {
                        timerRandomCar.Enabled = false;
                        timerSerit.Enabled = false;
                        kornaSesi = new SoundPlayer();
                        kornaSesi.SoundLocation = @"C:\Users\KAYIP BALIKMEMO\Desktop\C# ÖDEV\HIZLI AMA SAKİN\HIZLI AMA SAKİN\crash2.wav";
                       // kornaSesi.Play();
                       // kornaSesi.Load();


                        if (road > Settings.Default.HighScore)
                        {
                            MessageBox.Show("yeni Rekor Mesafe = " + road.ToString(), " m", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Settings.Default.HighScore = road;
                            Settings.Default.Save();
                        }
                        verileriKayitEt();
                        
                        Form3 frScore = new Form3();
                        frScore.ShowDialog();
                        DialogResult dr = MessageBox.Show("Oyun Bitti ! Yeniden başlasın mI", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(dr == DialogResult.Yes)
                        {
                            AracYerine();
                            for (int j = 0; j < rndCar.Length; j++)
                            {
                                rndCar[j].fakeCar.Dispose();
                                rndCar[j].FakeHaveCar = false;
                                rndCar[j].vakit = false;

                            }
                            
                            road = 0;
                            speed = 100;
                            rndCar[0].vakit = true;
                            timerRandomCar.Enabled = true;
                            timerRandomCar.Interval = 20;
                            pictureBox2.Image = Properties.Resources.volumeON;
                            timerSerit.Enabled = true;
                            timerSerit.Interval = 100;


                            RandomMusicEkle();


                            labelHighScore.Text = Settings.Default.HighScore.ToString();
                        }
                        else
                        {
                            Application.Exit();
                        }

                    }
                }


            }
            
        }

        private void RedCar_Click(object sender, EventArgs e)
        {

        }
        bool oyunDur ;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (oyunDur == true)
            { 
                
                timerRandomCar.Start();
                timerSerit.Start();
                oyunDur = false;
                
                pictureBox1.Image = Properties.Resources.pause_512;
                pictureBox2.Visible = true;
                button1.Visible = false;
                buttonYenidenBasla.Visible = false;
                buttonDevamEt.Visible = false;

            }
            else if (oyunDur == false)
            {
                timerRandomCar.Stop();
                timerSerit.Stop();
                oyunDur = true;

                pictureBox1.Image = Properties.Resources.pngwing_com__5_;
                pictureBox2.Image = Properties.Resources.volumeON;
                pictureBox2.Visible = false;
                button1.Visible = true;
                buttonYenidenBasla.Visible = true;
                buttonDevamEt.Visible = true;
               
            }
        }

        bool seritHareket = false;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        
        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonDevamEt_Click(object sender, EventArgs e)
        {
            timerRandomCar.Start();
            timerSerit.Start();
            button1.Visible=false;
            buttonDevamEt.Visible=false;
            buttonYenidenBasla.Visible=false;
            pictureBox1.Image = Properties.Resources.pause_512;
            pictureBox2.Visible = true;

        }

        private void buttonYenidenBasla_Click(object sender, EventArgs e)
        {
            timerRandomCar.Start();
            timerSerit.Start();
            button1.Visible = false;
            buttonDevamEt.Visible = false;
            buttonYenidenBasla.Visible = false;
            pictureBox1.Image = Properties.Resources.pause_512;
            pictureBox2.Image = Properties.Resources.volumeON;
            pictureBox2.Visible = true;
            
            road = 0;
            speed = 70;
            timerRandomCar.Interval = 20;
            timerSerit.Interval = 100;
            
        }

        private void TimerGeriSayim_Tick(object sender, EventArgs e)
        {
            geriSayim -= 1;
           
        }

        void HizLevel()
        {
            // 2. seviye
            if (road > 100 && road <300)
            {
                speed = 100;
                timerSerit.Interval = 70;
                timerRandomCar.Interval = 15;
            }


            // 3. seviye
            else if (road > 300 && road < 500)
                 {
                    speed = 130;
                    timerSerit.Interval = 50;
                    timerRandomCar.Interval = 10;
                 }


            // 4. seviye
            else if (road > 500 && road >700 )
            {
                speed = 170;
                timerSerit.Interval = 30;
                timerRandomCar.Interval = 5;
            }

            // 5. seviye

            else if(road > 750 )
            {
                speed = 200;
                timerSerit.Interval = 20;
                timerRandomCar.Interval = 1;
            }
        }
        private void timerSerit_Tick(object sender, EventArgs e)
        {
            
            road++;
            HizLevel();
            if (seritHareket == false) 
            {
                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("labelSolSerit" + i.ToString(), true)[0].Top -= 25;
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top -= 25;
                    seritHareket = true;
                }
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("labelSolSerit" + i.ToString(), true)[0].Top += 25;
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top += 25;
                    seritHareket = false;
                }
            }
            labelRoad.Text = road.ToString() + "M";
            labelSpeed.Text = speed.ToString() + " Km/h";
            
        }
    }
}
