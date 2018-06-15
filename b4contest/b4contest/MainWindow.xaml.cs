using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsFormsApplication1;
using Microsoft.DirectX.AudioVideoPlayback;

namespace b4contest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        List<object> boin = new List<object>();
        List<object> shin = new List<object>();

        Audio sound_b = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
        Audio sound_s = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");

        public MainWindow()
        {
            InitializeComponent();


            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav", "×"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\a.wav", "a"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\i.wav", "i"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\u.wav", "u"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\e.wav", "e"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\o.wav", "o"));
                                                                                 
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav", "×"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\k.wav", "k"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\s.wav", "s"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\t.wav", "t"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\n.wav", "n"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\h.wav", "h"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\m.wav", "m"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\j.wav", "y"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\r.wav", "r"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\w.wav", "w"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\g.wav", "g"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\z.wav", "z"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\d.wav", "d"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\b.wav", "b"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\p.wav", "p"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\v.wav", "v"));

            sound_b = new Audio(boin[0].ToString());
            sound_b.Balance = 10000;
            sound_s = new Audio(shin[0].ToString());
            sound_s.Balance = -10000;
             
        }


        public class CmbObject
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
                return Url;
            }

            public override bool Equals(string obj)
            {
                return (obj == this.Value);
                //return base.Equals(obj);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> data = new List<List<string>>();

            data = null;


            using (var csv = new CsvReader(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\result_time.csv"))
            {
                data = csv.ReadToEnd();
            }

            //配列の作成
            string text = textBox.Text;
            string[] str = new string[text.Length];

            string[] url_s = new string[text.Length];
            string[] url_b = new string[text.Length];

            int[] times = new int[text.Length];

            for (int i = 0; i < text.Length; i++) str[i] = text.Substring(i, 1);

            //検索して格納

            for (int i = 0; i < text.Length; i++)
            {
                int x = 0;

                while (x < data.Count)
                {
                    if (str[i] == data[x][0])
                    {
                        
                        object s =shin[shin.IndexOf(data[x][1])];
                        
                        url_s[i] = s.ToString();
                        object b = shin[shin.IndexOf(data[x][1])];
                        url_b[i] = b.ToString();
                        times[i] = Int32.Parse(data[x][3]);

                        break;
                    }
                    else
                    {
                        x++;
                    }

                }
            }

            shin_play(url_s);
            boin_play(url_b, times);

        }
        private async void shin_play(string[] urls)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                sound_s = new Audio(urls[i]);
                sound_s.Play();
                await Task.Delay(300);
            }
        }
        private async void boin_play(string[] urls,int[] times)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                sound_b = new Audio(urls[i]);
                await Task.Delay(times[i]);
                sound_b.Play();
                await Task.Delay(300);
            }
        }


    }
     
}
