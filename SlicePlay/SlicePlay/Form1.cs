using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace SlicePlay
{
    public partial class Form1 : Form
    {
        Audio toruko = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\input_t.wav");
        Audio anaun = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\input_a.wav");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_10r.wav");
            Audio left = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_10l.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            left.Play();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_10r_a.wav");
            Audio left = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_10l_a.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_2r.wav");
            Audio left = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_2l.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();

            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_2r_a.wav");
            Audio left = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output_2l_a.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toruko.Play();
            play_t();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            anaun.Play();
            play_a();
        }

        private async void play_t()
        {
            for (int i = 0; i < 100; i++)
            {
                toruko.Balance=10000;
                await Task.Delay(trackBar1.Value);
                toruko.Balance = -10000;
                await Task.Delay(trackBar1.Value);
            }
        }
        private async void play_a()
        {
            for (int i = 0; i < 100; i++)
            {
                anaun.Balance = 10000;
                await Task.Delay(trackBar2.Value);
                anaun.Balance = -10000;
                await Task.Delay(trackBar2.Value);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value + "m/s";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar2.Value + "m/s";
        }

        

        

        
    }
}
