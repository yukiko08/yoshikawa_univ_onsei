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
        List<CmbObject> boin = new List<CmbObject>();
        List<CmbObject> shin = new List<CmbObject>();

        Audio sound_b = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
        Audio sound_s = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
        

        public MainWindow()
        {
            InitializeComponent();
            slice.Value = 10;
            

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



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> data = new List<List<string>>();

            data = null;


            using (var csv = new CsvReader(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\result_time_vol.csv"))
            {
                data = csv.ReadToEnd();
            }

            //配列の作成
            string text = textBox.Text;
            string[] str = new string[text.Length];

            string[] url_s = new string[text.Length];
            string[] url_b = new string[text.Length];

            int[] times = new int[text.Length];
            int[] vols = new int[text.Length];

            for (int i = 0; i < text.Length; i++) str[i] = text.Substring(i, 1);

            //検索して格納

            for (int i = 0; i < text.Length; i++)
            {
                int x = 0;

                while (x < data.Count)
                {
                    if (str[i] == data[x][0])
                    {
                        
                        url_s[i] = shin.Find(v => v.Value == data[x][1]).Url;

                        url_b[i] = boin.Find(v => v.Value == data[x][2]).Url;
                        times[i] = Int32.Parse(data[x][3]);
                        vols[i] = Int32.Parse(data[x][4]);
                        Console.WriteLine(url_b[i]+url_s[i]);
                        break;
                    }
                    else
                    {
                        x++;
                    }

                }
            }

            if (distance.Value >= 0)
            {
                Task.Run(() =>
                {
                    boin_play(url_b, times, vols);
                    Task.Delay((int)distance.Value * 3);
                    shin_play(url_s);

                });
            }
            else
            {
                Task.Run(() =>
                {
                    shin_play(url_s);
                    Task.Delay((int)distance.Value * -3);
                    boin_play(url_b, times, vols);
                });
            }
            

        }
        private async void shin_play(string[] urls)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                sound_s = new Audio(urls[i]);
                sound_s.Volume = -1500;
                sound_s.Play();
                await Task.Delay(400);
            }
        }
        private async void boin_play(string[] urls,int[] times,int[] vols)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                sound_b = new Audio(urls[i]);
                sound_b.Volume = vols[i];
                await Task.Delay(times[i] * 10);
                time_sb.Content = (double)times[i]/100 ;
                sound_b.Play();
                await Task.Delay(400-times[i]*10);
            }
        }

        private void distance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            time_dis.Content = distance.Value;
        }
        private void slice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            div_sl.Content = slice.Value/1000;
            
        }

        Audio music_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_t.wav");
        Audio music_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_t.wav");

        private void music_Click(object sender, RoutedEventArgs e)
        {
            music_r.Balance = 10000;
            music_l.Balance = -10000;

            if (distance.Value >= 0)
            {
                Task.Run(() =>
                {
                    music_r.Play();
                    kiritori(music_r, 0);
                    Task.Delay((int)distance.Value * 3);
                    music_l.Play();
                    kiritori(music_l, (int)slice.Value);
                });
                
            }
            else
            {
                Task.Run(() =>
                {
                    music_l.Play();
                    kiritori(music_l, 0);
                    Task.Delay((int)distance.Value * -3);
                    music_r.Play();
                    kiritori(music_r, (int)slice.Value);
                });
                
            }
        }

        Audio announ_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
        Audio announ_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");

        private void announcer_Click(object sender, RoutedEventArgs e)
        {
            if (distance.Value >= 0)
            {
                Task.Run(() =>
                {
                    announ_r.Play();
                    kiritori(announ_r, 0);
                    Task.Delay((int)distance.Value * 3);
                    announ_l.Play();
                    kiritori(announ_l, (int)slice.Value);
                });
            }
            else
            {
                Task.Run(() =>
                {
                    announ_l.Play();
                    kiritori(announ_l, 0);
                    Task.Delay((int)distance.Value * -3);
                    announ_r.Play();
                    kiritori(announ_r, (int)slice.Value);

                });
            }
        }

        private async void kiritori(Audio onsei, int zure)
        {
            await Task.Delay(zure);

            while (onsei.Playing == true)
            {
                onsei.Volume = -10000;
                await Task.Delay(zure);
                onsei.Volume = 0;
                await Task.Delay(zure);

            }
           
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            announ_l.Stop();
            announ_r.Stop();
            music_r.Stop();
            music_l.Stop();
        }


    }
     
}
