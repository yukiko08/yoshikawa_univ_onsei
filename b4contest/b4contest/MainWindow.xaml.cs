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

namespace b4contest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CmbObject none =new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\none.wav", "×");

            List<object> boin = new List<object>();

            boin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\a.wav", "a"));
            
            boin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\i.wav", "i"));
            boin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\u.wav", "u"));
            boin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\e.wav", "e"));
            boin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\o.wav", "o"));
            
            List<object> shin = new List<object>();

            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\k.wav", "k"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\s.wav", "s"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\t.wav", "t"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\n.wav", "n"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\h.wav", "h"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\m.wav", "m"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\j.wav", "y"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\r.wav", "r"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\w.wav", "w"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\g.wav", "g"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\z.wav", "z"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\d.wav", "d"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\b.wav", "b"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\p.wav", "p"));
            shin.Add(new CmbObject("C:\\Users\\S2\\Music\\日本語発音\\wav\\v.wav", "v"));
            
             
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
                return Value;
            }

        }
    }
}
