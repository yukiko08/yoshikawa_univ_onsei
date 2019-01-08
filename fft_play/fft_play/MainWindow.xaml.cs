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

using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;

using System.Windows.Forms;

using Microsoft.DirectX.AudioVideoPlayback;

namespace fft_play
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CmbObject> fs = new List<CmbObject>();

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void left_sound_Click(object sender, RoutedEventArgs e)
        {
            CmbObject lbl = fs.Find(v => v.Value ==file_list.SelectedValue.ToString());
            left_name.Content = lbl.Url;
            left_list.Items.Add(lbl.Value);
      
        }

        private void right_sound_Click(object sender, RoutedEventArgs e)
        {
            CmbObject lbr = fs.Find(v => v.Value == file_list.SelectedValue.ToString());
            right_name.Content = lbr.Url;
            right_list.Items.Add(lbr.Value);
            
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            System.Random r = new System.Random();
            int idx = r.Next(left_list.Items.Count);
            Console.Write(idx);
            left_list.SelectedIndex = idx;
            right_list.SelectedIndex = idx;

            Audio sound_r = new Audio(fs.Find(v => v.Value == left_list.SelectedValue.ToString()).Url);
            Audio sound_l = new Audio(fs.Find(v => v.Value == right_list.SelectedValue.ToString()).Url);
     
            sound_r.Balance = 10000;
            sound_l.Balance = -10000;

            int wait = ((int)distance.Value )* 3;

            if (distance.Value >= 0)
            {
                Task.Run(async () =>
                {
                    sound_r.Play();
                    await Task.Delay(wait);
                    sound_l.Play();
     
                }).Wait();
            }
            else
            {
                Task.Run(async () =>
                {
                    sound_l.Play();
                    await Task.Delay(-wait);
                    sound_r.Play();
                }).Wait();
            }

        }

        private void file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // 複数ファイルを選択できるようにするかどうかの設定
            ofd.Multiselect = true;
            file_list.Items.Clear();

            // ダイアログボックスの表示
           

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 選択されたファイルを取り出す
                foreach (string strFilePath in ofd.FileNames)
                {
                    // ファイルパスからファイル名を取得
                    string strFileName = System.IO.Path.GetFileName(strFilePath);//
                    file_list.Items.Add(strFileName);
                    fs.Add(new CmbObject(strFilePath,strFileName));

                }
            }

        }

        private void distance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dis.Content =(int)distance.Value+"m";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Random r = new System.Random();
            List<int> list_dis = new List<int>() { 0, 15, 30, 45, -15, -30, -45 };


            List<string> listtext = new List<string>();

            while (list_dis.Count > 0)
            {
                listtext.Add("\r\n");
                int idx = r.Next(left_list.Items.Count);
                Console.WriteLine(idx);
                left_list.SelectedIndex = idx;
                right_list.SelectedIndex = idx;

                int d = r.Next(list_dis.Count);
                distance.Value = list_dis[d];
                list_dis.RemoveAt(d);

                Audio sound_r = new Audio(fs.Find(v => v.Value == right_list.SelectedValue.ToString()).Url);
                Audio sound_l = new Audio(fs.Find(v => v.Value == left_list.SelectedValue.ToString()).Url);

                sound_r.Balance = 10000;
                sound_l.Balance = -10000;

                int wait = ((int)distance.Value) * 3;
                Console.WriteLine(wait);

                if (distance.Value >= 0)
                {
                    Task.Run(async () =>
                    {
                        sound_r.Play();
                        await Task.Delay(wait);
                        sound_l.Play();

                    }).Wait();
                }
                else
                {
                    Task.Run(async () =>
                    {
                        sound_l.Play();
                        await Task.Delay(-wait);
                        sound_r.Play();
                    }).Wait();
                }
                //log
                listtext.Add(left_list.SelectedValue.ToString().Substring(0, 5));
                listtext.Add("," + distance.Value);

                
                await Task.Delay(5000);
            }
            
            using (var csv = new CsvWriter(@"C:\Users\S2\OneDrive\デスクトップ\研究室\音\fft_test.csv"))
            {
                csv.WriteRow(listtext);
                csv.Close();
            }
            
        }
    }
}
