using System.Windows.Media.Imaging;

namespace Links.Models.Collections.Serialization
{
    internal interface IBitmapImageSerializer
    {
        BitmapImage Deserialize(string data);
        string Serialize(BitmapImage image);
    }
}
