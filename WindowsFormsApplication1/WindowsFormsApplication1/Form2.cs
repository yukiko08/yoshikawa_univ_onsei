using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {

        List<List<string>> data = new List<List<string>>();


        Audio boin = new Audio("C:\\Users\\yukik\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
        Audio shin = new Audio("C:\\Users\\yukik\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");

        public Form1 Form1Obj;


        public Form2()
        {
            InitializeComponent();
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            data = null;

            using (var csv = new CsvReader(@"C:\\Users\\yukik\\OneDrive\\デスクトップ\\研究室\\子音と母音\result_time.csv"))
            {
                data = csv.ReadToEnd();
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string[] str = new string[text.Length];

            string[] url_s = new string[text.Length];
            string[] url_b = new string[text.Length];
            int[] times = new int[text.Length];


            for (int i = 0; i < text.Length; i++) str[i] = text.Substring(i, 1);

            //検索して格納

            for (int i = 0; i < text.Length ; i++)
            {
                int x = 0;
                while(x<data.Count){
                    if (str[i] == data[x][0])
                    {
                        int num_s = Form1Obj.comboBox_s.FindString(data[x][1]);
                        Form1Obj.comboBox_s.SelectedIndex = num_s;
                        url_s[i] = Form1Obj.shin.Url;
                        int num_b = Form1Obj.comboBox_b.FindString(data[x][2]);
                        Form1Obj.comboBox_b.SelectedIndex = num_b;
                        url_b[i] = Form1Obj.boin.Url;

                        times[i] = Int32.Parse(data[x][3]);

                        break;
                    }
                    else
                    {
                        x++;
                    }
                    
                }
            }
            shin.Volume = -1000 - distance.Value;
            boin.Volume = -1500 + distance.Value;

            shin.Balance = -10000;
            boin.Balance = 10000;

            shin_Asyns(url_b);
            boin_Asyns(url_s, times);

        }


        //一音を0.5秒ごとにならす
        private async void shin_Asyns(string[] url)
        {
            for (int i = 0; i < textBox1.Text.Length ; i++)
            {
                shin = new Audio(url[i]);
                shin.Play();
                await Task.Delay(500);
            }
            

            int time = Convert.ToInt32(distance.Value * 2.941);
            
        }
        private async void boin_Asyns(string[] url,int[] time)
        {
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                boin = new Audio(url[i]);
                label4.Text = time[i].ToString();
                boin.Play();
                await Task.Delay(distance.Value > 0 ? 500 + time[i]*10 + Convert.ToInt32(distance.Value * 2.941) : 500 + time[i] - Convert.ToInt32(distance.Value * -2.941));
            }



        }


        private void distance_Scroll(object sender, EventArgs e)
        {
            dis_label.Text = distance.Value.ToString()+"m"; 
        }

    }
}
