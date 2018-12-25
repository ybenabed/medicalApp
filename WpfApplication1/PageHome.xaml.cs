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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        private int num { get; set; }
        public List<String> listImage { get; set; }
        public PageHome()
        {
            InitializeComponent();

            List<MaterialDesignThemes.Wpf.PackIcon> listicon = new List<MaterialDesignThemes.Wpf.PackIcon>();
            listicon.Add(Image1); listicon.Add(Image2); listicon.Add(Image3); listicon.Add(Image4);
            listicon.Add(Image5); listicon.Add(Image6);
            listImage = new List<string>();
            listImage.Add(@"Images\Animation2.jpg");
            listImage.Add(@"Images\Animation3.jpg"); listImage.Add(@"Images\Animation4.jpg");
            listImage.Add(@"Images\Animation5.jpg"); listImage.Add(@"Images\Animation6.jpg");
            listImage.Add(@"Images\Animation1.jpg");
            num = 0;
            var change = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 5) };
            change.Tick += delegate
            {
                change.Stop();
                var deuxieme = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 50) };
                deuxieme.Tick += delegate
                {
                    Image.Opacity = Image.Opacity - 0.1f;
                    if (Image.Opacity < 0.05)
                    {
                        deuxieme.Stop();
                        Image.Opacity = 1;
                        Image.Source = Animations.GetImage(listImage[num]);
                        num++; if (num == 6) num = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            if (i == num) listicon[i].Kind = MaterialDesignThemes.Wpf.PackIconKind.Circle;
                            else listicon[i].Kind = MaterialDesignThemes.Wpf.PackIconKind.CircleOutline;
                        }
                        deuxieme.Stop();
                    }
                    else
                    {
                        deuxieme.Start();
                    }
                };
                deuxieme.Start();
                change.Start();
            };
            change.Start();
        }
    }
}
