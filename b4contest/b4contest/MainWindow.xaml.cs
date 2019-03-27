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

        int label = 0;
        int set_vol;
        int vol_r; //0が最大
        int vol_l;


        List<List<string>> data = new List<List<string>>();

        //List<List<string>> delay = new List<List<string>>();

        public MainWindow()
        {
            InitializeComponent();
            set_vol = (int)(1000-100 * 20 * Math.Log10((distance.Maximum+0.3)/0.3 ));
            vol_Ctl();
            slice.Value = 10;

            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\none.wav", "×"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\a.wav", "a"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\i.wav", "i"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\u.wav", "u"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\e.wav", "e"));
            boin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\o.wav", "o"));
                                                                                                  
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\none.wav", "×"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\k.wav", "k"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\s.wav", "s"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\ʃ.wav", "ʃ"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\t.wav", "t"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\tʃ.wav", "tʃ"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\n.wav", "n"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\h.wav", "h"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\m.wav", "m"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\j.wav", "y"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\j.wav", "j"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\r.wav", "r"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\w.wav", "w"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\g.wav", "g"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\z.wav", "z"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\dʒ.wav", "dʒ"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\d.wav", "d"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\b.wav", "b"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\p.wav", "p"));
            shin.Add(new CmbObject("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav_音量\\v.wav", "v"));

            sound_b = new Audio(boin[0].ToString());
            sound_b.Balance = 10000;
            sound_s = new Audio(shin[0].ToString());
            sound_s.Balance = -10000;


            data = null;

            using (var csv = new CsvReader(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\result_time_vol.csv"))
            {
                data = csv.ReadToEnd();
            }

   
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

        private void shin_boin_play()
        {
            

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
                        break;
                    }else{
                        x++;
                    }

                }
            }

            int dis = (int)distance.Value;

            if (distance.Value >= 0)
            {

                Task.Run(async () =>
                {
                    boin_play(url_b, times, vols);
                    await Task.Delay((int)(dis * 2.94 * 2));
                    shin_play(url_s);

                }).Wait();
                
            }
            else
            {
                Task.Run(async () =>
                {
                    shin_play(url_s);
                    await Task.Delay((int)(dis * 2.94 * -2));
                    boin_play(url_b, times, vols);
                }).Wait();
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            shin_boin_play();

        }
        private async void shin_play(string[] urls)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                if (!urls.Contains(""))
                {
                    sound_s = new Audio(urls[i]);
                    sound_s.Volume = vol_l;
                    sound_s.Balance = -10000;
                    sound_s.Play();
                    await Task.Delay(500);
                    sound_s.Dispose();
                    
                }
                

            }
        }
        private async void boin_play(string[] urls,int[] times,int[] vols)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                if (!urls.Contains(""))
                {
                    sound_b = new Audio(urls[i]);
                    sound_b.Volume = vols[i] + vol_r;
                    sound_b.Balance = 10000;
                    await Task.Delay(times[i] * 10);
                    sound_b.Play();
                    await Task.Delay(500 - times[i] * 10);
                    

                }
                
             
            }
            sound_b.Dispose();
        }
        

        private void distance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vol_Ctl();
            time_dis.Content = distance.Value;
        }
        private void slice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            div_sl.Content = slice.Value/1000;
            
        }



        Audio music_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\えみぬみぱ，びぢぢざご，だわべびめ，ひぱのには，つけかべく，ずかなざぱ，へのぽしあ，そかめおざ.wav");
        Audio music_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\えみぬみぱ，びぢぢざご，だわべびめ，ひぱのには，つけかべく，ずかなざぱ，へのぽしあ，そかめおざ.wav");

        private void music_Click(object sender, RoutedEventArgs e)
        {
            Audio music_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\えみぬみぱ，びぢぢざご，だわべびめ，ひぱのには，つけかべく，ずかなざぱ，へのぽしあ，そかめおざ.wav");
            Audio music_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\えみぬみぱ，びぢぢざご，だわべびめ，ひぱのには，つけかべく，ずかなざぱ，へのぽしあ，そかめおざ.wav");

            music_r.Balance = 10000;
            music_l.Balance = -10000;

            music_r.Volume = vol_r;
            music_l.Volume = vol_l;

            int dis = (int)distance.Value;
            int div = (int)slice.Value;

            if (distance.Value >= 0)
            {
                Task.Run(async() =>
                {

                    music_r.Play();
                    kiritori(music_r, div,0);
                    await Task.Delay(dis*6);
                    music_l.Play();
                    kiritori(music_l, div,div);
                });
                
            }
            else
            {
                Task.Run(async() =>
                {
                    music_l.Play();
                    kiritori(music_l, div,0);
                    await Task.Delay(dis*-6);
                    music_r.Play();
                    kiritori(music_r, div,div);
                });
                
            }
        }

        Audio announ_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
        Audio announ_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
        

        private string test_time_play(string url)
        {
            Audio test_time_r = new Audio(url);
            Audio test_time_l = new Audio(url); 

            test_time_r.Balance = 10000;
            test_time_l.Balance = -10000;

            test_time_r.Volume = vol_r;
            test_time_l.Volume = vol_l;
            int dis = (int)distance.Value;
            int div = (int)slice.Value;

            if (distance.Value >= 0)
            {
                Task.Run(async () =>
                {
                    test_time_r.Play();
                    kiritori(test_time_r, div, 0);
                    await Task.Delay((int)(dis * 2.94 * 2));
                    test_time_l.Play();
                    kiritori(test_time_l, div, div);
                }).Wait();
            }
            else
            {
                Task.Run(async () =>
                {
                    test_time_l.Play();
                    kiritori(test_time_l, div, 0);
                    await Task.Delay((int)(dis * 2.94 * -2));
                    test_time_r.Play();
                    kiritori(test_time_r, div, div);

                }).Wait();
            }
            return url;
        }
        private void announcer_Click(object sender, RoutedEventArgs e)
        {
            Audio announ_r = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
            Audio announ_l = new Audio(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\input_a.wav");
            announ_r.Balance = 10000;
            announ_l.Balance = -10000;
            announ_r.Volume = vol_r;
            announ_l.Volume = vol_l;
            int dis = (int)distance.Value;
            int div = (int)slice.Value;

            if (distance.Value >= 0)
            {
                Task.Run(async () =>
                {
                    announ_r.Play();
                    kiritori(announ_r, div, 0);
                    await Task.Delay((int)(dis * 2.94 * 2));
                    announ_l.Play();
                    kiritori(announ_l, div, div);
                }).Wait();
            }
            else
            {
                Task.Run(async () =>
                {
                    announ_l.Play();
                    kiritori(announ_l, div, 0);
                    await Task.Delay((int)(dis * 2.94 * -2));
                    announ_r.Play();
                    kiritori(announ_r, div, div);

                }).Wait();
                
            }
            
        }

        private async void kiritori(Audio onsei, int zure,int st)
        {
            await Task.Delay(st);

            while (onsei.Playing == true)
            {
                onsei.Volume = -10000;
                await Task.Delay(zure);
                if (st != 0) { 
                    onsei.Volume =(vol_l > vol_r ? vol_r : vol_l);
                }
                else
                {
                    onsei.Volume = (vol_l > vol_r ? vol_l :vol_r);
                }
                await Task.Delay(zure);
            }
            onsei.Dispose();
           
        }



        private void stop_Click(object sender, RoutedEventArgs e)
        {
            announ_l.Stop();
            announ_r.Stop();
            music_r.Stop();
            music_l.Stop();

            st = 1;
        }


        private void vChk_Click_1(object sender, RoutedEventArgs e)
        {
            //音量を反映するかのラベル
            if ((bool)vChk.IsChecked)
            {
                label = 1;
            }else{
                label = 0;
            }
            vol_Ctl();
        }
        private void vol_Ctl()
        {

            if (label == 1)
            {
                
                    vol_r = (int)(set_vol - 100 * 20 * Math.Log10((distance.Maximum+0.3 - distance.Value) / distance.Maximum+0.3));
                    vol_l = (int)(set_vol - 100 * 20 * Math.Log10((distance.Maximum+0.3 + distance.Value) / distance.Maximum+0.3));
                    Console.WriteLine("vol_r:" + vol_r + ",vol_l:" + vol_l);     
                
        
                
            }
            else
            {
                ///元の音源を中央で聞くと～とする
                vol_r = set_vol;
                vol_l = set_vol;
            }
        }



        string[] file = System.IO.Directory.GetFiles(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\jidou_gosei", "*", System.IO.SearchOption.AllDirectories);
        

        private void tangoPlay_Click(object sender, RoutedEventArgs e)
        {
            st = 0;
            Audio mono_r = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
            Audio mono_l = new Audio("C:\\Users\\S2\\OneDrive\\デスクトップ\\研究室\\子音と母音\\wav\\none.wav");
            mono_r.Balance = 10000;
            mono_l.Balance = -10000;

            /*using (var csv = new CsvReader(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\duration.csv"))
            {
                delay = csv.ReadToEnd();
            }*/

            

            int dis = (int)distance.Value;

            if (distance.Value >= 0)
            {
                Task.Run(async () =>
                {
                    mono_play(0,mono_r);
                    await Task.Delay((int)(dis * 2.94*2));
                    mono_play(1,mono_l);
                });
            }
            else
            {
                Task.Run(async () =>
                {
                    mono_play(1,mono_l);
                    await Task.Delay((int)(dis * 2.94 * -2));
                    mono_play(0,mono_r);

                });
            }
            
            
        }


        int st = 0;
        private async void mono_play(int a,Audio onsei)
        {
            await Task.Delay(138*a);
            for (int i = a; i < file.Length-1 ; i+=2)
            {
                
                onsei = new Audio(file[i]);
                Audio onsei_n = new Audio(file[i+1]);
                if (a == 0)
                {
                    onsei.Balance = 10000;
                }
                else
                {
                    onsei.Balance = -10000;
                    
                }
                onsei.Play();
                await Task.Delay((int)(onsei.Duration*1000 + onsei_n.Duration*1000));
                Console.WriteLine(i+":"+onsei.Duration);
                onsei.Dispose();
                onsei_n.Dispose();
                if (st== 1)
                {
                    break;
                }

            }
        }

        private string[] rand_hiragana()
        {   
            System.Random r = new System.Random();
            string[] lis = new string[]{"あ","い","う","え","お",
                                        "か","き","く","け","こ",
                                        "さ","し","す","せ","そ",
                                        "た","ち","つ","て","と",
                                        "な","に","ぬ","ね","の",
                                        "は","ひ","ふ","へ","ほ",
                                        "ま","み","む","め","も",
                                        "や","ゆ","よ",
                                        "ら","り","る","れ","ろ",
                                        "わ","を","ん",
                                        "が","ぎ","ぐ","げ","ご",
                                        "ざ","じ","ず","ぜ","ぞ",
                                        "だ","ぢ","づ","で","ど",
                                        "ば","び","ぶ","べ","ぼ",
                                        "ぱ","ぴ","ぷ","ぺ","ぽ"};
            string[] ans = new string[5];
            for(int i=0;i<5;i++){
                ans[i] =lis[r.Next(71)];
            }
            return ans;
        }

        private async void test_Click(object sender, RoutedEventArgs e)
        {
            System.Random r = new System.Random();
            List<string> listtext = new List<string>();
            double[] test_dis = new double[] { 0, 7.5, 15, -7.5, -15 }; //new int[] { 0, 0, 0, 0, 0, 0, 0 }; 
            List<double> list_dis = new List<double>();
            list_dis.AddRange(test_dis);
            await Task.Delay(10000);
            for(int j=0;j<7;j++){
                for (int i = 0; i < test_dis.Length; i++)
                {
                    int rand = r.Next(list_dis.Count);
                    distance.Value = list_dis[rand];
                    

                    textBox.Text = string.Join("", rand_hiragana());
                    listtext.Add(textBox.Text);
                    listtext.Add(distance.Value.ToString("#;-#;0"));
                    listtext.Add("\r\n");
                    shin_boin_play();
                    await Task.Delay(7000);   //あきらめた
                    list_dis.RemoveAt(rand);
                }
                
                
                list_dis.AddRange(test_dis);
                using (var csv = new CsvWriter(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\hiragana.csv"))
                {
                    Console.Write(listtext);
                    csv.WriteRow(listtext);
                    csv.Close();
                }

                listtext = new List<string>();

            }
            listtext.Add("\r\n");
        
            
        
       
               
        }

        private async void test_t_Click(object sender, RoutedEventArgs e)
        {
            double[] test_dis = new double[] { 0, 7.5, 15, -7.5, -15 }; //new int[] { 0, 0, 0, 0, 0, 0, 0 }; 
            List<double> list_dis = new List<double>();
            

            int[] test_time = new int[] { 10, 50, 100, 150, 200};//0.01 0,05 ,0.1,0.15
            List<int> list_time = new List<int>();

            string[] file_t = System.IO.Directory.GetFiles(@"C:\Users\S2\OneDrive\デスクトップ\研究室\音\ランダム５文字_女性\wav_音量", "*", System.IO.SearchOption.AllDirectories);

            System.Random r = new System.Random();
            List<string> listtext = new List<string>();


            list_time.AddRange(test_time);
        
            for (int i = 0; i < test_time.Length; i++)
            {
                int rand_t = r.Next(list_time.Count);
                slice.Value = list_time[rand_t];
                div_sl.Content = slice.Value / 1000;
                list_time.RemoveAt(rand_t);
                listtext.Add(div_sl.Content.ToString());
                listtext.Add("\r\n");


                list_dis.AddRange(test_dis);

                for (int j = 0; j < test_dis.Length; j++)
                {
                    int rand_d = r.Next(list_dis.Count);
                    distance.Value = list_dis[rand_d];
                    listtext.Add(distance.Value.ToString());
                    await Task.Delay(2500);   //あきらめた
                    string url = test_time_play(file_t[r.Next(file_t.Length)]);
                    listtext.Add(url.Substring(url.Length - 9, 5));

                    listtext.Add("\r\n");
                    list_dis.RemoveAt(rand_d);

                    await Task.Delay(4500);

                }
                using (var csv = new CsvWriter(@"C:\Users\S2\OneDrive\デスクトップ\研究室\子音と母音\jikanjiku.csv"))
                {
                    Console.Write(listtext);
                    csv.WriteRow(listtext);
                    csv.Close();
                }
                listtext = new List<string>();
            }
            
        }

       

     




    }
     
}




