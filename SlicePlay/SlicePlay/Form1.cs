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
using System.Threading;

namespace SlicePlay
{
    public partial class Form1 : Form
    {
        Audio toruko_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_t.wav");
        Audio toruko_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_t.wav");
        Audio anaun_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
        Audio anaun_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
        Audio beep = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\p2000.wav");
        

        string[] file = System.IO.Directory.GetFiles(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\tango_demo", "*", System.IO.SearchOption.AllDirectories);
     

        public Form1()
        {
            InitializeComponent();
        }

        private void toruko_10_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_10r.wav");
            Audio left = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_10l.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            left.Play();
        }
        private void anaun_10_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_10r_a.wav");
            Audio left = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_10l_a.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void toruko_2_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_2r.wav");
            Audio left = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_2l.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();

            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void anaun_2_Click(object sender, EventArgs e)
        {
            Audio right = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_2r_a.wav");
            Audio left = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\output_2l_a.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            System.Threading.Thread.Sleep(50);
            left.Play();
        }

        private void toruko_p_Click(object sender, EventArgs e)
        {
            toruko_r.Stop();
            toruko_l.Stop();

            toruko_r.Balance = 10000;
            toruko_l.Balance = -10000;
            if (trackPlace.Value > 0)
            {
                toruko_r.Play();
                kiritori(toruko_r, 0);
                Thread.Sleep(trackPlace.Value * 3);
                toruko_l.Play();
                kiritori(toruko_l, trackSlice.Value);
            }
            else
            {
                toruko_l.Play();
                kiritori(toruko_l, 0);
                Thread.Sleep(Math.Abs(trackPlace.Value) * 3);
                toruko_r.Play();
                kiritori(toruko_r, trackSlice.Value);
            }
            
        }
        private void anaun_p_Click(object sender, EventArgs e)
        {
            anaun_r.Stop();
            anaun_l.Stop();

            anaun_r.Balance = 10000;
            anaun_l.Balance = -10000;

            if (trackPlace.Value > 0)
            {
                anaun_r.Play();
                kiritori(anaun_r, 0);
                Thread.Sleep(trackPlace.Value * 3);
                anaun_l.Play();
                kiritori(anaun_l, trackSlice.Value);
            }
            else
            {
                anaun_l.Play();
                kiritori(anaun_l, 0);
                Thread.Sleep(Math.Abs(trackPlace.Value) * 3);
                anaun_r.Play();
                kiritori(anaun_r,  trackSlice.Value);
            }
            
        }

        private async void kiritori(Audio onsei,int zure)
        {
            await Task.Delay(zure);

            for (int i = 0; i < 300; i++)
            {
                onsei.Volume = 0;
                await Task.Delay(trackSlice.Value);
                onsei.Volume = -10000;
                await Task.Delay(trackSlice.Value);
            }
        }

       

        private void trackSlice_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackSlice.Value + "m/s";
        }

        private void trackPlace_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackPlace.Value + "m";
        }


        private void tango_p_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(file);
            Random r = new System.Random();
            beep.Volume = -500;

            for (int i = 0; i < file.Length; i+=2)
            {
                Audio tango = new Audio(file[i]);
                Audio tango_n = new Audio(file[i + 1]);
                tango.Play();
                Thread.Sleep(Convert.ToInt32(tango.Duration*1000));
                
                int j=i;
                while(i==j){
                   j = r.Next(10);
                }
                Audio zatuon = new Audio(file[j]);

                beep.Play();
                zatuon.Play();
                Thread.Sleep(Convert.ToInt32(zatuon.Duration * 1000));
                beep.Stop();
                
            }


        }




        

        
    }
}
