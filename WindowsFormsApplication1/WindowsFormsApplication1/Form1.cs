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

        CmbObject boin;
        CmbObject shin;

        Audio sound_b;
        Audio sound_si;

        int time;
        Item item = new Item(0) ;
        //time = item.Times;
        
        int volume;

        string[] sounds = new string[] {"C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav"};

        public Form1()
        {
            InitializeComponent();
            strings.Text = " ";

            //母音リスト


            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav", "×"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\a.wav", "a"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\i.wav", "i"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\j.wav", "j"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\u.wav", "u"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\e.wav", "e"));
            comboBox_b.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\o.wav", "o"));


            
            
            //子音リスト
          
           
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav", "×"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\k.wav", "k"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\s.wav", "s"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\s_1.wav", "s_1"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\s_2.wav", "s_2"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\θ.wav", "θ"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\ʃ.wav", "ʃ"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\t.wav", "t"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\ʒ.wav", "ʒ"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\tʃ.wav", "tʃ"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\n.wav", "n"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\ŋ.wav", "ŋ"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\h.wav", "h"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\f.wav", "f"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\m.wav", "m"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\j.wav", "y"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\r.wav", "r"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\l.wav", "l"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\w.wav", "w"));

            //濁点
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\g.wav", "g"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\z.wav", "z"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\d.wav", "d"));
            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\dʒ.wav", "dʒ"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\b.wav", "b"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\p.wav", "p"));

            comboBox_s.Items.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\v.wav", "v"));

            

        }

        class CmbObject
        {
            public string Url { get; set; }
            public string Value { get; set; }
            public CmbObject(string Url, string Value)
            {
                this.Url = Url;
                this.Value = Value;
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

            if (shin.Value != null && boin.Value != null)
            {
                if (time > 0)
                {
                    sound_si.Play();
                    System.Threading.Thread.Sleep(time * 10);
                    sound_b.Play();
                }
                else
                {
                    sound_b.Play();
                    System.Threading.Thread.Sleep(time * -10);
                    sound_si.Play();
                }
            }
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            boin = (CmbObject)comboBox_b.SelectedItem;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            shin = (CmbObject)comboBox_s.SelectedItem;
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            time = trackBar1.Value;
            label3.Text = time.ToString();

        }

        private void string_in_Click(object sender, EventArgs e)
        {
            strings.AppendText(shin.Value);
            strings.AppendText(boin.Value);
            
            Array.Resize(ref sounds, sounds.Length +2);
            sounds[sounds.Length - 2] = shin.Url;
            sounds[sounds.Length - 1] = boin.Url;
        }

        private void go_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < sounds.Length-1; i=i+2)
            {
                sound_si = new Audio(sounds[i]);
                sound_b = new Audio(sounds[i+1]);


                if (time > 0)
                {
                    sound_si.Play();
                    System.Threading.Thread.Sleep(time * 10);
                    sound_b.Play();
                }
                else
                {
                    sound_b.Play();
                    System.Threading.Thread.Sleep(time * -10);
                    sound_si.Play();
                }
                System.Threading.Thread.Sleep(500);

            }
            
 
        }

        private void del_Click(object sender, EventArgs e)
        {
            strings.Text = " ";
            Array.Resize(ref sounds, 1);
            sounds[0] = "C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav";
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            time = -10;
            
            while (time<=10)
            {
                trackBar1.Value = time;

                label3.Text = time.ToString();
                Console.WriteLine(time.ToString());

                button1.PerformClick();
                System.Threading.Thread.Sleep(800);
                time++;

            }

        }

        private void crrect_Click(object sender, EventArgs e)
        {
            //ここを追記処理にして，最後に書き込むようにする
            var data = new List<List<string>>()
            {
                new List<string>(){shin.Value,boin.Value,time.ToString()},
            };
            using (var writer = new CsvWriter(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\result.csv"))
            {
                writer.Write(data);
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
