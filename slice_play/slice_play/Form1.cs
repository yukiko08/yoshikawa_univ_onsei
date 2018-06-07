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


namespace slice_play
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Audio right= new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output.wav");
            Audio left = new Audio(@"C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\output2.wav");

            right.Balance = 10000;
            left.Balance = -10000;

            right.Play();
            left.Play();
        }
    }
}
