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

        Audio sound_b;
        Audio sound_si;

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

        private void start_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string[] str = new string[text.Length];
            for (int i = 0; i < text.Length; i++) str[i] = text.Substring(i, 1);

            //検索
            for (int i = 0; i < text.Length ; i++)
            {
                int x = 0;
                int y;
                while(x<data.Count){
                    if (str[i] == data[x][0])
                    {

                       // Form1Obj.comboBox_b.Items();
                        break;
                    }
                    else
                    {
                        x++;
                    }
                    
                }
            }

        }

    }
}
