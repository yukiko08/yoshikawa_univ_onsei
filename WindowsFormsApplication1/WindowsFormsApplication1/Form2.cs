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


        Audio boin;
        Audio shin;

        public Form1 Form1Obj;

        public Form2()
        {
            InitializeComponent();
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            data = null;

            using (var csv = new CsvReader(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\result_time.csv"))
            {
                data = csv.ReadToEnd();
            }
        }

        private async void start_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string[] str = new string[text.Length];
            for (int i = 0; i < text.Length; i++) str[i] = text.Substring(i, 1);

            //検索
            for (int i = 0; i < text.Length ; i++)
            {
                int x = 0;
                while(x<data.Count){
                    if (str[i] == data[x][0])
                    {
                        int num_s = Form1Obj.comboBox_s.FindString(data[x][1]);
                        Form1Obj.comboBox_s.SelectedIndex = num_s;
                        int num_b = Form1Obj.comboBox_b.FindString(data[x][2]);
                        Form1Obj.comboBox_b.SelectedIndex = num_b;

                        break;
                    }
                    else
                    {
                        x++;
                    }
                    
                }
                shin = new Audio(Form1Obj.shin.Url);
                shin.Balance = -10000;
                boin = new Audio(Form1Obj.boin.Url);
                boin.Balance = 10000;

                int time = Int32.Parse(data[x][3]) * 10 + Convert.ToInt32(distance.Value * 2.941);
                if (time > 0)
                {
                    shin.Play();
                    await Task.Delay(time);
                    boin.Play();
                }
                else
                {
                    boin.Play();
                    await Task.Delay(-time);
                    shin.Play();
                }
                

                await Task.Delay(500);
                

            }


        }


        private void distance_Scroll(object sender, EventArgs e)
        {
            dis_label.Text = distance.Value.ToString(); 
        }

    }
}
