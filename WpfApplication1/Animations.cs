using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class Animations
    {
        public static String lien;
        public static BitmapImage GetImage(string imageUri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imageUri, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        public static void AddSound(int i)
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer();

            switch (i)
            {
                /* Correct */
                case 1:
                    sp.SoundLocation = @"Correct.wav";
                    sp.Play();
                    break;
                /* Incorrect */
                case 2:
                    sp.SoundLocation = @"Incorrect.wav";
                    sp.Play();
                    break;
            }
        }
    }
}
