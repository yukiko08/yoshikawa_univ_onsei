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
   

    public partial class Form1 : Form
    {

        public CmbObject boin;
        public CmbObject shin;

        Audio sound_b;
        Audio sound_si;

        //Item item = new Item(0) ;
        //time = item.Times;

       
        int[] times = new int[]{0};

        string[] sounds = new string[] {"C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav"};

        public Form1()
        {
            InitializeComponent();


            strings.Text = " ";

            //母音リスト


            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav", "×",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\a.wav", "a",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\i.wav", "i",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\j.wav", "j",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\u.wav", "u",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\e.wav", "e",0));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\o.wav", "o",0));


            
            
            //子音リスト
          
           
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav", "×",0));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\k.wav", "k", 5));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\s.wav", "s",28));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\t.wav", "t", 6));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\tʃ.wav", "tʃ",6));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\n.wav", "n",5));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\h.wav", "h",6));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\f.wav", "f",4));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\m.wav", "m",5));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\j.wav", "y",2));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\r.wav", "r",4));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\w.wav", "w",6));

            //濁点
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\g.wav", "g",0));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\z.wav", "z", 0));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\d.wav", "d", 0));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\dʒ.wav", "dʒ", 0));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\b.wav", "b", 0));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\p.wav", "p", 0));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\v.wav", "v", 0));

            

        }

        public class CmbObject
        {
            public string Url { get; set; }
            public string Value { get; set; }
            public int Div { get; set; }
            public CmbObject(string Url, string Value,int Div)
            {
                this.Url = Url;
                this.Value = Value;
                this.Div = Div;
            }
            public override string ToString()
            {
                return Value;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            sound_b = new Audio(boin.Url);
            sound_si = new Audio(shin.Url);

            sound_b.Volume = vol_b.Value;
            sound_si.Volume = vol_s.Value;
            //10000:右　-10000:左
            sound_b.Balance = 10000;
            sound_si.Balance = -10000;

            double lg = trackBar2.Value * 0.002941; 
            int lon = Convert.ToInt32(Math.Floor(lg*1000));

            if (shin.Value != null && boin.Value != null)
            {
                if (trackBar1.Value > 0)
                {
                    sound_si.Play();
                    System.Threading.Thread.Sleep(trackBar1.Value * 10 +lon);
                    sound_b.Play();
                }
                else
                {
                    sound_b.Play();
                    System.Threading.Thread.Sleep(trackBar1.Value * -10 +lon);
                    sound_si.Play();
                }
            }


            time = trackBar1.Value;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            boin = (CmbObject)comboBox_b.SelectedItem;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            shin = (CmbObject)comboBox_s.SelectedItem;
            trackBar1.Value = shin.Div;
            textdiv1.Text = trackBar1.Value.ToString();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textdiv1.Text = trackBar1.Value.ToString();

        }


        private void string_in_Click(object sender, EventArgs e)
        {
            strings.AppendText(shin.Value);
            strings.AppendText(boin.Value);


            Array.Resize(ref times, times.Length + 1);
            Array.Resize(ref sounds, sounds.Length +2);
            times[times.Length - 1] = trackBar1.Value;
            sounds[sounds.Length - 2] = shin.Url;
            sounds[sounds.Length - 1] = boin.Url;
        }

        private async void go_Click(object sender, EventArgs e)
        {
            int t=0;
            for (int i = 1; i < sounds.Length-1; i=i+2)
            {
                sound_si = new Audio(sounds[i]);
                sound_b = new Audio(sounds[i+1]);

                t++;

                if (times[t] > 0)
                {
                    sound_si.Play();
                    await Task.Delay(times[t] * 10);
                    sound_b.Play();
                }
                else
                {
                    sound_b.Play();
                    await Task.Delay(times[t] * -10);
                    sound_si.Play();
                }
                await Task.Delay(500);

            }

            t = 0;
        }

        private void del_Click(object sender, EventArgs e)
        {
            strings.Text = " ";
            Array.Resize(ref sounds, 1);
            Array.Resize(ref times, 1);
            sounds[0] = "C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav";
            times[0] =  0 ;
        }

        int time;
        private async void Auto_Click(object sender, EventArgs e)
        {
            int stime = trackBar1.Value;
            for(int num=0;num<10;num++)
            {
                trackBar1.Value = stime + num;
                time = trackBar1.Value;
                textdiv1.Text = trackBar1.Value.ToString();

                button1.PerformClick();
                await Task.Delay(500);
               

            }

        }

        private async void crrect_Click(object sender, EventArgs e)
        {
            int v_s = vol_s.Value;
            int v_b = vol_b.Value;
            await Task.Run(() =>
            {
                //ここを追記処理にして，最後に書き込むようにする

                var data = new List<string>(){
                    shin.Value,boin.Value,time.ToString(),v_s.ToString(),v_b.ToString()
                };

                using (var writer = new CsvWriter("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\result.csv"))
                {
                    writer.WriteRow(data);
                    //非同期にすると失敗する
                }
            });
            


            
        }

        private void textdiv1_TextChanged(object sender, EventArgs e)
        {
      
            trackBar1.Value = int.Parse(textdiv1.Text);
       
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textlong.Text = trackBar2.Value.ToString();
        }

        private void form2_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            form2.Form1Obj = this;
            form2.Show();
        }

        private async void auto2_Click(object sender, EventArgs e)
        {
            int v = vol_b.Value;
            for (int num = 0; num >-2000; num=num-100)
            {
                vol_b.Value = v + num;

                button1.PerformClick();
                await Task.Delay(500);


            }
        }




      

        
    }
    
    public class Item
    {
        public int Times { get; set; }
        public Item(int Times){
            this.Times = Times;
    }
    }
}
