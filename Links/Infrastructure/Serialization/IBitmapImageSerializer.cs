using System.Windows.Media.Imaging;

namespace Links.Infrastructure.Serialization
{
    internal interface IBitmapImageSerializer
    {
        BitmapImage Deserialize(string data);
        string Serialize(BitmapImage image);
    }
}
